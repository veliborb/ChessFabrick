using ChessFabrickCommons.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Hubs
{
    public class ChessGameHub : Hub
    {
        private readonly StatelessServiceContext serviceContext;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;

        public ChessGameHub(StatelessServiceContext context)
        {
            serviceContext = context;
            proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(serviceContext);
        }

        public override Task OnConnectedAsync()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetTest");
            IChessFabrickStatefulService helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            string message = await helloWorldClient.HelloChessAsync();
            await Clients.All.SendAsync("Test", message);
        }

        [Authorize]
        public async Task GetSecret()
        {
            ServiceEventSource.Current.ServiceMessage(serviceContext, $"GetSecret");
            IChessFabrickStatefulService helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            string message = await helloWorldClient.HelloChessAsync();
            await Clients.All.SendAsync("Test", $"Secret: {message}");
        }
    }
}
