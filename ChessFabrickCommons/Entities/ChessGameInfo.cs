using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChessFabrickCommons.Entities
{
    [DataContract]
    public sealed class ChessGameInfo
    {
        [DataMember] public string GameId { get; set; }
        [DataMember] public ChessPlayer White { get; set; }
        [DataMember] public ChessPlayer Black { get; set; }
        [DataMember] public string MoveHistory { get; set; }

        public ChessGameInfo() { }

        public ChessGameInfo(string gameId, ChessPlayer white, ChessPlayer black, string moveHistory = null)
        {
            GameId = gameId;
            White = white;
            Black = black;
            MoveHistory = moveHistory;
        }
    }
}
