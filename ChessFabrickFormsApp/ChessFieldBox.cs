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
        public Color? BorderColor { get; set; }
        public ButtonBorderStyle FieldBorderStyle { get; set; }

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
            if (BorderColor.HasValue)
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                    BorderColor.Value, 4, FieldBorderStyle,
                    BorderColor.Value, 4, FieldBorderStyle,
                    BorderColor.Value, 4, FieldBorderStyle,
                    BorderColor.Value, 4, FieldBorderStyle);
            }
        }
    }
}
