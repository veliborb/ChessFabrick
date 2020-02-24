using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChessCommons
{
    public static class ChessGameUtils
    {

        public static string ToMoveString(this PieceMove pieceMove)
        {
            return new StringBuilder(FieldToString(pieceMove.FromX, pieceMove.FromY)).Append(',').Append(FieldToString(pieceMove.ToX, pieceMove.ToY)).ToString();
        }

        public static string FieldToString(int x, int y)
        {
            return new StringBuilder().Append((char)('A' + x)).Append(y + 1).ToString();
        }

        public static Tuple<int, int> FieldFromString(string s)
        {
            return Tuple.Create(s[0] - 'A', s[1] - '1');
        }

        public static void PerformMoves(this Board board, string moves)
        {
            if (string.IsNullOrEmpty(moves))
            {
                return;
            }
            var moveList = moves.Trim().Split(';');
            foreach (var move in moveList)
            {
                var fields = move.Trim().Split(',');
                var from = FieldFromString(fields[0]);
                var to = FieldFromString(fields[1]);
                board.MovePiece(board[from.Item1, from.Item2], to.Item1, to.Item2);
            }
        }

        public static string ToMovesString(this Board board)
        {
            var moves = new List<string>();
            var lastMove = board.LastMove;
            while (lastMove != null)
            {
                if (lastMove.ConnectedMove)
                {
                    lastMove = lastMove.LastMove;
                }
                moves.Add(lastMove.ToMoveString());
                lastMove = lastMove.LastMove;
            }
            moves.Reverse();
            return string.Join(";", moves);
        }
    }
}
