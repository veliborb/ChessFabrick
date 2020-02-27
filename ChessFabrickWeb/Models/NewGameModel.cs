using ChessCommons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessFabrickWeb.Models
{
    public class NewGameModel
    {
        public long PlayerId { get; set; }
        public PieceColor PlayerColor { get; set; }
    }
}
