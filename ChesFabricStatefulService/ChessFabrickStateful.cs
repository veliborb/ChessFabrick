using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessCommons;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ChessFabrickStateful
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ChessFabrickStateful : StatefulService, IChessFabrickStatefulService
    {
        private static readonly string DICTIONARY_IDS = "dict_ids";
        private static readonly string ELEMENT_LAST_GAME_ID = "elem_last_game_id";
        private static readonly string ELEMENT_LAST_PLAYER_ID = "elem_last_player_id";
        private static readonly string DICTIONARY_PLAYERS = "dict_players";
        private static readonly string DICTIONARY_NEW_GAMES = "dict_new_games";
        private static readonly string DICTIONARY_ACTIVE_GAMES = "dict_active_games";
        private static readonly string DICTIONARY_COMPLETED_GAMES = "dict_completed_games";

        public ChessFabrickStateful(StatefulServiceContext context)
            : base(context)
        { }

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

        private async Task<ChessPlayer> GetPlayerAsync(ITransaction tx, long playerId)
        {
            var dictPlayers = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessPlayer>>(DICTIONARY_PLAYERS);
            var player = await dictPlayers.TryGetValueAsync(tx, playerId);
            if (!player.HasValue)
            {
                throw new ArgumentException("Player does not exist.");
            }
            return player.Value;
        }

        private async Task<ChessGameInfo> GetActiveGameAsync(ITransaction tx, long gameId)
        {
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_ACTIVE_GAMES);
            var game = await dictGames.TryGetValueAsync(tx, gameId);
            if (!game.HasValue)
            {
                throw new ArgumentException("Game does not exist.");
            }
            return game.Value;
        }

        private async Task<Board> GetActiveGameBoardAsync(ITransaction tx, long gameId)
        {
            var gameInfo = await GetActiveGameAsync(tx, gameId);
            var board = new Board();
            board.PerformMoves(gameInfo.MoveHistory);
            return board;
        }

        public Task<string> HelloChessAsync()
        {
            return Task.FromResult("Allo Chess!");
        }

        public async Task<ChessPlayer> NewPlayerAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Player name must not be empty");
            }

            var dictIds = await StateManager.GetOrAddAsync<IReliableDictionary<string, long>>(DICTIONARY_IDS);
            var dictPlayers = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessPlayer>>(DICTIONARY_PLAYERS);
            using (var tx = StateManager.CreateTransaction())
            {
                var playerId = await dictIds.AddOrUpdateAsync(tx, ELEMENT_LAST_PLAYER_ID, 1, (key, value) => ++value);
                var player = new ChessPlayer(playerId, name);
                await dictPlayers.AddAsync(tx, playerId, player);
                await tx.CommitAsync();
                return player;
            }
        }

        public async Task<ChessPlayer> PlayerInfoAsync(long playerId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                return await GetPlayerAsync(tx, playerId);
            }
        }

        public async Task<ChessGameInfo> NewGameAsync(long playerId, PieceColor playerColor)
        {
            var dictIds = await StateManager.GetOrAddAsync<IReliableDictionary<string, long>>(DICTIONARY_IDS);
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_NEW_GAMES);
            using (var tx = StateManager.CreateTransaction())
            {
                var player = await GetPlayerAsync(tx, playerId);
                var gameId = await dictIds.AddOrUpdateAsync(tx, ELEMENT_LAST_GAME_ID, 1, (key, value) => ++value);
                var game = playerColor == PieceColor.White ?
                    new ChessGameInfo(gameId, player, null) :
                    new ChessGameInfo(gameId, null, player);
                await dictGames.AddAsync(tx, gameId, game);
                await tx.CommitAsync();
                return game;
            }
        }

        public async Task<ChessGameInfo> JoinGameAsync(long playerId, long gameId)
        {
            var dictNewGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_NEW_GAMES);
            var dictActiveGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_ACTIVE_GAMES);
            using (var tx = StateManager.CreateTransaction())
            {
                var player = await GetPlayerAsync(tx, playerId);
                var game = await dictNewGames.TryGetValueAsync(tx, gameId);
                if (!game.HasValue)
                {
                    throw new ArgumentException("Game does not exist.");
                }
                if ((game.Value.White ?? game.Value.Black).Id == playerId)
                {
                    throw new ArgumentException("Can't play against yourself");
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

        public async Task<ChessGameState> GameStateAsync(long gameId)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await GetActiveGameAsync(tx, gameId);
                return new ChessGameState(game);
            }
        }

        public async Task<List<string>> ListPieceMovesAsync(long gameId, string from)
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

        public async Task<ChessGameState> MovePieceAsync(long playerId, long gameId, string from, string to)
        {
            var dictActiveGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_ACTIVE_GAMES);
            var dictCompletedGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, ChessGameInfo>>(DICTIONARY_COMPLETED_GAMES);
            using (var tx = StateManager.CreateTransaction())
            {
                var game = await GetActiveGameAsync(tx, gameId);
                if (playerId != game.White.Id && playerId != game.Black.Id)
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

                var newGameState = new ChessGameInfo(game.GameId, game.White, game.Black, board.ToMovesString());
                if (board.IsCheckmate || board.IsDraw)
                {
                    await dictActiveGames.TryRemoveAsync(tx, gameId);
                    await dictCompletedGames.TryAddAsync(tx, gameId, newGameState);
                }
                else
                {
                    await dictActiveGames.TryUpdateAsync(tx, gameId, newGameState, game);
                }
                await tx.CommitAsync();
                return new ChessGameState(newGameState);
            }
        }
    }
}
