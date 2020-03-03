using System;
using System.Fabric;
using System.Threading.Tasks;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Utils;
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
        private readonly Uri playerServiceUri;
        private readonly IUserService userService;

        public ChessPlayerController(StatelessServiceContext context, IUserService userService)
        {
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.playerServiceUri = ChessFabrickWeb.GetChessFabrickPlayersStatefulName(context);
            this.userService = userService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> PostNewPlayer([FromBody] AuthenticationModel model)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"PostNewPlayer({model.Name})");

            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(model.Name));
            var player = await playersClient.NewPlayerAsync(model.Name);
            var user = await userService.Authenticate(model.Name, model.Password);

            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel model)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"Authenticate({model.Name})");

            var user = await userService.Authenticate(model.Name, model.Password);

            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCurrent()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetCurrent()");

            var userClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(User.Identity.Name));
            var player = await userClient.PlayerInfoAsync(User.Identity.Name);

            return Ok(player);
        }

        [Authorize]
        [HttpGet("{playerName}")]
        public async Task<IActionResult> GetPlayer(string playerName)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetPlayer({playerName})");

            var userClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(playerName));
            var player = await userClient.PlayerInfoAsync(playerName);

            return Ok(player);
        }
    }
}
