using ChessFabrickCommons.Services;
using ChessFabrickWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Hubs
{
    public class ChessGameHub : Hub
    {
        private readonly StatelessServiceContext serviceContext;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;

        public ChessGameHub(StatelessServiceContext context)
        {
            serviceContext = context;
            proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(serviceContext);
        }

        public override Task OnConnectedAsync()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetTest");
            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            string message = await chessClient.HelloChessAsync();
            await Clients.All.SendAsync("Test", message);
        }

        [Authorize]
        public async Task GetSecret()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetSecret: {Context.User.Identity.Name}");
            await Clients.All.SendAsync("Test", $"Name: {Context.User.Identity.Name}");
        }

        public async Task JoinGame(string gameIdentifier)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"JoinGame({gameIdentifier}): {Context.User.Identity.Name}");
            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            try
            {
                var gameId = long.Parse(gameIdentifier);
                var board = await chessClient.GameStateAsync(gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, ChessFabrickUtils.GameGroupName(gameId));
                await Clients.Caller.SendAsync("OnBoardChanged", board);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("OnError", ex.Message);
            }
        }

        [Authorize]
        public async Task LeaveGame(string gameIdentifier)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"LeaveGame({gameIdentifier}): {Context.User.Identity.Name}");
            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            try
            {
                var gameId = long.Parse(gameIdentifier);
                var board = await chessClient.GameStateAsync(gameId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, ChessFabrickUtils.GameGroupName(gameId));
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("OnError", ex.Message);
            }
        }

        [Authorize]
        public async Task GetPieceMoves(string gameIdentifier, string field)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetMoves({gameIdentifier}, {field}): {Context.User.Identity.Name}");
            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            try
            {
                var gameId = long.Parse(gameIdentifier);
                var moves = await chessClient.ListPieceMovesAsync(gameId, field);
                await Clients.Caller.SendAsync("ShowPieceMoves", field, moves);
            } catch (Exception ex)
            {
                await Clients.Caller.SendAsync("OnError", ex.Message);
            }
        }

        [Authorize]
        public async Task MovePiece(string gameIdentifier, string from, string to)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"MovePiece({gameIdentifier}, {from}, {to}): {Context.User.Identity.Name}");
            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            try
            {
                var gameId = long.Parse(gameIdentifier);
                var board = await chessClient.MovePieceAsync(Context.User.Identity.Name, gameId, from, to);
                await Clients.Users(board.GameInfo.White.Name, board.GameInfo.Black.Name).SendAsync("OnPieceMoved", from, to, board);
                await Clients.Group(ChessFabrickUtils.GameGroupName(gameId)).SendAsync("OnBoardChanged", board);
            } catch (Exception ex)
            {
                await Clients.Caller.SendAsync("OnError", ex.Message);
            }
        }
    }
}
