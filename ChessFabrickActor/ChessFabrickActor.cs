using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ChessFabrickCommons.Actors;
using ChessFabrickCommons.Models;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using ChessFabrickCommons.Utils;
using ChessFabrickCommons.Services;
using ChessCommons;

namespace ChessFabrickActor
{
    [StatePersistence(StatePersistence.None)]
    internal class ChessFabrickActor : Actor, IChessFabrickActor
    {
        private readonly string gameId;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;
        private readonly Random rand;

        public ChessFabrickActor(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
            this.gameId = actorId.ToString();
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = new Uri($"{actorService.Context.CodePackageActivationContext.ApplicationName}/ChessFabrickStateful");
            this.rand = new Random();
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, $"Actor {gameId} activated.");
            return Task.CompletedTask;
        }

        public async Task PerformMove()
        {
            ActorEventSource.Current.ActorMessage(this, $"PerformMove({gameId})");
            var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
            var game = await chessClient.ActiveGameStateAsync(gameId);
            ActorEventSource.Current.ActorMessage(this, $"MoveHistory: {game.GameInfo.MoveHistory}");
            var board = new Board();
            try
            {
                board.PerformMoves(game.GameInfo.MoveHistory);
            } catch (Exception) { }
            var move = GetRandomMove(board);
            await chessClient.MovePieceAsync(gameId, ChessFabrickUtils.BOT_NAME, move.Item1, move.Item2);
        }

        private Tuple<string, string> GetRandomMove(Board board)
        {
            var pieces = board.GetAlive(board.TurnColor);
            while (true)
            {
                var piece = pieces[rand.Next(0, pieces.Count - 1)];
                var moves = piece.GetPossibleMoves();
                if (moves.Count > 0)
                {
                    var move = moves[rand.Next(0, moves.Count - 1)];
                    return Tuple.Create(ChessGameUtils.FieldToString(piece.X, piece.Y), ChessGameUtils.FieldToString(move.Item1, move.Item2));
                }
            }
        }
    }
}
