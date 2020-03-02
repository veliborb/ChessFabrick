using System;
using System.Fabric;
using System.Threading.Tasks;
using ChessFabrickCommons.Services;
using ChessFabrickWeb.Models;
using ChessFabrickWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace ChessFabrickWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/player")]
    public class ChessPlayerController : Controller
    {
        private readonly StatelessServiceContext context;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;
        private readonly IUserService userService;

        public ChessPlayerController(StatelessServiceContext context, IUserService userService)
        {
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(context);
            this.userService = userService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostNewPlayer([FromBody] NewPlayerModel model)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"PostNewPlayer({model.Name})");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var player = await chessClient.NewPlayerAsync(model.Name);
            var user = await userService.Authenticate(player.Id);

            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel model)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"Authenticate({model.Id})");

            var user = await userService.Authenticate(model.Id);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetCurrent()");

            var userId = long.Parse(User.Identity.Name);

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var player = await chessClient.PlayerInfoAsync(userId);

            return Ok(player);
        }

        [Authorize]
        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer(long playerId)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetPlayer({playerId})");

            IChessFabrickStatefulService chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            var player = await chessClient.PlayerInfoAsync(playerId);

            return Ok(player);
        }
    }
}
