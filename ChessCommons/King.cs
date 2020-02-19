using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class King : Figure
    {
        public King(FigureColor color, ChessTable table, int x, int y) : base(color, table, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            var possibleMoves = new Tuple<int, int>[]
            {
                Tuple.Create(X - 1, Y - 1),
                Tuple.Create(X - 1, Y),
                Tuple.Create(X - 1, Y + 1),
                Tuple.Create(X, Y - 1),
                Tuple.Create(X, Y + 1),
                Tuple.Create(X + 1, Y - 1),
                Tuple.Create(X + 1, Y),
                Tuple.Create(X + 1, Y + 1),
            };
            foreach (var move in possibleMoves)
            {
                if (Table.FieldExists(move.Item1, move.Item2) && Table[move.Item1, move.Item2]?.Color != Color)
                {
                    moves.Add(move);
                }
            }
        }
    }
}
