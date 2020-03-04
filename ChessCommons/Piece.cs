using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace ChessCommons
{
    public abstract class Piece
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public PieceColor Color { get; internal set; }
        public Board Board { get; internal set; }
        public bool HasMoved { get; internal set; }
        public bool IsCaptured { get; internal set; }

        public Piece(PieceColor color, Board board, int x, int y, bool hasMoved = false, bool isCaptured = false)
        {
            this.Color = color;
            this.Board = board;
            this.X = x;
            this.Y = y;
            this.HasMoved = hasMoved;
            this.IsCaptured = isCaptured;
        }

        public Piece(Piece piece)
        {
            CopyFrom(piece);
        }

        public void CopyFrom(Piece piece)
        {
            X = piece.X;
            Y = piece.Y;
            Color = piece.Color;
            Board = piece.Board;
            HasMoved = piece.HasMoved;
            IsCaptured = piece.IsCaptured;
        }

        public Piece Clone()
        {
            return MemberwiseClone() as Piece;
        }

        public List<Tuple<int, int>> GetPossibleMoves()
        {
            var moves = new List<Tuple<int, int>>();
            CalculatePossibleMoves(moves);
            return moves;
        }

        protected abstract void CalculatePossibleMoves(List<Tuple<int, int>> moves);

        public bool MoveTo(int x, int y)
        {
            return Board.MovePiece(this, x, y);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
