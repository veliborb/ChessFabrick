using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public enum FigureColor
    {
        White,
        Black
    }

    public static class FigureColorExtensions
    {
        public static FigureColor Other(this FigureColor color)
        {
            return color == FigureColor.White ? FigureColor.Black : FigureColor.White;
        }
    }
}
