﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Client;
using ChessFabrickCommons.Services;
using System.Fabric;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using ChessFabrickCommons.Models;
using ChessFabrickCommons.Utils;

namespace ChessFabrickCommons.Services
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string userName, string password);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings appSettings;
        private readonly ServiceProxyFactory proxyFactory;
        private readonly Uri userServiceUri;

        public UserService(IOptions<AppSettings> appSettings, StatelessServiceContext context)
        {
            this.appSettings = appSettings.Value;
            this.proxyFactory = new ServiceProxyFactory((c) =>
            {
                return new FabricTransportServiceRemotingClientFactory();
            });
            this.userServiceUri = new Uri($"{context.CodePackageActivationContext.ApplicationName}/ChessFabrickPlayersStateful");
        }

        public async Task<UserModel> Authenticate(string userName, string password)
        {
            var userClient = proxyFactory.CreateServiceProxy<IChessFabrickPlayersStatefulService>(userServiceUri, ChessFabrickUtils.NamePartitionKey(userName));
            var player = await userClient.PlayerInfoAsync(userName);

            if (player == null)
            {
                throw new ArgumentException("Authentication failed.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, player.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = new UserModel { Player = player, Token = tokenHandler.WriteToken(token) };
            return user;
        }
    }
}
