using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Pawn : Figure
    {
        public Pawn(FigureColor color, ChessTable table, int x, int y) : base(color, table, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            var moveDirection = (Color == FigureColor.White) ? 1 : -1;
            if (Table[X, Y + moveDirection] == null)
            {
                moves.Add(Tuple.Create(X, Y + moveDirection));
                if (!HasMoved && Table[X, Y + 2 * moveDirection] == null)
                {
                    moves.Add(Tuple.Create(X, Y + 2 * moveDirection));
                }
            }
            if (X > 0 && Table[X - 1, Y + moveDirection] != null && Table[X - 1, Y + moveDirection].Color != Color)
            {
                moves.Add(Tuple.Create(X - 1, Y + moveDirection));
            }
            if (X < (ChessTable.SIZE - 1) && Table[X + 1, Y + moveDirection] != null && Table[X + 1, Y + moveDirection].Color != Color)
            {
                moves.Add(Tuple.Create(X + 1, Y + moveDirection));
            }
        }
    }
}
