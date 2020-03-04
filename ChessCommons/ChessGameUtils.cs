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

        public static char ToChar(this Piece piece)
        {
            char c = ' ';
            if (piece is Pawn)
            {
                c = 'p';
            }
            if (piece is Bishop)
            {
                c = 'b';
            }
            if (piece is Knight)
            {
                c = 'k';
            }
            if (piece is Rook)
            {
                c = 'r';
            }
            if (piece is Queen)
            {
                c = 'q';
            }
            if (piece is King)
            {
                c = 'w';
            }
            return piece.Color == PieceColor.White ? char.ToUpper(c) : char.ToLower(c);
        }

        public static Piece FromChar(char c)
        {
            switch (c)
            {
                case 'P':
                    return new Pawn(PieceColor.White, null, -1, -1);
                case 'p':
                    return new Pawn(PieceColor.Black, null, -1, -1);
                case 'B':
                    return new Bishop(PieceColor.White, null, -1, -1);
                case 'b':
                    return new Bishop(PieceColor.Black, null, -1, -1);
                case 'K':
                    return new Knight(PieceColor.White, null, -1, -1);
                case 'k':
                    return new Knight(PieceColor.Black, null, -1, -1);
                case 'R':
                    return new Rook(PieceColor.White, null, -1, -1);
                case 'r':
                    return new Rook(PieceColor.Black, null, -1, -1);
                case 'Q':
                    return new Queen(PieceColor.White, null, -1, -1);
                case 'q':
                    return new Queen(PieceColor.Black, null, -1, -1);
                case 'W':
                    return new King(PieceColor.White, null, -1, -1);
                case 'w':
                    return new King(PieceColor.Black, null, -1, -1);
            }
            return null;
        }
    }
}
