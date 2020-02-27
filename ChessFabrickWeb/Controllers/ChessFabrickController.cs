using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ChessFabrickCommons;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickWeb.Hubs;
using ChessFabrickWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Newtonsoft.Json;

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
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
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

        [HttpGet("")]
        public async Task<IActionResult> GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"GetTest");

            Uri chessServiceUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
            Uri proxyAddress = GetProxyAddress(chessServiceUri);

            ServiceEventSource.Current.ServiceMessage(context, $"chessServiceUri: {chessServiceUri}; proxyAddress: {proxyAddress}");

            IChessFabrickStatefulService helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessServiceUri, new ServicePartitionKey(1));
            string message = await helloWorldClient.HelloChessAsync();

            return Json(message);
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostNewGame([FromBody] NewGameModel model)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"PostNewGame {model.PlayerId}, {model.PlayerColor}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.NewGameAsync(model.PlayerId, model.PlayerColor);
            await gameHubContext.Clients.All.SendAsync("GameCreated", result);

            return Json(result);
        }

        [HttpPost("game/{gameId}/join")]
        public async Task<IActionResult> PostJoinGame(long gameId, [FromBody] NewGameModel model)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"PostJoinGame {gameId}, {model.PlayerId}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.JoinGameAsync(model.PlayerId, gameId);
            await gameHubContext.Clients.All.SendAsync("PlayerJoined", gameId, model.PlayerId);

            return Json(result);
        }

        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetGameState(long gameId)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"GetGameState {gameId}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.GameStateAsync(gameId);

            return Json(result);
        }
    }
}
