using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChessFabrickCommons.Models
{
    [DataContract]
    public sealed class ChessGameInfo
    {
        [DataMember] public long GameId { get; private set; }
        [DataMember] public ChessPlayer White { get; private set; }
        [DataMember] public ChessPlayer Black { get; private set; }
        [DataMember] public string MoveHistory { get; private set; }

        public ChessGameInfo(long gameId, ChessPlayer white, ChessPlayer black, string moveHistory = null)
        {
            GameId = gameId;
            White = white;
            Black = black;
            MoveHistory = moveHistory;
        }
    }
}
