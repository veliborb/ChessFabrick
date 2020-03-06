using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Utils;
using ChessFabrickWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

namespace ChessFabrickWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/player")]
    public class ChessPlayerController : Controller
    {
        private readonly StatelessServiceContext context;
        private readonly FabricClient fabricClient;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri playerServiceUri;
        private readonly IUserService userService;

        public ChessPlayerController(StatelessServiceContext context, FabricClient fabricClient, IUserService userService)
        {
            this.context = context;
            this.fabricClient = fabricClient;
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
            ServiceEventSource.Current.ServiceMessage(context, $"PostNewPlayer({model.Name}, {model.Password})");

            if (string.IsNullOrEmpty(model.Name) || model.Name.Length > 30)
            {
                return BadRequest("Username must not be empty or longer than 30 characters.");
            }
            if (!Regex.IsMatch(model.Name, "^[a-zA-Z0-9_]+$"))
            {
                return BadRequest("Username can contain only letters, numbers and underscore.");
            }
            //if (!Regex.IsMatch(model.Password, "([a-zA-Z0-9_]+)"))
            //{
            //    return BadRequest("Username can contain only letters, numbers and underscore.");
            //}

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

            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(User.Identity.Name));
            var player = await playersClient.PlayerInfoAsync(User.Identity.Name);

            return Ok(player);
        }

        [Authorize]
        [HttpGet("{playerName}")]
        public async Task<IActionResult> GetPlayer(string playerName)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetPlayer({playerName})");

            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(playerName));
            var player = await playersClient.PlayerInfoAsync(playerName);

            return Ok(player);
        }

        [Authorize]
        [HttpGet("{playerName}/games")]
        public async Task<IActionResult> GetPlayerGames(string playerName)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetPlayerGames({playerName})");

            var playersClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(playerName));
            var games = await playersClient.PlayerGamesAsync(playerName);

            return Ok(games);
        }
    }
}
