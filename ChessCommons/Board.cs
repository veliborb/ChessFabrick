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
        private readonly List<Piece> pieces;
        public PieceMove LastMove { get; private set; }
        public PieceColor TurnColor { get; private set; }
        public List<Piece> CheckingPieces { get; private set; }
        public bool IsCheck => CheckingPieces.Count > 0;
        public bool IsCheckmate { get; private set; }
        public bool IsDraw => GetAlive().Count == 2;

        public Board()
        {
            TurnColor = PieceColor.White;
            CheckingPieces = new List<Piece>();
            fields = new Piece[SIZE, SIZE];
            pieces = new List<Piece>(32);
            for (int i = 0; i < SIZE; ++i)
            {
                pieces.Add(new Pawn(PieceColor.White, this, i, 1));
                pieces.Add(new Pawn(PieceColor.Black, this, i, 6));
            }
            pieces.Add(new Rook(PieceColor.White, this, 0, 0));
            pieces.Add(new Rook(PieceColor.White, this, 7, 0));
            pieces.Add(new Knight(PieceColor.White, this, 1, 0));
            pieces.Add(new Knight(PieceColor.White, this, 6, 0));
            pieces.Add(new Bishop(PieceColor.White, this, 2, 0));
            pieces.Add(new Bishop(PieceColor.White, this, 5, 0));
            pieces.Add(new Queen(PieceColor.White, this, 3, 0));
            pieces.Add(new King(PieceColor.White, this, 4, 0));
            pieces.Add(new Rook(PieceColor.Black, this, 0, 7));
            pieces.Add(new Rook(PieceColor.Black, this, 7, 7));
            pieces.Add(new Knight(PieceColor.Black, this, 1, 7));
            pieces.Add(new Knight(PieceColor.Black, this, 6, 7));
            pieces.Add(new Bishop(PieceColor.Black, this, 2, 7));
            pieces.Add(new Bishop(PieceColor.Black, this, 5, 7));
            pieces.Add(new Queen(PieceColor.Black, this, 3, 7));
            pieces.Add(new King(PieceColor.Black, this, 4, 7));
            foreach (var piece in pieces)
            {
                fields[piece.X, piece.Y] = piece;
            }
        }

        public List<Piece> GetAlive(PieceColor? color = null)
            => pieces.FindAll(piece => piece.IsAlive && (color == null || piece.Color == color));

        public List<Piece> GetKilled(PieceColor? color = null)
            => pieces.FindAll(piece => !piece.IsAlive && (color == null || piece.Color == color));

        public King King(PieceColor color)
            => pieces.Find(piece => piece is King && piece.Color == color) as King;

        public bool MovePiece(Piece piece, int x, int y)
        {
            if (IsCheckmate || IsDraw
                || fields[piece.X, piece.Y] != piece 
                || piece.Color != TurnColor 
                || !piece.GetPossibleMoves().Contains(Tuple.Create(x, y)))
            {
                return false;
            }
            PerformMove(piece, x, y);
            if (CheckCheck(piece.Color.Other()).Count > 0)
            {
                PerformUndoMove();
                return false;
            }
            PerformTurn();
            return true;
        }

        public void UndoMovePiece()
        {
            if (LastMove == null)
            {
                return;
            }
            PerformUndoMove();
            PerformTurn();
        }

        private void PerformMove(Piece piece, int x, int y)
        {
            var destPiece = fields[x, y];
            if (piece is Pawn && destPiece == null && piece.X != x)
            {
                destPiece = LastMove.MovedPiece;
            }

            var rokada = (piece as King)?.Rokada(x, y);

            LastMove = new PieceMove(piece, x, y, destPiece, LastMove);

            if (destPiece != null)
            {
                fields[destPiece.X, destPiece.Y] = null;
                destPiece.IsAlive = false;
            }
            fields[piece.X, piece.Y] = null;
            piece.X = x;
            piece.Y = y;
            piece.HasMoved = true;
            fields[x, y] = piece;

            if (rokada != null)
            {
                PerformMove(rokada.Item1, rokada.Item2, rokada.Item3);
                LastMove.ConnectedMove = true;
            }
            else if (piece is Pawn && (piece.Y == 0 || piece.Y == SIZE - 1))
            {
                var queen = new Queen(piece.Color, this, -1, -1);

                LastMove = new PieceMove(queen, piece.X, piece.Y, piece, LastMove);
                LastMove.ConnectedMove = true;

                fields[x, y] = queen;
                queen.X = x;
                queen.Y = y;
                pieces.Add(queen);

                piece.IsAlive = false;
            }
        }

        private void PerformTurn()
        {
            CheckingPieces = CheckCheck(TurnColor);
            IsCheckmate = IsCheck && CheckCheckmate(TurnColor);
            TurnColor = TurnColor.Other();
        }

        private void PerformUndoMove()
        {
            if (LastMove.FromX == -1)
            {
                pieces.Remove(LastMove.MovedPiece);
            }
            else
            {
                fields[LastMove.FromX, LastMove.FromY] = LastMove.MovedPiece;
                LastMove.MovedPiece.X = LastMove.FromX;
                LastMove.MovedPiece.Y = LastMove.FromY;
                LastMove.MovedPiece.HasMoved = LastMove.PieceHasMoved;
            }

            fields[LastMove.ToX, LastMove.ToY] = null;
            if (LastMove.KilledPiece != null)
            {
                fields[LastMove.KilledPiece.X, LastMove.KilledPiece.Y] = LastMove.KilledPiece;
                LastMove.KilledPiece.IsAlive = true;
            }

            var connected = LastMove.ConnectedMove;
            LastMove = LastMove.LastMove;
            if (connected)
            {
                PerformUndoMove();
            }
        }

        // Returns the list of pieces of a given color that are checking the opposing King.
        private List<Piece> CheckCheck(PieceColor color)
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

        // Returns true if King of opposite color is checkmated.
        private bool CheckCheckmate(PieceColor color)
        {
            var pieces = GetAlive(color.Other());
            foreach (var piece in pieces)
            {
                foreach (var move in piece.GetPossibleMoves())
                {
                    PerformMove(piece, move.Item1, move.Item2);
                    var check = CheckCheck(color);
                    PerformUndoMove();
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
