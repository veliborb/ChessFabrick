namespace ChessFabrickFormsApp
{
    partial class ChessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panTable = new System.Windows.Forms.Panel();
            this.panKilledWhite = new System.Windows.Forms.Panel();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.panKilledBlack = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.btnUndo = new System.Windows.Forms.Button();
            this.labPlaying = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.cfbPlaying = new ChessFabrickFormsApp.ChessFieldBox();
            this.panKilledWhite.SuspendLayout();
            this.panKilledBlack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).BeginInit();
            this.SuspendLayout();
            // 
            // panTable
            // 
            this.panTable.BackColor = System.Drawing.Color.FloralWhite;
            this.panTable.Location = new System.Drawing.Point(12, 88);
            this.panTable.Name = "panTable";
            this.panTable.Size = new System.Drawing.Size(435, 435);
            this.panTable.TabIndex = 0;
            // 
            // panKilledWhite
            // 
            this.panKilledWhite.AutoScroll = true;
            this.panKilledWhite.BackColor = System.Drawing.Color.Peru;
            this.panKilledWhite.Controls.Add(this.hScrollBar2);
            this.panKilledWhite.Location = new System.Drawing.Point(12, 529);
            this.panKilledWhite.Name = "panKilledWhite";
            this.panKilledWhite.Size = new System.Drawing.Size(435, 70);
            this.panKilledWhite.TabIndex = 1;
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Enabled = false;
            this.hScrollBar2.Location = new System.Drawing.Point(0, 53);
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(435, 17);
            this.hScrollBar2.TabIndex = 0;
            // 
            // panKilledBlack
            // 
            this.panKilledBlack.AutoScroll = true;
            this.panKilledBlack.BackColor = System.Drawing.Color.Cornsilk;
            this.panKilledBlack.Controls.Add(this.hScrollBar1);
            this.panKilledBlack.Location = new System.Drawing.Point(12, 12);
            this.panKilledBlack.Name = "panKilledBlack";
            this.panKilledBlack.Size = new System.Drawing.Size(435, 70);
            this.panKilledBlack.TabIndex = 2;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Enabled = false;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(435, 17);
            this.hScrollBar1.TabIndex = 0;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(453, 88);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // labPlaying
            // 
            this.labPlaying.Location = new System.Drawing.Point(453, 9);
            this.labPlaying.Name = "labPlaying";
            this.labPlaying.Size = new System.Drawing.Size(75, 20);
            this.labPlaying.TabIndex = 5;
            this.labPlaying.Text = "Now playing";
            this.labPlaying.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(453, 576);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 6;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // cfbPlaying
            // 
            this.cfbPlaying.BorderColor = null;
            this.cfbPlaying.Location = new System.Drawing.Point(464, 32);
            this.cfbPlaying.Name = "cfbPlaying";
            this.cfbPlaying.Size = new System.Drawing.Size(50, 50);
            this.cfbPlaying.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cfbPlaying.TabIndex = 4;
            this.cfbPlaying.TabStop = false;
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 614);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.labPlaying);
            this.Controls.Add(this.cfbPlaying);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.panKilledBlack);
            this.Controls.Add(this.panKilledWhite);
            this.Controls.Add(this.panTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChessForm";
            this.Text = "ChessFabrick";
            this.Load += new System.EventHandler(this.ChessForm_Load);
            this.panKilledWhite.ResumeLayout(false);
            this.panKilledBlack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTable;
        private System.Windows.Forms.Panel panKilledWhite;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Panel panKilledBlack;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnUndo;
        private ChessFieldBox cfbPlaying;
        private System.Windows.Forms.Label labPlaying;
        private System.Windows.Forms.Button btnNewGame;
    }
}

