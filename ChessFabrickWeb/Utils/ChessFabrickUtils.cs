using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.ServiceFabric.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Utils
{
    public static class ChessFabrickUtils
    {
        public static readonly int PARTITION_COUNT = 5;

        public static ServicePartitionKey NamePartitionKey(string name)
        {
            return new ServicePartitionKey(name[name.Length - 1] % PARTITION_COUNT);
        }

        public static ServicePartitionKey GuidPartitionKey(string guid)
        {
            return new ServicePartitionKey(long.Parse(guid.Substring(guid.LastIndexOf('-') + 1), System.Globalization.NumberStyles.HexNumber) % PARTITION_COUNT);
        }

        public static string GameGroupName(string gameId)
        {
            return $"game-{gameId}";
        }
    }
}
