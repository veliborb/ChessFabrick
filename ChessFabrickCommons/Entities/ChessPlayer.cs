using System;
using System.Runtime.Serialization;

namespace ChessFabrickCommons.Entities
{
    [DataContract]
    public sealed class ChessPlayer
    {
        [DataMember] public string Name { get; private set; }

        public ChessPlayer() { }

        public ChessPlayer(string name)
        {
            Name = name;
        }
    }
}
