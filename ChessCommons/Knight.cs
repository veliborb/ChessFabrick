using System;
using System.Collections.Generic;
using System.Text;

namespace ChessCommons
{
    public class Knight : Piece
    {
        public Knight(PieceColor color, Board board, int x, int y) : base(color, board, x, y) { }

        protected override void CalculatePossibleMoves(List<Tuple<int, int>> moves)
        {
            var possibleMoves = new Tuple<int, int>[]
            {
                Tuple.Create(X - 1, Y - 2),
                Tuple.Create(X - 1, Y + 2),
                Tuple.Create(X + 1, Y - 2),
                Tuple.Create(X + 1, Y + 2),
                Tuple.Create(X - 2, Y - 1),
                Tuple.Create(X - 2, Y + 1),
                Tuple.Create(X + 2, Y - 1),
                Tuple.Create(X + 2, Y + 1)
            };
            foreach (var move in possibleMoves)
            {
                if (Board.FieldExists(move.Item1, move.Item2) && Board[move.Item1, move.Item2]?.Color != Color)
                {
                    moves.Add(move);
                }
            }
        }
    }
}
