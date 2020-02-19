using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Queen : Figure
    {
        public Queen(FigureColor color, ChessTable table, int x, int y) : base(color, table, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            Bishop.CalculatePossibleMoves(this, moves);
            Rook.CalculatePossibleMoves(this, moves);
        }
    }
}
