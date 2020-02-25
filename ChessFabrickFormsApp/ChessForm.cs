using ChessCommons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ChessFabrickFormsApp
{
    public partial class ChessForm : Form
    {
        private Board board;
        private Piece selectedPiece;
        private List<Tuple<int, int>> possibleMoves;

        private ChessFieldBox[,] fieldBoxes = new ChessFieldBox[8, 8];

        public ChessForm()
        {
            Icon = Icon.FromHandle(Properties.Resources.knight_white.GetHicon());
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            panTable.SuspendLayout();
            for (int i = 0; i < 8; ++i)
            {
                var labColumn = new Label();
                labColumn.Location = new Point(20 + 53 * i, 0);
                labColumn.AutoSize = false;
                labColumn.Size = new Size(50, 20);
                labColumn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labColumn.Text = ((char)('A' + i)).ToString();
                panTable.Controls.Add(labColumn);

                var labRow = new Label();
                labRow.Location = new Point(0, 20 + 53 * i);
                labRow.AutoSize = false;
                labRow.Size = new Size(20, 50);
                labRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labRow.Text = ((char)('8' - i)).ToString();
                panTable.Controls.Add(labRow);

                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = new ChessFieldBox();
                    fieldBox.Location = new Point(20 + 53 * i, 20 + 53 * (7 - j));
                    fieldBox.BackColor = (i + j) % 2 == 0 ? Color.Peru : Color.Cornsilk;
                    fieldBox.Click += FieldBox_Click;
                    fieldBox.Tag = Tuple.Create(i, j);
                    panTable.Controls.Add(fieldBox);
                    fieldBoxes[i, j] = fieldBox;
                }
            }
            panTable.ResumeLayout(false);
        }

        private void FieldBox_Click(object sender, EventArgs e)
        {
            var field = (sender as ChessFieldBox).Tag as Tuple<int, int>;
            if (selectedPiece == null)
            {
                selectedPiece = board[field.Item1, field.Item2];
                if (selectedPiece?.Color != board.TurnColor)
                {
                    selectedPiece = null;
                }
                possibleMoves = selectedPiece?.GetPossibleMoves();
            }
            else
            {
                if (possibleMoves.Contains(field))
                {
                    selectedPiece.MoveTo(field.Item1, field.Item2);
                }
                selectedPiece = null;
                possibleMoves = null;
            }
            RefreshViews();
        }

        private void ChessForm_Load(object sender, EventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            board = new Board();
            try
            {
                board.PerformMoves(txbMoves.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            RefreshViews();
        }

        private void RefreshViews()
        {
            if (board.IsCheckmate)
            {
                panTable.Enabled = false;
                labPlaying.Text = "Victory";
                cfbPlaying.Image = PieceImageUtils.King(board.TurnColor);
            }
            else if (board.IsDraw)
            {
                panTable.Enabled = false;
                labPlaying.Text = "Draw";
                cfbPlaying.Image = null;
            }
            else
            {
                panTable.Enabled = true;
                labPlaying.Text = "Now playing";
                cfbPlaying.Image = PieceImageUtils.Pawn(board.TurnColor);
            }

            pieceListPanelBlack.setPieces(board.GetKilled(PieceColor.Black));
            pieceListPanelWhite.setPieces(board.GetKilled(PieceColor.White));

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = fieldBoxes[i, j];
                    fieldBox.SuspendLayout();
                    fieldBox.Style = ChessFieldBox.BoxStyle.None;
                    fieldBox.Image = board[i, j]?.Image();
                }
            }

            if (board.IsCheck)
            {
                var king = board.King(board.TurnColor);
                fieldBoxes[king.X, king.Y].Style = ChessFieldBox.BoxStyle.Checked;
                foreach (var piece in board.CheckingPieces)
                {
                    fieldBoxes[piece.X, piece.Y].Style = ChessFieldBox.BoxStyle.Checking;
                }
            }

            if (selectedPiece != null)
            {
                fieldBoxes[selectedPiece.X, selectedPiece.Y].Style = ChessFieldBox.BoxStyle.Selected;
                foreach (var field in possibleMoves)
                {
                    fieldBoxes[field.Item1, field.Item2].Style = 
                        board[field.Item1, field.Item2] != null || (selectedPiece is Pawn && selectedPiece.X != field.Item1) ?
                        ChessFieldBox.BoxStyle.Attack : ChessFieldBox.BoxStyle.Move;
                }
            }

            foreach (var fieldBox in fieldBoxes)
            {
                fieldBox.ResumeLayout(false);
                fieldBox.Invalidate();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            board.UndoMovePiece();
            RefreshViews();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void btnWriteMoves_Click(object sender, EventArgs e)
        {
            txbMoves.Text = board.ToMovesString();
        }

        private void btnClearMoves_Click(object sender, EventArgs e)
        {
            txbMoves.Text = "";
        }
    }
}
