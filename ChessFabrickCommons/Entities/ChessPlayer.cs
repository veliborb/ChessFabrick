using System;
using System.Runtime.Serialization;

namespace ChessFabrickCommons.Entities
{
    [DataContract]
    public sealed class ChessPlayer
    {
        [DataMember] public long Id { get; private set; }
        [DataMember] public string Name { get; private set; }

        public ChessPlayer(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
