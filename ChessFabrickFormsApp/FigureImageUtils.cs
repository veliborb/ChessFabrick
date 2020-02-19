using ChessCommons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFabrickFormsApp
{
    public static class FigureImageUtils
    {
        public static Image Image(this Figure figure)
        {
            if (figure is Pawn)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.pawn_white : Properties.Resources.pawn_black;
            }
            if (figure is Bishop)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.bishop_white : Properties.Resources.bishop_black;
            }
            if (figure is Knight)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.knight_white : Properties.Resources.knight_black;
            }
            if (figure is Rook)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.rook_white : Properties.Resources.rook_black;
            }
            if (figure is Queen)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.queen_white : Properties.Resources.queen_black;
            }
            if (figure is King)
            {
                return figure.Color == FigureColor.White ? Properties.Resources.king_white : Properties.Resources.king_black;
            }
            return null;
        }
    }
}
