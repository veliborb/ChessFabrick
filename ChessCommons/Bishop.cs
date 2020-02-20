using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Bishop : Piece
    {
        public Bishop(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        internal static void CalculatePossibleMoves(Piece bishop, List<Tuple<int, int>> moves)
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
                var piece = bishop.Board[x, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (piece.Color != bishop.Color)
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
                var piece = bishop.Board[x, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (piece.Color != bishop.Color)
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
                var piece = bishop.Board[x, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (piece.Color != bishop.Color)
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
                var piece = bishop.Board[x, y];
                if (piece == null)
                {
                    moves.Add(Tuple.Create(x, y));
                }
                else
                {
                    if (piece.Color != bishop.Color)
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
