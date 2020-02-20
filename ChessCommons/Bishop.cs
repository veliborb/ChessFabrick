using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Bishop : Figure
    {
        public Bishop(FigureColor color, Board table, int x, int y) : base(color, table, x, y) { }

        internal static void CalculatePossibleMoves(Figure bishop, List<Tuple<int, int>> moves)
        {
            var size = Math.Min(Board.SIZE, Board.SIZE);

            for (int i = 1; i < size; ++i)
            {
                int x = bishop.X + i;
                int y = bishop.Y + i;
                if (!bishop.Table.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Table[x, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (figure.Color != bishop.Color)
                    {
                        moves.Add(Tuple.Create(x, y));
                    }
                    break;
                }
            }

            for (int i = 1; i < size; ++i)
            {
                int x = bishop.X + i;
                int y = bishop.Y - i;
                if (!bishop.Table.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Table[x, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (figure.Color != bishop.Color)
                    {
                        moves.Add(Tuple.Create(x, y));
                    }
                    break;
                }
            }

            for (int i = 1; i < size; ++i)
            {
                int x = bishop.X - i;
                int y = bishop.Y + i;
                if (!bishop.Table.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Table[x, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (figure.Color != bishop.Color)
                    {
                        moves.Add(Tuple.Create(x, y));
                    }
                    break;
                }
            }

            for (int i = 1; i < size; ++i)
            {
                int x = bishop.X - i;
                int y = bishop.Y - i;
                if (!bishop.Table.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Table[x, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (figure.Color != bishop.Color)
                    {
                        moves.Add(Tuple.Create(x, y));
                    }
                    break;
                }
            }
        }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            CalculatePossibleMoves(this, moves);
        }
    }
}
