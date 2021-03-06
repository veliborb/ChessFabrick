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

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, $"Actor {gameId} activated.");
            return Task.CompletedTask;
        }

        public async Task PerformMove()
        {
            ActorEventSource.Current.ActorMessage(this, $"PerformMove({gameId})");
            await Task.Delay(rand.Next(800, 1600));

            var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
            var game = await chessClient.ActiveGameStateAsync(gameId);
            while (true)
            {
                try
                {
                    var board = new Board();
                    board.PerformMoves(game.GameInfo.MoveHistory);
                    var move = GetRandomMove(board);
                    await chessClient.MovePieceAsync(gameId, ChessFabrickUtils.BOT_NAME, move.Item1, move.Item2);
                    return;
                }
                catch (Exception ex)
                {
                    ActorEventSource.Current.ActorMessage(this, $"PerformMoveException({gameId}): \n{ex}");
                }
            }
        }

        private Tuple<string, string> GetRandomMove(Board board)
        {
            var pieces = board.GetAlive(board.TurnColor);
            while (true)
            {
                var piece = pieces[rand.Next(0, pieces.Count)];
                var x = piece.X;
                var y = piece.Y;
                var moves = piece.GetPossibleMoves();
                while (moves.Count > 0)
                {
                    var move = moves[rand.Next(0, moves.Count)];
                    if (piece.MoveTo(move.Item1, move.Item2))
                    {
                        return Tuple.Create(ChessGameUtils.FieldToString(x, y), ChessGameUtils.FieldToString(move.Item1, move.Item2));
                    }
                    moves.Remove(move);
                }
                pieces.Remove(piece);
            }
        }
    }
}
