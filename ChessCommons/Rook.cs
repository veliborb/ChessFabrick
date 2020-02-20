using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Rook : Figure
    {
        public Rook(FigureColor color, Board table, int x, int y) : base(color, table, x, y) { }

        internal static void CalculatePossibleMoves(Figure rook, List<Tuple<int, int>> moves)
        {
            for (int x = rook.X; x < Board.SIZE; ++x)
            {
                var figure = rook.Table[x, rook.Y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, rook.Y));
                }
                else
                {
                    if (figure == rook) continue;
                    if (figure.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(x, rook.Y));
                    }
                    break;
                }
            }

            for (int x = rook.X; x >= 0; --x)
            {
                var figure = rook.Table[x, rook.Y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(x, rook.Y));
                }
                else
                {
                    if (figure == rook) continue;
                    if (figure.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(x, rook.Y));
                    }
                    break;
                }
            }

            for (int y = rook.Y; y < Board.SIZE; ++y)
            {
                var figure = rook.Table[rook.X, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(rook.X, y));
                }
                else
                {
                    if (figure == rook) continue;
                    if (figure.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(rook.X, y));
                    }
                    break;
                }
            }

            for (int y = rook.Y; y >= 0; --y)
            {
                var figure = rook.Table[rook.X, y];
                if (figure == null)
                {
                    moves.Add(Tuple.Create(rook.X, y));
                }
                else
                {
                    if (figure == rook) continue;
                    if (figure.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(rook.X, y));
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
