using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Rook : Piece
    {
        public Rook(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        internal static void CalculatePossibleMoves(Piece rook, List<Tuple<int, int>> moves)
        {
            for (int x = rook.X; x < Board.SIZE; ++x)
            {
                var piece = rook.Board[x, rook.Y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, rook.Y));
                }
                else
                {
                    if (piece == rook) continue;
                    if (piece.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(x, rook.Y));
                    }
                    break;
                }
            }

            for (int x = rook.X; x >= 0; --x)
            {
                var piece = rook.Board[x, rook.Y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, rook.Y));
                }
                else
                {
                    if (piece == rook) continue;
                    if (piece.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(x, rook.Y));
                    }
                    break;
                }
            }

            for (int y = rook.Y; y < Board.SIZE; ++y)
            {
                var piece = rook.Board[rook.X, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(rook.X, y));
                }
                else
                {
                    if (piece == rook) continue;
                    if (piece.Color != rook.Color)
                    {
                        moves.Add(Tuple.Create(rook.X, y));
                    }
                    break;
                }
            }

            for (int y = rook.Y; y >= 0; --y)
            {
                var piece = rook.Board[rook.X, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(rook.X, y));
                }
                else
                {
                    if (piece == rook) continue;
                    if (piece.Color != rook.Color)
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
