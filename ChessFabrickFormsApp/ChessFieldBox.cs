using ChessCommons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFabrickFormsApp
{
    class ChessFieldBox : PictureBox
    {
        public enum BoxStyle
        {
            None = 0,
            Selected,
            Move,
            Attack,
            Checked,
            Checking,
            MouseoverPossible,
            MouseoverImpossible,
            MoveFrom,
            MoveTo,
            MoveToCapture
        }

        private Color? borderColor;
        private ButtonBorderStyle borderStyle;

        private BoxStyle _boxStyle;
        public BoxStyle Style {
            get => _boxStyle;
            set
            {
                _boxStyle = value;
                switch (_boxStyle)
                {
                    case BoxStyle.None:
                        borderColor = null;
                        borderStyle = ButtonBorderStyle.None;
                        break;
                    case BoxStyle.Selected:
                        borderColor = Color.DarkBlue;
                        borderStyle = ButtonBorderStyle.Inset;
                        break;
                    case BoxStyle.Move:
                        borderColor = Color.GreenYellow;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.Attack:
                        borderColor = Color.OrangeRed;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.Checked:
                        borderColor = Color.Red;
                        borderStyle = ButtonBorderStyle.Solid;
                        break;
                    case BoxStyle.Checking:
                        borderColor = Color.Red;
                        borderStyle = ButtonBorderStyle.Dashed;
                        break;
                    case BoxStyle.MouseoverPossible:
                        borderColor = Color.DarkBlue;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.MouseoverImpossible:
                        borderColor = Color.LightGray;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.MoveFrom:
                        borderColor = Color.LightGoldenrodYellow;
                        borderStyle = ButtonBorderStyle.Inset;
                        break;
                    case BoxStyle.MoveTo:
                        borderColor = Color.LightGoldenrodYellow;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.MoveToCapture:
                        borderColor = Color.OrangeRed;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                }
            }
        }

        public ChessFieldBox() : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ChessFieldBox
            // 
            this.Size = new System.Drawing.Size(50, 50);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (borderColor.HasValue || borderStyle != ButtonBorderStyle.None)
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                    borderColor.Value, 4, borderStyle,
                    borderColor.Value, 4, borderStyle,
                    borderColor.Value, 4, borderStyle,
                    borderColor.Value, 4, borderStyle);
            }
        }
    }
}
