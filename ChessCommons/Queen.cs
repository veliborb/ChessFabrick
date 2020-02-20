using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Queen : Piece
    {
        public Queen(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            Bishop.CalculatePossibleMoves(this, moves);
            Rook.CalculatePossibleMoves(this, moves);
        }
    }
}
