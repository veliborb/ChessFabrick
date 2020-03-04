using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChessFabrickCommons.Entities;
using ChessFabrickCommons.Services;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace ChessFabrickPlayersStateful
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ChessFabrickPlayersStateful : StatefulService, IChessFabrickPlayersStatefulService
    {
        public ChessFabrickPlayersStateful(StatefulServiceContext context)
            : base(context)
        { }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                 new ServiceReplicaListener((c) =>
                 {
                     return new FabricTransportServiceRemotingListener(c, this);
                 })
             };
        }

        private Task<IReliableDictionary2<string, ChessPlayer>> GetPlayerDict()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary2<string, ChessPlayer>>("dict_players");
        }

        private Task<IReliableDictionary2<string, List<string>>> GetPlayerGamesDict()
        {
            return StateManager.GetOrAddAsync<IReliableDictionary2<string, List<string>>>("dict_player_games");
        }

        private async Task<ChessPlayer> GetPlayerAsync(ITransaction tx, string playerName)
        {
            var dictPlayers = await GetPlayerDict();
            var player = await dictPlayers.TryGetValueAsync(tx, playerName);
            if (!player.HasValue)
            {
                throw new ArgumentException("Player does not exist.");
            }
            return player.Value;
        }

        public async Task<ChessPlayer> NewPlayerAsync(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                throw new ArgumentException("Player name must not be empty");
            }

            var dictPlayers = await GetPlayerDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var player = new ChessPlayer(playerName);
                await dictPlayers.AddAsync(tx, playerName, player);
                await tx.CommitAsync();
                return player;
            }
        }

        public async Task<ChessPlayer> PlayerInfoAsync(string playerName)
        {
            using (var tx = StateManager.CreateTransaction())
            {
                return await GetPlayerAsync(tx, playerName);
            }
        }

        public async Task<List<string>> PlayerGamesAsync(string playerName)
        {
            var dictPlayerGames = await GetPlayerGamesDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var games = await dictPlayerGames.TryGetValueAsync(tx, playerName);
                if (games.HasValue)
                {
                    return games.Value;
                }
                return new List<string>();
            }
        }

        public async Task AddPlayerGameAsync(string playerName, string gameId)
        {
            var dictPlayerGames = await GetPlayerGamesDict();
            using (var tx = StateManager.CreateTransaction())
            {
                var games = await dictPlayerGames.AddOrUpdateAsync(tx, playerName,
                    new string[] { gameId }.ToList(),
                    (key, value) => {
                        var list = value.ToList();
                        list.Add(gameId);
                        return list;
                    });
                await tx.CommitAsync();
            }
        }
    }
}
