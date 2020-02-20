using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            var moveDirection = (Color == PieceColor.White) ? 1 : -1;

            if (Board[X, Y + moveDirection] == null)
            {
                moves.Add(Tuple.Create(X, Y + moveDirection));
                if (!HasMoved && Board[X, Y + 2 * moveDirection] == null)
                {
                    moves.Add(Tuple.Create(X, Y + 2 * moveDirection));
                }
            }

            if (X > 0 && Board[X - 1, Y + moveDirection] != null && Board[X - 1, Y + moveDirection].Color != Color)
            {
                moves.Add(Tuple.Create(X - 1, Y + moveDirection));
            }
            if (X < (Board.SIZE - 1) && Board[X + 1, Y + moveDirection] != null && Board[X + 1, Y + moveDirection].Color != Color)
            {
                moves.Add(Tuple.Create(X + 1, Y + moveDirection));
            }

            if (Board?.LastMove?.MovedPiece is Pawn)
            {
                if (X > 0 && Board.LastMove.ToX == X - 1 
                    && (Board.LastMove.FromY == Y + moveDirection || Board.LastMove.ToY == Y))
                {
                    moves.Add(Tuple.Create(X - 1, Y + moveDirection));
                }
                if (X < (Board.SIZE - 1) && Board.LastMove.ToX == X + 1
                    && (Board.LastMove.FromY == Y + moveDirection || Board.LastMove.ToY == Y))
                {
                    moves.Add(Tuple.Create(X + 1, Y + moveDirection));
                }
            }
        }
    }
}
