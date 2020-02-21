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
                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = new ChessFieldBox();
                    fieldBox.Location = new Point(3 + 53 * i, 3 + 53 * j);
                    fieldBox.BackColor = (i + j) % 2 == 1 ? Color.Peru : Color.Cornsilk;
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
            RefreshViews();
        }

        private void RefreshViews()
        {
            if (board.IsCheckmate)
            {
                labPlaying.Text = "Victory";
                cfbPlaying.Image = PieceImageUtils.King(board.TurnColor);
            }
            else if (board.IsDraw)
            {
                labPlaying.Text = "Draw";
                cfbPlaying.Image = null;
            }
            else
            {
                labPlaying.Text = "Now playing";
                cfbPlaying.Image = PieceImageUtils.Pawn(board.TurnColor);
            }

            panKilledWhite.SuspendLayout();
            panKilledWhite.Controls.Clear();
            var k = 0;
            foreach (var killed in board.GetKilled(PieceColor.White))
            {
                var fieldBox = new ChessFieldBox();
                fieldBox.Location = new Point(3 + 53 * k++, 20);
                fieldBox.Image = killed.Image();
                panKilledWhite.Controls.Add(fieldBox);
            }
            panKilledWhite.ResumeLayout(false);

            panKilledBlack.SuspendLayout();
            panKilledBlack.Controls.Clear();
            k = 0;
            foreach (var killed in board.GetKilled(PieceColor.Black))
            {
                var fieldBox = new ChessFieldBox();
                fieldBox.Location = new Point(3 + 53 * k++, 3);
                fieldBox.Image = killed.Image();
                panKilledBlack.Controls.Add(fieldBox);
            }
            panKilledBlack.ResumeLayout(false);

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = fieldBoxes[i, j];
                    fieldBox.SuspendLayout();
                    fieldBox.BorderColor = null;
                    fieldBox.Image = board[i, j]?.Image();
                }
            }

            if (board.IsCheck)
            {
                var king = board.King(board.TurnColor);
                fieldBoxes[king.X, king.Y].BorderColor = Color.Red;
                fieldBoxes[king.X, king.Y].FieldBorderStyle = ButtonBorderStyle.Solid;
                foreach (var piece in board.CheckPieces)
                {
                    fieldBoxes[piece.X, piece.Y].BorderColor = Color.Red;
                    fieldBoxes[piece.X, piece.Y].FieldBorderStyle = ButtonBorderStyle.Dashed;
                }
            }

            if (selectedPiece != null)
            {
                fieldBoxes[selectedPiece.X, selectedPiece.Y].BorderColor = Color.Blue;
                fieldBoxes[selectedPiece.X, selectedPiece.Y].FieldBorderStyle = ButtonBorderStyle.Inset;
                foreach (var field in possibleMoves)
                {
                    fieldBoxes[field.Item1, field.Item2].BorderColor = 
                        board[field.Item1, field.Item2] != null || 
                        (selectedPiece is Pawn && selectedPiece.X != field.Item1) ? Color.Red : Color.Green;
                    fieldBoxes[field.Item1, field.Item2].FieldBorderStyle =
                        ButtonBorderStyle.Outset;
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
    }
}
