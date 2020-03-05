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
using System.Net.Http;
using Microsoft.Extensions.Configuration.Json;
using ChessFabrickCommons.Services;
using ChessFabrickCommons.Models;
using Microsoft.AspNetCore.SignalR;
using ChessFabrickWeb.Hubs;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;

namespace ChessFabrickWeb
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class ChessFabrickWeb : StatelessService, IChessFabrickSignalRService
    {
        private readonly IHubContext<ChessGameHub> gameHubContext;

        public ChessFabrickWeb(StatelessServiceContext context)
            : base(context)
        { }

        internal static Uri GetChessFabrickSignalRServiceName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickWeb");
        }

        internal static Uri GetChessFabrickStatefulServiceName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickStateful");
        }

        internal static Uri GetChessFabrickPlayersStatefulName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickPlayersStateful");
        }

        internal static Uri GetChessFabrickActorName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickActorService");
        }

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
                        return new FabricTransportServiceRemotingListener(serviceContext, this, new FabricTransportRemotingListenerSettings() { EndpointResourceName = "ServiceEndpointV2" });
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
                                            .AddSingleton<HttpClient>(new HttpClient())
                                            .AddSingleton<FabricClient>(new FabricClient())
                                            .AddSingleton<StatelessServiceContext>(serviceContext))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .Build();

                        return webHost;
                    }), "KestrelListener")
            };
        }

        public async Task<string> BoardUpdatedAsync(ChessGameState gameState)
        {
            return "Radim!";
        }
    }
}
