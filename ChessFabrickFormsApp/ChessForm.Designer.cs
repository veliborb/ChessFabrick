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
            this.btnUndo = new System.Windows.Forms.Button();
            this.labPlaying = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.txbMoves = new System.Windows.Forms.TextBox();
            this.btnWriteMoves = new System.Windows.Forms.Button();
            this.pieceListPanelWhite = new ChessFabrickFormsApp.PieceListPanel();
            this.pieceListPanelBlack = new ChessFabrickFormsApp.PieceListPanel();
            this.cfbPlaying = new ChessFabrickFormsApp.ChessFieldBox();
            this.btnClearMoves = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).BeginInit();
            this.SuspendLayout();
            // 
            // panTable
            // 
            this.panTable.BackColor = System.Drawing.Color.FloralWhite;
            this.panTable.Location = new System.Drawing.Point(12, 95);
            this.panTable.Name = "panTable";
            this.panTable.Size = new System.Drawing.Size(444, 444);
            this.panTable.TabIndex = 0;
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndo.Location = new System.Drawing.Point(462, 95);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // labPlaying
            // 
            this.labPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPlaying.Location = new System.Drawing.Point(462, 12);
            this.labPlaying.Name = "labPlaying";
            this.labPlaying.Size = new System.Drawing.Size(75, 20);
            this.labPlaying.TabIndex = 5;
            this.labPlaying.Text = "Now playing";
            this.labPlaying.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewGame.Location = new System.Drawing.Point(462, 599);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 6;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // txbMoves
            // 
            this.txbMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMoves.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbMoves.Location = new System.Drawing.Point(462, 124);
            this.txbMoves.Multiline = true;
            this.txbMoves.Name = "txbMoves";
            this.txbMoves.Size = new System.Drawing.Size(75, 415);
            this.txbMoves.TabIndex = 10;
            // 
            // btnWriteMoves
            // 
            this.btnWriteMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteMoves.Location = new System.Drawing.Point(462, 572);
            this.btnWriteMoves.Name = "btnWriteMoves";
            this.btnWriteMoves.Size = new System.Drawing.Size(75, 23);
            this.btnWriteMoves.TabIndex = 11;
            this.btnWriteMoves.Text = "Write Moves";
            this.btnWriteMoves.UseVisualStyleBackColor = true;
            this.btnWriteMoves.Click += new System.EventHandler(this.btnWriteMoves_Click);
            // 
            // pieceListPanelWhite
            // 
            this.pieceListPanelWhite.AutoScroll = true;
            this.pieceListPanelWhite.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.pieceListPanelWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieceListPanelWhite.Location = new System.Drawing.Point(12, 545);
            this.pieceListPanelWhite.Name = "pieceListPanelWhite";
            this.pieceListPanelWhite.Size = new System.Drawing.Size(444, 77);
            this.pieceListPanelWhite.TabIndex = 8;
            // 
            // pieceListPanelBlack
            // 
            this.pieceListPanelBlack.AutoScroll = true;
            this.pieceListPanelBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pieceListPanelBlack.Location = new System.Drawing.Point(12, 12);
            this.pieceListPanelBlack.Name = "pieceListPanelBlack";
            this.pieceListPanelBlack.Size = new System.Drawing.Size(444, 77);
            this.pieceListPanelBlack.TabIndex = 7;
            // 
            // cfbPlaying
            // 
            this.cfbPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cfbPlaying.Location = new System.Drawing.Point(474, 35);
            this.cfbPlaying.Name = "cfbPlaying";
            this.cfbPlaying.Size = new System.Drawing.Size(50, 50);
            this.cfbPlaying.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cfbPlaying.Style = ChessFabrickFormsApp.ChessFieldBox.BoxStyle.None;
            this.cfbPlaying.TabIndex = 4;
            this.cfbPlaying.TabStop = false;
            // 
            // btnClearMoves
            // 
            this.btnClearMoves.Location = new System.Drawing.Point(462, 545);
            this.btnClearMoves.Name = "btnClearMoves";
            this.btnClearMoves.Size = new System.Drawing.Size(75, 23);
            this.btnClearMoves.TabIndex = 12;
            this.btnClearMoves.Text = "Clear";
            this.btnClearMoves.UseVisualStyleBackColor = true;
            this.btnClearMoves.Click += new System.EventHandler(this.btnClearMoves_Click);
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 634);
            this.Controls.Add(this.btnClearMoves);
            this.Controls.Add(this.btnWriteMoves);
            this.Controls.Add(this.txbMoves);
            this.Controls.Add(this.pieceListPanelWhite);
            this.Controls.Add(this.pieceListPanelBlack);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.labPlaying);
            this.Controls.Add(this.cfbPlaying);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.panTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChessForm";
            this.Text = "ChessFabrick";
            this.Load += new System.EventHandler(this.ChessForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panTable;
        private System.Windows.Forms.Button btnUndo;
        private ChessFieldBox cfbPlaying;
        private System.Windows.Forms.Label labPlaying;
        private System.Windows.Forms.Button btnNewGame;
        private PieceListPanel pieceListPanelBlack;
        private PieceListPanel pieceListPanelWhite;
        private System.Windows.Forms.TextBox txbMoves;
        private System.Windows.Forms.Button btnWriteMoves;
        private System.Windows.Forms.Button btnClearMoves;
    }
}

