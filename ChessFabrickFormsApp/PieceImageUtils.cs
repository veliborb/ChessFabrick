using ChessCommons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessFabrickFormsApp
{
    public static class PieceImageUtils
    {
        public static Image Image(this Piece piece)
        {
            if (piece is Pawn)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.pawn_white : Properties.Resources.pawn_black;
            }
            if (piece is Bishop)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.bishop_white : Properties.Resources.bishop_black;
            }
            if (piece is Knight)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.knight_white : Properties.Resources.knight_black;
            }
            if (piece is Rook)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.rook_white : Properties.Resources.rook_black;
            }
            if (piece is Queen)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.queen_white : Properties.Resources.queen_black;
            }
            if (piece is King)
            {
                return piece.Color == PieceColor.White ? Properties.Resources.king_white : Properties.Resources.king_black;
            }
            return null;
        }

        public static Image Pawn(PieceColor color)
        {
            return color == PieceColor.White ? Properties.Resources.pawn_white : Properties.Resources.pawn_black;
        }

        public static Image King(PieceColor color)
        {
            return color == PieceColor.White ? Properties.Resources.king_white : Properties.Resources.king_black;
        }
    }
}
