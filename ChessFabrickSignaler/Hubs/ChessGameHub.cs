using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessFabrickSignaler.Hubs
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
            chessStatefulUri = new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickStateful");
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

        public async Task<string> GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetTest()");
            var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(new Random().Next(0, 4)));
            string message = await chessClient.HelloChessAsync();
            await Clients.All.SendAsync("Test", message);
            return "Test";
        }

        [Authorize]
        public async Task<string> GetSecret()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetSecret(): {Context.User.Identity.Name}");
            await Clients.All.SendAsync("Test", $"Name: {Context.User.Identity.Name}");
            return "Secret";
        }

        public async Task<ChessGameState> SpectateGame(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"SpectateGame({gameId}): {Context.User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var board = await chessClient.ActiveGameStateAsync(gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, ChessFabrickUtils.GameGroupName(gameId));
                return board;
            }
            catch (Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }

        public async Task UnspectateGame(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"UnspectateGame({gameId}): {Context.User.Identity.Name}");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ChessFabrickUtils.GameGroupName(gameId));
        }

        [Authorize]
        public async Task<List<string>> GetPieceMoves(string gameId, string field)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetPieceMoves({gameId}, {field}): {Context.User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var moves = await chessClient.ListPieceMovesAsync(gameId, field);
                return moves;
            } catch (Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }

        [Authorize]
        public async Task<ChessGameState> MovePiece(string gameId, string from, string to)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"MovePiece({gameId}, {from}, {to}): {Context.User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var board = await chessClient.MovePieceAsync(gameId, Context.User.Identity.Name, from, to);
                return board;
            }
            catch (Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }

        [Authorize]
        public async Task<ChessGameState> JoinGame(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"JoinGame({gameId}): {Context.User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var board = await chessClient.ActiveGameStateAsync(gameId);
                if (board.GameInfo.White.Name != Context.User.Identity.Name && board.GameInfo.Black.Name != Context.User.Identity.Name)
                {
                    throw new ArgumentException("Player not in the game.");
                }
                await Clients.User(board.GameInfo.White.Name == Context.User.Identity.Name ? board.GameInfo.Black.Name : board.GameInfo.White.Name)
                    .SendAsync("OnPlayerJoined", board);
                return board;
            }
            catch (Exception ex)
            {
                throw new HubException(ex.Message);
            }
        }
    }
}
