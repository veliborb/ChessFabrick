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
            this.panBoard = new System.Windows.Forms.Panel();
            this.panKilledWhite = new System.Windows.Forms.Panel();
            this.panKilledBlack = new System.Windows.Forms.Panel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnCheckCheckmate = new System.Windows.Forms.Button();
            this.labCheckmate = new System.Windows.Forms.Label();
            this.labKilledWhite = new System.Windows.Forms.Label();
            this.labKilledBlack = new System.Windows.Forms.Label();
            this.labTurn = new System.Windows.Forms.Label();
            this.panKilledWhite.SuspendLayout();
            this.panKilledBlack.SuspendLayout();
            this.SuspendLayout();
            // 
            // panBoard
            // 
            this.panBoard.BackColor = System.Drawing.Color.FloralWhite;
            this.panBoard.Location = new System.Drawing.Point(12, 88);
            this.panBoard.Name = "panBoard";
            this.panBoard.Size = new System.Drawing.Size(435, 435);
            this.panBoard.TabIndex = 0;
            // 
            // panKilledWhite
            // 
            this.panKilledWhite.BackColor = System.Drawing.Color.Peru;
            this.panKilledWhite.Controls.Add(this.hScrollBar2);
            this.panKilledWhite.Location = new System.Drawing.Point(12, 529);
            this.panKilledWhite.Name = "panKilledWhite";
            this.panKilledWhite.Size = new System.Drawing.Size(435, 70);
            this.panKilledWhite.TabIndex = 1;
            // 
            // panKilledBlack
            // 
            this.panKilledBlack.BackColor = System.Drawing.Color.Cornsilk;
            this.panKilledBlack.Controls.Add(this.hScrollBar1);
            this.panKilledBlack.Location = new System.Drawing.Point(12, 12);
            this.panKilledBlack.Name = "panKilledBlack";
            this.panKilledBlack.Size = new System.Drawing.Size(435, 70);
            this.panKilledBlack.TabIndex = 2;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(435, 17);
            this.hScrollBar1.TabIndex = 0;
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(0, 53);
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(435, 17);
            this.hScrollBar2.TabIndex = 0;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(453, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnCheckCheckmate
            // 
            this.btnCheckCheckmate.Location = new System.Drawing.Point(453, 41);
            this.btnCheckCheckmate.Name = "btnCheckCheckmate";
            this.btnCheckCheckmate.Size = new System.Drawing.Size(75, 23);
            this.btnCheckCheckmate.TabIndex = 4;
            this.btnCheckCheckmate.Text = "Checkmate?";
            this.btnCheckCheckmate.UseVisualStyleBackColor = true;
            this.btnCheckCheckmate.Click += new System.EventHandler(this.btnCheckCheckmate_Click);
            // 
            // labCheckmate
            // 
            this.labCheckmate.AutoSize = true;
            this.labCheckmate.Location = new System.Drawing.Point(534, 46);
            this.labCheckmate.Name = "labCheckmate";
            this.labCheckmate.Size = new System.Drawing.Size(29, 13);
            this.labCheckmate.TabIndex = 5;
            this.labCheckmate.Text = "false";
            // 
            // labKilledWhite
            // 
            this.labKilledWhite.AutoSize = true;
            this.labKilledWhite.Location = new System.Drawing.Point(453, 88);
            this.labKilledWhite.Name = "labKilledWhite";
            this.labKilledWhite.Size = new System.Drawing.Size(60, 13);
            this.labKilledWhite.TabIndex = 6;
            this.labKilledWhite.Text = "Killed white";
            // 
            // labKilledBlack
            // 
            this.labKilledBlack.AutoSize = true;
            this.labKilledBlack.Location = new System.Drawing.Point(453, 119);
            this.labKilledBlack.Name = "labKilledBlack";
            this.labKilledBlack.Size = new System.Drawing.Size(61, 13);
            this.labKilledBlack.TabIndex = 7;
            this.labKilledBlack.Text = "Killed black";
            // 
            // labTurn
            // 
            this.labTurn.AutoSize = true;
            this.labTurn.Location = new System.Drawing.Point(534, 17);
            this.labTurn.Name = "labTurn";
            this.labTurn.Size = new System.Drawing.Size(32, 13);
            this.labTurn.TabIndex = 8;
            this.labTurn.Text = "Turn:";
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 620);
            this.Controls.Add(this.labTurn);
            this.Controls.Add(this.labKilledBlack);
            this.Controls.Add(this.labKilledWhite);
            this.Controls.Add(this.labCheckmate);
            this.Controls.Add(this.btnCheckCheckmate);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.panKilledBlack);
            this.Controls.Add(this.panKilledWhite);
            this.Controls.Add(this.panBoard);
            this.Name = "ChessForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ChessForm_Load);
            this.panKilledWhite.ResumeLayout(false);
            this.panKilledBlack.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panBoard;
        private System.Windows.Forms.Panel panKilledWhite;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Panel panKilledBlack;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnCheckCheckmate;
        private System.Windows.Forms.Label labCheckmate;
        private System.Windows.Forms.Label labKilledWhite;
        private System.Windows.Forms.Label labKilledBlack;
        private System.Windows.Forms.Label labTurn;
    }
}

