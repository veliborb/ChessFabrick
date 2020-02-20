using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class King : Piece
    {
        public King(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            if (!HasMoved)
            {
                var rook1 = Board[0, Y];
                if (rook1 != null && !rook1.HasMoved && Board[1, Y] == null && Board[2, Y] == null)
                {
                    moves.Add(Tuple.Create(1, Y));
                }
                var rook2 = Board[7, Y];
                if (rook2 != null && !rook2.HasMoved && Board[6, Y] == null && Board[5, Y] == null && Board[4, Y] == null)
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
                if (Board.FieldExists(move.Item1, move.Item2) && Board[move.Item1, move.Item2]?.Color != Color)
                {
                    moves.Add(move);
                }
            }
        }

        internal Tuple<Piece, int, int> Rokada(int x, int y)
        {
            if (X == 3 && x == 1)
            {
                return Tuple.Create(Board[0, Y], 2, Y);
            }
            if (X == 3 && x == 5)
            {
                return Tuple.Create(Board[7, Y], 4, Y);
            }
            return null;
        }
    }
}
