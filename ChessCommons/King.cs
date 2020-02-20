using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class King : Figure
    {
        public King(FigureColor color, Board table, int x, int y) : base(color, table, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            if (!HasMoved)
            {
                var rook1 = Table[0, Y];
                if (rook1 != null && !rook1.HasMoved && Table[1, Y] == null && Table[2, Y] == null)
                {
                    moves.Add(Tuple.Create(1, Y));
                }
                var rook2 = Table[7, Y];
                if (rook2 != null && !rook2.HasMoved && Table[6, Y] == null && Table[5, Y] == null && Table[4, Y] == null)
                {
                    moves.Add(Tuple.Create(5, Y));
                }
            }

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

        internal Tuple<Figure, int, int> Rokada(int x, int y)
        {
            if (X == 3 && x == 1)
            {
                return Tuple.Create(Table[0, Y], 2, Y);
            }
            if (X == 3 && x == 5)
            {
                return Tuple.Create(Table[7, Y], 4, Y);
            }
            return null;
        }
    }
}
