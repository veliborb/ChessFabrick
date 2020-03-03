using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChessFabrickCommons.Entities
{
    [DataContract]
    public sealed class ChessGameInfo
    {
        [DataMember] public string GameId { get; private set; }
        [DataMember] public ChessPlayer White { get; private set; }
        [DataMember] public ChessPlayer Black { get; private set; }
        [DataMember] public string MoveHistory { get; private set; }

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
