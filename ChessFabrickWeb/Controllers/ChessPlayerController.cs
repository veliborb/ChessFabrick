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
using ChessFabrickWeb.Models;
using ChessFabrickWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Newtonsoft.Json;

namespace ChessFabrickWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/player")]
    public class ChessPlayerController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;
        private readonly StatelessServiceContext context;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;
        private readonly IUserService userService;

        public ChessPlayerController(HttpClient httpClient, StatelessServiceContext context, FabricClient fabricClient, IUserService userService)
        {
            this.fabricClient = fabricClient;
            this.httpClient = httpClient;
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
            this.userService = userService;
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

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer(long playerId)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: GetPlayer {playerId}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.PlayerInfoAsync(playerId);

            return Json(result);
        }

        [Authorize]
        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            return Ok(User.Identity.Name);
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            return Ok(users);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = userService.Authenticate(model.Id);

            if (user == null)
                return BadRequest(new { message = "Id is incorrect." });

            return Ok(user);
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostNewPlayer([FromBody] NewPlayerModel model)
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: PostNewPlayer {model.Name}");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var result = await chessClient.NewPlayerAsync(model.Name);

            return Json(result);
        }
    }
}
