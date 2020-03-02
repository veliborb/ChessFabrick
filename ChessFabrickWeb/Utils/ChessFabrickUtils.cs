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
        public static long PartitionNumber(this string id)
        {
            return id[id.Length - 1] % 5;
        }
        public static ServicePartitionKey PartitionKey(this string id)
        {
            return new ServicePartitionKey(id.PartitionNumber());
        }

        public static string GameGroupName(this string gameId)
        {
            return $"game-{gameId}";
        }
    }
}
