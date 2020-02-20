using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public enum PieceColor
    {
        White,
        Black
    }

    public static class PieceColorExtensions
    {
        public static PieceColor Other(this PieceColor color)
        {
            return color == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }
    }
}
