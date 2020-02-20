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
        private Figure selectedFigure;
        private List<Tuple<int, int>> possibleMoves;

        private ChessFieldBox[,] fieldBoxes = new ChessFieldBox[8, 8];

        public ChessForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            panBoard.SuspendLayout();
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    var fieldBox = new ChessFieldBox();
                    fieldBox.Location = new Point(3 + 53 * i, 3 + 53 * j);
                    fieldBox.BackColor = (i + j) % 2 == 1 ? Color.Peru : Color.Cornsilk;
                    fieldBox.Click += FieldBox_Click;
                    fieldBox.Tag = Tuple.Create(i, j);
                    panBoard.Controls.Add(fieldBox);
                    fieldBoxes[i, j] = fieldBox;
                }
            }
            panBoard.ResumeLayout(false);
        }

        private void FieldBox_Click(object sender, EventArgs e)
        {
            var field = (sender as ChessFieldBox).Tag as Tuple<int, int>;
            if (selectedFigure == null)
            {
                selectedFigure = board[field.Item1, field.Item2];
                if (selectedFigure?.Color != board.TurnColor)
                {
                    selectedFigure = null;
                }
                possibleMoves = selectedFigure?.GetPossibleMoves();
            }
            else
            {
                if (possibleMoves.Contains(field))
                {
                    selectedFigure.MoveTo(field.Item1, field.Item2);
                }
                selectedFigure = null;
                possibleMoves = null;
            }
            RefreshViews();
        }

        private void ChessForm_Load(object sender, EventArgs e)
        {
            board = new Board();
            RefreshViews();
        }

        private void RefreshViews()
        {
            labTurn.Text = board.TurnColor.ToString();

            var sb = new StringBuilder();
            foreach (var killed in board.GetKilled(FigureColor.White))
            {
                sb.Append(killed.GetType().Name).Append(", ");
            }
            labKilledWhite.Text = sb.ToString();

            sb.Clear();
            foreach (var killed in board.GetKilled(FigureColor.Black))
            {
                sb.Append(killed.GetType().Name).Append(", ");
            }
            labKilledBlack.Text = sb.ToString();

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

            if (selectedFigure != null)
            {
                fieldBoxes[selectedFigure.X, selectedFigure.Y].BorderColor = Color.Blue;
                foreach (var field in possibleMoves)
                {
                    fieldBoxes[field.Item1, field.Item2].BorderColor = 
                        board[field.Item1, field.Item2] != null ? Color.Red : Color.Green;
                }
                var checkFigures = board.CheckCheck(board.TurnColor);
                if (checkFigures.Count > 0)
                {
                    
                }
                foreach (var figure in checkFigures)
                {
                    fieldBoxes[figure.X, figure.Y].BorderColor = Color.Black;
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
            board.UndoMove();
            RefreshViews();
        }

        private void btnCheckCheckmate_Click(object sender, EventArgs e)
        {
            labCheckmate.Text = board.CheckCheckmate(board.TurnColor).ToString();
        }
    }
}
