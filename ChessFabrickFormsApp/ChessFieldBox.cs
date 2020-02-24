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
            MouseoverImpossible
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
                        borderColor = Color.Blue;
                        borderStyle = ButtonBorderStyle.Inset;
                        break;
                    case BoxStyle.Move:
                        borderColor = Color.Green;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.Attack:
                        borderColor = Color.Red;
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
                        borderColor = Color.Blue;
                        borderStyle = ButtonBorderStyle.Outset;
                        break;
                    case BoxStyle.MouseoverImpossible:
                        borderColor = Color.Blue;
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
