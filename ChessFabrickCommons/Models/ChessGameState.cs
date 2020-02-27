using ChessCommons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessFabrickCommons.Models
{
    public sealed class ChessGameState
    {
        public ChessGameInfo GameInfo { get; set; }
        public string ChessBoard { get; set; }
        public List<char> KilledPieces { get; set; }
        public List<string> CheckingPieces { get; set; }
        public bool IsDraw { get; set; }
        public bool IsCheckmate { get; set; }
        public PieceColor OnTurn { get; set; }

        public ChessGameState() { }

        public ChessGameState(ChessGameInfo gameInfo)
        {
            GameInfo = gameInfo;

            var board = new Board();
            board.PerformMoves(gameInfo.MoveHistory);

            var sb = new StringBuilder();
            for (int i = 0; i < Board.SIZE; ++i)
            {
                for (int j = 0; j < Board.SIZE; ++j)
                {
                    if (board[i, j] != null)
                    {
                        sb.Append(board[i, j].ToChar());
                    }
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append(';');
            }
            ChessBoard = sb.Remove(sb.Length - 1, 1).ToString();

            KilledPieces = new List<char>();
            foreach (var piece in board.GetKilled())
            {
                KilledPieces.Add(piece.ToChar());
            }

            CheckingPieces = new List<string>();
            foreach (var piece in board.CheckingPieces)
            {
                CheckingPieces.Add(ChessGameUtils.FieldToString(piece.X, piece.Y));
            }

            IsDraw = board.IsDraw;
            IsCheckmate = board.IsCheckmate;
            OnTurn = board.TurnColor;
        }
    }
}
