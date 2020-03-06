using ChessFabrickCommons.Entities;
using Microsoft.ServiceFabric.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickCommons.Utils
{
    public static class ChessFabrickUtils
    {
        public static readonly int PARTITION_COUNT = 5;
        public static readonly string BOT_NAME = "-BOT-";

        public static ServicePartitionKey NamePartitionKey(string name) => new ServicePartitionKey(
            name[name.Length - 1] % PARTITION_COUNT
        );

        public static ServicePartitionKey GuidPartitionKey(string guid) => new ServicePartitionKey(
            long.Parse(guid.Substring(guid.LastIndexOf('-') + 1), System.Globalization.NumberStyles.HexNumber) % PARTITION_COUNT
        );

        public static string GameGroupName(string gameId) => $"game-{gameId}";

        public static bool IsBot(this ChessPlayer player) => player?.Name == BOT_NAME;
    }
}
