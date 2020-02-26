using ChessFabrickCommons.Services;
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
    public class ChatHub : Hub
    {
        private readonly StatelessServiceContext context;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri chessStatefulUri;

        public ChatHub(StatelessServiceContext context)
        {
            this.context = context;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.chessStatefulUri = ChessFabrickWeb.GetChessFabrickStatefulServiceName(this.context);
        }

        public async Task GetTest()
        {
            ServiceEventSource.Current.ServiceMessage(this.context, $"Message: GetTest");
            IChessFabrickStatefulService helloWorldClient = proxyFactory.CreateServiceProxy<IChessFabrickStatefulService>(chessStatefulUri, new ServicePartitionKey(1));
            string message = await helloWorldClient.HelloChessAsync();
            await Clients.All.SendAsync("ReceiveMessage", "system", message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
