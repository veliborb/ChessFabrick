using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChessCommons
{
    public class Board
    {
        public static readonly int SIZE = 8;

        private readonly Piece[,] fields;
        private readonly List<Piece> alive;
        private readonly List<Piece> killed;
        public PieceMove LastMove { get; private set; }
        public PieceColor TurnColor { get; private set; } = PieceColor.White;

        public Board()
        {
            fields = new Piece[SIZE, SIZE];
            alive = new List<Piece>(32);
            killed = new List<Piece>(30);
            for (int i = 0; i < SIZE; ++i)
            {
               alive.Add(new Pawn(PieceColor.White, this, i, 1));
               alive.Add(new Pawn(PieceColor.Black, this, i, 6));
            }
            alive.Add(new Rook(PieceColor.White, this, 0, 0));
            alive.Add(new Rook(PieceColor.White, this, 7, 0));
            alive.Add(new Knight(PieceColor.White, this, 1, 0));
            alive.Add(new Knight(PieceColor.White, this, 6, 0));
            alive.Add(new Bishop(PieceColor.White, this, 2, 0));
            alive.Add(new Bishop(PieceColor.White, this, 5, 0));
            alive.Add(new Queen(PieceColor.White, this, 4, 0));
            alive.Add(new King(PieceColor.White, this, 3, 0));
            alive.Add(new Rook(PieceColor.Black, this, 0, 7));
            alive.Add(new Rook(PieceColor.Black, this, 7, 7));
            alive.Add(new Knight(PieceColor.Black, this, 1, 7));
            alive.Add(new Knight(PieceColor.Black, this, 6, 7));
            alive.Add(new Bishop(PieceColor.Black, this, 2, 7));
            alive.Add(new Bishop(PieceColor.Black, this, 5, 7));
            alive.Add(new Queen(PieceColor.Black, this, 4, 7));
            alive.Add(new King(PieceColor.Black, this, 3, 7));
            foreach (var piece in alive)
            {
                fields[piece.X, piece.Y] = piece;
            }
        }

        public List<Piece> GetAlive(PieceColor? color = null)
            => alive.FindAll(piece => color == null || piece.Color == color);

        public List<Piece> GetKilled(PieceColor? color = null)
            => killed.FindAll(piece => color == null || piece.Color == color);

        internal bool MovePiece(Piece piece, int x, int y)
        {
            if (fields[piece.X, piece.Y] != piece || piece.Color != TurnColor)
            {
                return false;
            }

            var destPiece = fields[x, y];
            if (piece is Pawn)
            {
                if (destPiece == null && piece.X != x)
                {
                    destPiece = LastMove.MovedPiece;
                }
            }
            var rokada = (piece as King)?.Rokada(x, y);

            LastMove = new PieceMove(piece, x, y, destPiece, LastMove, rokada != null);

            if (destPiece != null)
            {
                fields[destPiece.X, destPiece.Y] = null;
                alive.Remove(destPiece);
                killed.Add(destPiece);
            }

            fields[piece.X, piece.Y] = null;
            piece.X = x;
            piece.Y = y;
            piece.HasMoved = true;
            fields[x, y] = piece;

            if ((rokada == null && CheckCheck(piece.Color.Other()).Count > 0)
                || (rokada != null && !MovePiece(rokada.Item1, rokada.Item2, rokada.Item3)))
            {
                UndoMove();
                return false;
            }

            TurnColor = piece.Color.Other();
            return true;
        }

        public void UndoMove()
        {
            if (LastMove == null)
            {
                return;
            }

            fields[LastMove.FromX, LastMove.FromY] = LastMove.MovedPiece;
            LastMove.MovedPiece.X = LastMove.FromX;
            LastMove.MovedPiece.Y = LastMove.FromY;
            LastMove.MovedPiece.HasMoved = LastMove.PieceHasMoved;

            fields[LastMove.ToX, LastMove.ToY] = null;
            if (LastMove.KilledPiece != null)
            {
                fields[LastMove.KilledPiece.X, LastMove.KilledPiece.Y] = LastMove.KilledPiece;
                killed.Remove(LastMove.KilledPiece);
                alive.Add(LastMove.KilledPiece);
            }

            LastMove = LastMove.LastMove;
            if (LastMove?.ConnectedMove ?? false)
            {
                UndoMove();
            }
            else
            {
                TurnColor = LastMove?.MovedPiece?.Color.Other() ?? PieceColor.White;
            }
        }

        public List<Piece> CheckCheck(PieceColor color)
        {
            var checkPieces = new List<Piece>();
            var pieces = GetAlive(color);
            foreach (var piece in pieces)
            {
                if (piece.GetPossibleMoves().Find(field => fields[field.Item1, field.Item2] is King) != null)
                {
                    checkPieces.Add(piece);
                }
            }
            return checkPieces;
        }

        public bool CheckCheckmate(PieceColor color)
        {
            var pieces = GetAlive(color.Other());
            foreach (var piece in pieces)
            {
                foreach (var move in piece.GetPossibleMoves())
                {
                    piece.MoveTo(move.Item1, move.Item2);
                    var check = CheckCheck(color);
                    UndoMove();
                    if (check.Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool FieldExists(int x, int y)
        {
            return x >= 0 && x < SIZE && y >= 0 && y < SIZE;
        }

        public Piece this[int x, int y]
        {
            get => fields[x, y];
        }
    }
}
