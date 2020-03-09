using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Models;
using Microsoft.AspNetCore.SignalR;
using ChessFabrickCommons.Utils;
using ChessFabrickSignaler.Hubs;
using ChessFabrickCommons.Entities;
using Microsoft.Extensions.Configuration.Json;

namespace ChessFabrickSignaler
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class ChessFabrickSignaler : StatelessService, IChessFabrickSignalRService
    {
        private IHubContext<ChessGameHub> gameHubContext;

        public ChessFabrickSignaler(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener((serviceContext) =>
                    {
                        return new FabricTransportServiceRemotingListener(serviceContext, this,
                            new FabricTransportRemotingListenerSettings() { EndpointResourceName = "ServiceEndpointV2" });
                    }, "RemotingListener"),
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");
                        var webHost = new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureAppConfiguration((builderContext, config) =>
                                    {
                                        config.Add(new JsonConfigurationSource { Path = "appsettings.json",  ReloadOnChange = true });
                                    })
                                    .ConfigureServices(
                                        services => services
                                            .AddSingleton<FabricClient>(new FabricClient())
                                            .AddSingleton<StatelessServiceContext>(serviceContext))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .Build();
                        gameHubContext = webHost.Services.GetService<IHubContext<ChessGameHub>>();
                        return webHost;
                    }), "KestrelListener")
            };
        }

        public async Task GameCreated(ChessGameInfo game)
        {
            await gameHubContext.Clients.All.SendAsync("OnGameCreated", game);
        }

        public async Task PlayerJoined(string playerName, ChessGameInfo game)
        {
            await gameHubContext.Clients.Group(ChessFabrickUtils.GameGroupName(game.GameId)).SendAsync("OnPlayerJoined", game, playerName);
        }

        public async Task PieceMovedAsync(string playerName, string from, string to, ChessGameState gameState)
        {
            await gameHubContext.Clients.User(gameState.GameInfo.White.Name == playerName ? gameState.GameInfo.Black.Name : gameState.GameInfo.White.Name)
                .SendAsync("OnPieceMoved", from, to, gameState);
            await gameHubContext.Clients.Group(ChessFabrickUtils.GameGroupName(gameState.GameInfo.GameId)).SendAsync("OnBoardChanged", gameState);
        }
    }
}
