using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessCommons;
using ChessFabrickCommons.Actors;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Utils;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ChessFabrickStateful
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ChessFabrickStateful : StatefulService, IChessFabrickStatefulService
    {
        private ServiceProxyFactory proxyFactory;
        private readonly Uri playerServiceUri;
        private readonly Uri chessSignalRUri;
        private readonly Uri chessActorUri;

        public ChessFabrickStateful(StatefulServiceContext context) : base(context)
        {
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.playerServiceUri = new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickPlayersStateful");
            this.chessSignalRUri = new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickWeb");
            this.chessActorUri = new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickActor");
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                 new ServiceReplicaListener((c) =>
                 {
                     return new FabricTransportServiceRemotingListener(c, this);
                 })
             };
        }

        private Task<IReliableDictionary2<string, ChessGameInfo>> GetNewGameDict()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary2<string, ChessGameInfo>>("dict_new_games");
        }

        private Task<IReliableDictionary2<string, ChessGameInfo>> GetActiveGameDict()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary2<string, ChessGameInfo>>("dict_active_games");
        }

        private Task<IReliableDictionary2<string, ChessGameInfo>> GetCompletedGameDict()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary2<string, ChessGameInfo>>("dict_completed_games");
        }

        private async Task<ChessGameInfo> GetActiveGameAsync(ITransaction tx, string gameId)
        {
            var dictGames = await GetActiveGameDict();
            var game = await dictGames.TryGetValueAsync(tx, gameId);
            if (!game.HasValue)
            {
                throw new ArgumentException("Game does not exist.");
            }
            return game.Value;
        }

        private async Task<Board> GetActiveGameBoardAsync(ITransaction tx, string gameId)
        {
            var gameInfo = await GetActiveGameAsync(tx, gameId);
            var board = new Board();
            board.PerformMoves(gameInfo.MoveHistory);
            return board;
        }

        public Task<string> HelloChessAsync()
        {
            return Task.FromResult($"Allo Chess: {Guid.NewGuid().ToString()}");
        }

        public async Task<ChessGameInfo> NewGameAsync(string gameId, string playerName, PieceColor playerColor)
        {
            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(playerName));
            var player = await playersClient.PlayerInfoAsync(playerName);
            var dictGames = await GetNewGameDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var game = playerColor == PieceColor.White ?
                    new ChessGameInfo(gameId, player, null) :
                    new ChessGameInfo(gameId, null, player);
                await dictGames.AddAsync(tx, gameId, game);
                await playersClient.AddPlayerGameAsync(playerName, gameId);
                await tx.CommitAsync();
                return game;
            }
        }

        public async Task<ChessGameInfo> JoinGameAsync(string gameId, string playerName)
        {
            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(playerName));
            var player = await playersClient.PlayerInfoAsync(playerName);
            var dictNewGames = await GetNewGameDict();
            var dictActiveGames = await GetActiveGameDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await dictNewGames.TryGetValueAsync(tx, gameId);
                if (!game.HasValue)
                {
                    throw new ArgumentException("Game does not exist.");
                }
                if ((game.Value.White ?? game.Value.Black).Name == playerName)
                {
                    throw new ArgumentException("Can't play against yourself");
                }
                var activeGame = game.Value.White == null ?
                    new ChessGameInfo(game.Value.GameId, player, game.Value.Black) :
                    new ChessGameInfo(game.Value.GameId, game.Value.White, player);
                await dictNewGames.TryRemoveAsync(tx, gameId);
                await dictActiveGames.AddAsync(tx, gameId, activeGame);
                await playersClient.AddPlayerGameAsync(playerName, gameId);
                await tx.CommitAsync();
                return activeGame;
            }
        }

        public async Task<ChessGameState> ActiveGameStateAsync(string gameId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await GetActiveGameAsync(tx, gameId);
                return new ChessGameState(game);
            }
        }

        public async Task<ChessGameState> CompletedGameStateAsync(string gameId)
        {
            var dictGames = await GetCompletedGameDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await dictGames.TryGetValueAsync(tx, gameId);
                if (!game.HasValue)
                {
                    throw new ArgumentException("Game does not exist.");
                }
                return new ChessGameState(game.Value);
            }
        }

        public async Task<ChessGameState> NewGameStateAsync(string gameId)
        {
            var dictGames = await GetNewGameDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await dictGames.TryGetValueAsync(tx, gameId);
                if (!game.HasValue)
                {
                    throw new ArgumentException("Game does not exist.");
                }
                return new ChessGameState(game.Value);
            }
        }

        public async Task<List<string>> ListPieceMovesAsync(string gameId, string from)
        {
            Board board;
            using (var tx = StateManager.CreateTransaction())
            {
                board = await GetActiveGameBoardAsync(tx, gameId);
            }
            var moves = new List<string>();
            var fromField = ChessGameUtils.FieldFromString(from);
            var piece = board[fromField.Item1, fromField.Item2];
            foreach (var move in piece.GetPossibleMoves())
            {
                moves.Add(ChessGameUtils.FieldToString(move.Item1, move.Item2));
            }
            return moves;
        }

        public async Task<ChessGameState> MovePieceAsync(string gameId, string playerName, string from, string to)
        {
            ChessGameState newGameState = null;
            var dictActiveGames = await GetActiveGameDict();
            var dictCompletedGames = await GetCompletedGameDict();

            using (var tx = StateManager.CreateTransaction())
            {
                var game = await GetActiveGameAsync(tx, gameId);
                if (playerName != game.White.Name && playerName != game.Black.Name)
                {
                    throw new ArgumentException("Player not in the game.");
                }

                var board = new Board();
                board.PerformMoves(game.MoveHistory);
                var fromField = ChessGameUtils.FieldFromString(from);
                var toField = ChessGameUtils.FieldFromString(to);
                var piece = board[fromField.Item1, fromField.Item2];
                if (!piece.MoveTo(toField.Item1, toField.Item2))
                {
                    throw new ArgumentException("Illegal move.");
                }

                var newGameInfo = new ChessGameInfo(game.GameId, game.White, game.Black, board.ToMovesString());
                if (board.IsCheckmate || board.IsDraw)
                {
                    await dictActiveGames.TryRemoveAsync(tx, gameId);
                    await dictCompletedGames.TryAddAsync(tx, gameId, newGameInfo);
                }
                else
                {
                    await dictActiveGames.TryUpdateAsync(tx, gameId, newGameInfo, game);
                }

                await tx.CommitAsync();
                newGameState = new ChessGameState(newGameInfo);
            }

            var chessSignalRClient = proxyFactory.CreateServiceProxy<IChessFabrickSignalRService>(chessSignalRUri);
            await chessSignalRClient.PieceMovedAsync(playerName, from, to, newGameState);

            var actor = ActorProxy.Create<IChessFabrickActor>(new ActorId(gameId), chessActorUri);
            //actor.PerformMove().Start();
            //Task.Run(async () =>
            //{
            //    Thread.Sleep(5000);
            //    await actor.PerformMove();
            //}).Start();
            return newGameState;
        }

        public async Task<List<string>> NewGameIdsAsync()
        {
            var dictNewGames = await GetNewGameDict();
            var games = new List<string>();
            using (var tx = StateManager.CreateTransaction())
            {
                var keys = await dictNewGames.CreateKeyEnumerableAsync(tx);
                var enumerator = keys.GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync(CancellationToken.None))
                {
                    games.Add(enumerator.Current);
                }
            }
            return games;
        }

        public async Task<List<string>> ActiveGameIdsAsync()
        {
            var dictActiveGames = await GetActiveGameDict();
            var games = new List<string>();
            using (var tx = StateManager.CreateTransaction())
            {
                var keys = await dictActiveGames.CreateKeyEnumerableAsync(tx);
                var enumerator = keys.GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync(CancellationToken.None))
                {
                    games.Add(enumerator.Current);
                }
            }
            return games;
        }

        public async Task<List<string>> CompletedGameIdsAsync()
        {
            var dictCompletedGames = await GetCompletedGameDict();
            var games = new List<string>();
            using (var tx = StateManager.CreateTransaction())
            {
                var keys = await dictCompletedGames.CreateKeyEnumerableAsync(tx);
                var enumerator = keys.GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync(CancellationToken.None))
                {
                    games.Add(enumerator.Current);
                }
            }
            return games;
        }

        public async Task<ChessGameInfo> AddBot(string gameId)
        {
            var dictNewGames = await GetNewGameDict();
            var dictActiveGames = await GetActiveGameDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var player = new ChessPlayer(ChessFabrickUtils.BOT_NAME);
                var game = await dictNewGames.TryGetValueAsync(tx, gameId);
                if (!game.HasValue)
                {
                    throw new ArgumentException("Game does not exist.");
                }
                var activeGame = game.Value.White == null ?
                    new ChessGameInfo(game.Value.GameId, player, game.Value.Black) :
                    new ChessGameInfo(game.Value.GameId, game.Value.White, player);
                await dictNewGames.TryRemoveAsync(tx, gameId);
                await dictActiveGames.AddAsync(tx, gameId, activeGame);
                await tx.CommitAsync();
                return activeGame;
            }
        }
    }
}
