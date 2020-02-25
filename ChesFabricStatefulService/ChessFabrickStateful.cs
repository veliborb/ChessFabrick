using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessCommons;
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
        private static readonly string ELEMENT_LAST_USER_ID = "elem_last_user_id";
        private static readonly string DICTIONARY_GAMES = "dict_games";

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

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            var myDictionary = await StateManager.GetOrAddAsync<IReliableDictionary<string, long>>("myDictionary");

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using (var tx = StateManager.CreateTransaction())
                {
                    var result = await myDictionary.TryGetValueAsync(tx, "Counter");

                    ServiceEventSource.Current.ServiceMessage(Context, "Current Counter Value: {0}",
                        result.HasValue ? result.Value.ToString() : "Value does not exist.");

                    await myDictionary.AddOrUpdateAsync(tx, "Counter", 0, (key, value) => ++value);

                    // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                    // discarded, and nothing is saved to the secondary replicas.
                    await tx.CommitAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

        public Task<string> HelloChessAsync()
        {
            return Task.FromResult("Allo Chess!");
        }

        public async Task<ChessGame> NewGameAsync()
        {
            var dictIds = await StateManager.GetOrAddAsync<IReliableDictionary<string, long>>(DICTIONARY_IDS);
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, string>>(DICTIONARY_GAMES);

            ChessGame game;
            using (var tx = StateManager.CreateTransaction())
            {
                var gameId = await dictIds.AddOrUpdateAsync(tx, ELEMENT_LAST_GAME_ID, 1, (key, value) => ++value);
                await dictGames.AddAsync(tx, gameId, "");

                game = new ChessGame(gameId);

                ServiceEventSource.Current.ServiceMessage(Context, $"Starting NewGame with GameId: {game.GameId}");

                await tx.CommitAsync();
            }

            return game;
        }

        public async Task<ChessGame> GameInfoAsync(long gameId)
        {
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, string>>(DICTIONARY_GAMES);
            Board board = new Board();
            using (var tx = StateManager.CreateTransaction())
            {
                var gameString = await dictGames.TryGetValueAsync(tx, gameId);

                ServiceEventSource.Current.ServiceMessage(Context, $"GameId: {gameId}; GameString: {(gameString.HasValue ? gameString.Value : "null")}");

                if (gameString.HasValue)
                {
                    board.PerformMoves(gameString.Value);
                }
            }
            return new ChessGame(gameId, board);
        }

        public async Task<List<string>> ListPieceMovesAsync(long gameId, string from)
        {
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, string>>(DICTIONARY_GAMES);
            Board board = new Board();
            using (var tx = StateManager.CreateTransaction())
            {
                var gameString = await dictGames.TryGetValueAsync(tx, gameId);

                ServiceEventSource.Current.ServiceMessage(Context, $"GameId: {gameId}; GameString: {(gameString.HasValue ? gameString.Value : "null")}");

                if (gameString.HasValue)
                {
                    board.PerformMoves(gameString.Value);
                }
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

        public async Task<ChessGame> MovePieceAsync(long gameId, string from, string to)
        {
            var dictGames = await StateManager.GetOrAddAsync<IReliableDictionary<long, string>>(DICTIONARY_GAMES);
            Board board = new Board();
            using (var tx = StateManager.CreateTransaction())
            {
                var gameString = await dictGames.TryGetValueAsync(tx, gameId);

                ServiceEventSource.Current.ServiceMessage(Context, $"GameId: {gameId}; GameString: {(gameString.HasValue ? gameString.Value : "null")}");

                if (gameString.HasValue)
                {
                    board.PerformMoves(gameString.Value);
                }

                var fromField = ChessGameUtils.FieldFromString(from);
                var piece = board[fromField.Item1, fromField.Item2];
                var toField = ChessGameUtils.FieldFromString(to);

                if (piece.MoveTo(toField.Item1, toField.Item2))
                {
                    var result = await dictGames.TryUpdateAsync(tx, gameId, board.ToMovesString(), gameString.Value);
                    await tx.CommitAsync();
                }
            }
            return new ChessGame(gameId, board);
        }
    }
}
