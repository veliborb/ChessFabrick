using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessCommons
{
    public class PieceMove
    {
        public Piece MovedPiece { get; private set; }
        public bool PieceHasMoved { get; private set; }
        public int FromX { get; private set; }
        public int FromY { get; private set; }
        public int ToX { get; private set; }
        public int ToY { get; private set; }
        public Piece CapturedPiece { get; private set; }
        public PieceMove LastMove { get; private set; }
        public bool ConnectedMove { get; internal set; }

        public PieceMove(Piece movedPiece, int toX, int toY, Piece capturedPiece = null, PieceMove lastMove = null, bool connectedMove = false)
        {
            MovedPiece = movedPiece;
            FromX = movedPiece.X;
            FromY = movedPiece.Y;
            PieceHasMoved = movedPiece.HasMoved;
            ToX = toX;
            ToY = toY;
            CapturedPiece = capturedPiece;
            LastMove = lastMove;
            ConnectedMove = connectedMove;
        }
    }
}
