using System;
using System.Collections.Generic;
using System.Fabric;
using System.Net.Http;
using System.Threading.Tasks;
using ChessFabrickCommons.Services;
using ChessFabrickWeb.Hubs;
using ChessFabrickWeb.Models;
using ChessFabrickWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace ChessFabrickWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/chess")]
    public class ChessFabrickController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;
        private readonly StatelessServiceContext context;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;
        private readonly IHubContext<ChessGameHub> gameHubContext;

        public ChessFabrickController(HttpClient httpClient, StatelessServiceContext context, FabricClient fabricClient, IHubContext<ChessGameHub> gameHubContext)
        {
            this.fabricClient = fabricClient;
            this.httpClient = httpClient;
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(context);
            this.gameHubContext = gameHubContext;
        }

        /// <summary>
        /// Constructs a reverse proxy URL for a given service.
        /// Example: http://localhost:19081/ChessFabrickApp/ChessFabrickStateful"
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private Uri GetProxyAddress(Uri serviceName)
        {
            return new Uri($"http://localhost:19081{serviceName.AbsolutePath}");
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetTest()");

            Uri chessServiceUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(context);
            Uri proxyAddress = GetProxyAddress(chessServiceUri);

            var partitions = await fabricClient.QueryManager.GetPartitionListAsync(chessStatefulUri);
            foreach (var partition in partitions)
            {
                var partitionInfo = (Int64RangePartitionInformation)partition.PartitionInformation;
                ServiceEventSource.Current.ServiceMessage(context, $"Partition {partitionInfo.Id}, {partitionInfo.LowKey}, {partitionInfo.HighKey}, {partitionInfo.Kind}");
            }

            ServiceEventSource.Current.ServiceMessage(context, $"chessServiceUri: {chessServiceUri}; proxyAddress: {proxyAddress}, partitions: {partitions.Count}");

            var helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessServiceUri, new ServicePartitionKey(new Random().Next(0, 4)));
            string message = await helloWorldClient.HelloChessAsync();

            return Ok(message);
        }

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> PostNewGame([FromBody] NewGameModel model)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"PostNewGame({model.PlayerColor}): {User.Identity.Name}");
            try
            {
                var gameId = Guid.NewGuid().ToString();
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var game = await chessClient.NewGameAsync(gameId, User.Identity.Name, model.PlayerColor);
                await gameHubContext.Clients.All.SendAsync("OnGameCreated", game);
                return Ok(game);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("game/active")]
        public async Task<IActionResult> GetActiveGames()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetActiveGames()");
            var gameIds = new List<string>();
            var partitions = await fabricClient.QueryManager.GetPartitionListAsync(chessStatefulUri);
            foreach (var partition in partitions)
            {
                var partitionInfo = (Int64RangePartitionInformation)partition.PartitionInformation;
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(partitionInfo.LowKey));
                var games = await chessClient.ActiveGameIdsAsync();
                gameIds.AddRange(games);
            }
            return Ok(gameIds);
        }

        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetGameState(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetGameState({gameId})");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var board = await chessClient.GameStateAsync(gameId);
                return Ok(board);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("game/{gameId}/join")]
        public async Task<IActionResult> PostJoinGame(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"PostJoinGame({gameId}): {User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var game = await chessClient.JoinGameAsync(gameId, User.Identity.Name);
                await gameHubContext.Clients.Group(ChessFabrickUtils.GameGroupName(gameId)).SendAsync("OnPlayerJoined", game, User.Identity.Name);
                return Ok(game);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
