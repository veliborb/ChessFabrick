using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Utils
{
    public static class ChessFabrickUtils
    {
        public static long GamePartition(long gameId)
        {
            return gameId % 5;
        }

        public static string GameGroupName(long gameId)
        {
            return $"game-{gameId}";
        }
    }
}
