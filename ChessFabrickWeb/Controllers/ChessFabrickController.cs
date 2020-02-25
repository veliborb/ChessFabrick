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
using Microsoft.AspNetCore.Mvc;
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

        public ChessFabrickController(HttpClient httpClient, StatelessServiceContext context, FabricClient fabricClient)
        {
            this.fabricClient = fabricClient;
            this.httpClient = httpClient;
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
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
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: GetTest");

            Uri chessServiceUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
            Uri proxyAddress = GetProxyAddress(chessServiceUri);

            ServiceEventSource.Current.ServiceMessage(context, $"chessServiceUri: {chessServiceUri}; proxyAddress: {proxyAddress}");

            IChessFabrickStatefulService helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessServiceUri, new ServicePartitionKey(1));
            string message = await helloWorldClient.HelloChessAsync();

            return Json(message);
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostNewGame()
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: PostNewGame");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.NewGameAsync();

            return Json(result);
        }

        [HttpGet("game/{gameId}")]
        public async Task<IActionResult> GetGameInfo(long gameId)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: GetGameInfo {gameId}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.GameInfoAsync(gameId);

            return Json(result);
        }

        [HttpGet("game/{gameId}/{field}")]
        public async Task<IActionResult> GetMoves(long gameId, string field)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: GetMoves {gameId}, {field}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.ListPieceMovesAsync(gameId, field);

            return Json(result);
        }

        [HttpPost("game/{gameId}")]
        public async Task<IActionResult> PostMove(long gameId, [FromBody] ChessMove move)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: PostMove {gameId}, {move.From}, {move.To}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.MovePieceAsync(gameId, move.From, move.To);

            return Json(result);
        }
    }
}
