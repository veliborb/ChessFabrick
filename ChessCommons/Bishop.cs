using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Bishop : Figure
    {
        public Bishop(FigureColor color, Board board, int x, int y) : base(color, board, x, y) { }

        internal static void CalculatePossibleMoves(Figure bishop, List<Tuple<int, int>> moves)
        {
            var size = Math.Min(Board.SIZE, Board.SIZE);

            for (int i = 1; i < size; ++i)
            {
                int x = bishop.X + i;
                int y = bishop.Y + i;
                if (!bishop.Board.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Board[x, y];
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
                if (!bishop.Board.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Board[x, y];
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
                if (!bishop.Board.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Board[x, y];
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
                if (!bishop.Board.FieldExists(x, y))
                {
                    break;
                }
                var figure = bishop.Board[x, y];
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
