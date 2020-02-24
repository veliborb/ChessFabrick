using ChessCommons;
using ChessFabrickFormsApp.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFabrickFormsApp
{
    class PieceListPanel : Panel
    {
        private HScrollBar hScrollBar;

        private List<ChessFieldBox> fieldBoxes = new List<ChessFieldBox>();

        private List<Piece> pieces = new List<Piece>();

        public PieceListPanel() : base()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Enabled = false;
            this.hScrollBar.Location = new System.Drawing.Point(0, 56);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(109, 17);
            this.hScrollBar.TabIndex = 0;
            // 
            // PieceListPanel
            // 
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.hScrollBar);
            this.Size = new System.Drawing.Size(109, 75);
            this.ResumeLayout(false);

        }

        private void RefreshList()
        {
            SuspendLayout();

            for (int i = fieldBoxes.Count; i < pieces.Count; ++i)
            {
                var box = new ChessFieldBox();
                box.Location = new Point(3 + (box.Width + 3) * i, 3);
                Controls.Add(box);
                fieldBoxes.Add(box);
            }

            while (fieldBoxes.Count > pieces.Count)
            {
                Controls.Remove(fieldBoxes[fieldBoxes.Count - 1]);
                fieldBoxes.RemoveAt(fieldBoxes.Count - 1);
            }

            for (int i = 0; i < pieces.Count; ++i)
            {
                fieldBoxes[i].Image = pieces[i].Image();
            }

            hScrollBar.Visible = 3 + 53 * fieldBoxes.Count < Width;

            ResumeLayout(true);
        }

        public void setPieces(List<Piece> pieces)
        {
            this.pieces = pieces ?? new List<Piece>();
            RefreshList();
        }
    }
}
