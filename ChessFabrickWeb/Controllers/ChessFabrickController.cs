﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.Net.Http;
using System.Threading.Tasks;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Services;
using ChessFabrickWeb.Hubs;
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
    [Route("api/game")]
    public class ChessFabrickController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;
        private readonly StatelessServiceContext context;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;
        private readonly Uri playerServiceUri;
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
            this.playerServiceUri = ChessFabrickWeb.GetChessFabrickPlayersStatefulName(context);
            this.gameHubContext = gameHubContext;
        }

        [HttpGet]
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

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameState(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetGameState({gameId})");
            var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
            try
            {
                var board = await chessClient.ActiveGameStateAsync(gameId);
                return Ok(board);
            }
            catch (Exception ex)
            {
                try
                {
                    var board = await chessClient.CompletedGameStateAsync(gameId);
                    return Ok(board);
                }
                catch (Exception ex1)
                {
                    return StatusCode(500, ex1.Message);
                }
            }
        }

        [HttpGet("new")]
        public async Task<IActionResult> GetNewGames()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetNewGames()");
            var gameIds = new List<string>();
            var partitions = await fabricClient.QueryManager.GetPartitionListAsync(chessStatefulUri);
            foreach (var partition in partitions)
            {
                var partitionInfo = (Int64RangePartitionInformation)partition.PartitionInformation;
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(partitionInfo.LowKey));
                var games = await chessClient.NewGameIdsAsync();
                gameIds.AddRange(games);
            }
            return Ok(gameIds);
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

        [Authorize]
        [HttpPost("new/{gameId}/join")]
        public async Task<IActionResult> PostJoinGame(string gameId)
        {
            ServiceEventSource.Current.ServiceMessage(context, $"PostJoinGame({gameId}): {User.Identity.Name}");
            try
            {
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, ChessFabrickUtils.GuidPartitionKey(gameId));
                var game = await chessClient.JoinGameAsync(gameId, User.Identity.Name);
                await gameHubContext.Clients.Group(ChessFabrickUtils.GameGroupName(gameId)).SendAsync("OnPlayerJoined", game, User.Identity.Name);
                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedGames()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetCompletedGames()");
            var gameIds = new List<string>();
            var partitions = await fabricClient.QueryManager.GetPartitionListAsync(chessStatefulUri);
            foreach (var partition in partitions)
            {
                var partitionInfo = (Int64RangePartitionInformation)partition.PartitionInformation;
                var chessClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(partitionInfo.LowKey));
                var games = await chessClient.CompletedGameIdsAsync();
                gameIds.AddRange(games);
            }
            return Ok(gameIds);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetPlayerGames()
        {
            ServiceEventSource.Current.ServiceMessage(context, $"GetPlayerGames()");
            try
            {
                var playerClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(playerServiceUri, ChessFabrickUtils.NamePartitionKey(User.Identity.Name));
                var games = await playerClient.PlayerGamesAsync(User.Identity.Name);
                return Ok(games);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.ServiceMessage(context, ex.ToString()); ;
                return StatusCode(500, ex.Message);
            }
        }
    }
}
