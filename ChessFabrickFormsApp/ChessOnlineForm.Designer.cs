﻿namespace ChessFabrickFormsApp
{
    partial class ChessOnlineForm
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
            this.labPlaying = new System.Windows.Forms.Label();
            this.pieceListPanelWhite = new ChessFabrickFormsApp.PieceListPanel();
            this.pieceListPanelBlack = new ChessFabrickFormsApp.PieceListPanel();
            this.cfbPlaying = new ChessFabrickFormsApp.ChessFieldBox();
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
            // ChessOnlineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 634);
            this.Controls.Add(this.pieceListPanelWhite);
            this.Controls.Add(this.pieceListPanelBlack);
            this.Controls.Add(this.labPlaying);
            this.Controls.Add(this.cfbPlaying);
            this.Controls.Add(this.panTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChessOnlineForm";
            this.Text = "ChessFabrick";
            this.Load += new System.EventHandler(this.ChessForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTable;
        private ChessFieldBox cfbPlaying;
        private System.Windows.Forms.Label labPlaying;
        private PieceListPanel pieceListPanelBlack;
        private PieceListPanel pieceListPanelWhite;
    }
}
