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
            this.txbMessage = new System.Windows.Forms.TextBox();
            this.labPlayerNames = new System.Windows.Forms.Label();
            this.btnAddBot = new System.Windows.Forms.Button();
            this.grbPlaying = new System.Windows.Forms.GroupBox();
            this.cfbPlaying = new ChessFabrickFormsApp.ChessFieldBox();
            this.grbPlayerColor = new System.Windows.Forms.GroupBox();
            this.cfbPlayerColor = new ChessFabrickFormsApp.ChessFieldBox();
            this.labLastMove = new System.Windows.Forms.Label();
            this.pieceListPanelWhite = new ChessFabrickFormsApp.PieceListPanel();
            this.pieceListPanelBlack = new ChessFabrickFormsApp.PieceListPanel();
            this.grbPlaying.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).BeginInit();
            this.grbPlayerColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlayerColor)).BeginInit();
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
            // txbMessage
            // 
            this.txbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMessage.Location = new System.Drawing.Point(462, 143);
            this.txbMessage.Multiline = true;
            this.txbMessage.Name = "txbMessage";
            this.txbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txbMessage.Size = new System.Drawing.Size(151, 396);
            this.txbMessage.TabIndex = 11;
            // 
            // labPlayerNames
            // 
            this.labPlayerNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labPlayerNames.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labPlayerNames.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labPlayerNames.Location = new System.Drawing.Point(462, 545);
            this.labPlayerNames.Name = "labPlayerNames";
            this.labPlayerNames.Size = new System.Drawing.Size(151, 77);
            this.labPlayerNames.TabIndex = 13;
            this.labPlayerNames.Text = "White:\r\nBlack:";
            // 
            // btnAddBot
            // 
            this.btnAddBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBot.Location = new System.Drawing.Point(462, 545);
            this.btnAddBot.Name = "btnAddBot";
            this.btnAddBot.Size = new System.Drawing.Size(151, 77);
            this.btnAddBot.TabIndex = 14;
            this.btnAddBot.Text = "/addbot";
            this.btnAddBot.UseVisualStyleBackColor = true;
            this.btnAddBot.Click += new System.EventHandler(this.btnAddBot_Click);
            // 
            // grbPlaying
            // 
            this.grbPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbPlaying.Controls.Add(this.cfbPlaying);
            this.grbPlaying.Location = new System.Drawing.Point(462, 12);
            this.grbPlaying.Name = "grbPlaying";
            this.grbPlaying.Size = new System.Drawing.Size(75, 77);
            this.grbPlaying.TabIndex = 15;
            this.grbPlaying.TabStop = false;
            // 
            // cfbPlaying
            // 
            this.cfbPlaying.Location = new System.Drawing.Point(6, 21);
            this.cfbPlaying.Name = "cfbPlaying";
            this.cfbPlaying.Size = new System.Drawing.Size(63, 50);
            this.cfbPlaying.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cfbPlaying.Style = ChessFabrickFormsApp.ChessFieldBox.BoxStyle.None;
            this.cfbPlaying.TabIndex = 4;
            this.cfbPlaying.TabStop = false;
            // 
            // grbPlayerColor
            // 
            this.grbPlayerColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbPlayerColor.Controls.Add(this.cfbPlayerColor);
            this.grbPlayerColor.Location = new System.Drawing.Point(538, 12);
            this.grbPlayerColor.Name = "grbPlayerColor";
            this.grbPlayerColor.Size = new System.Drawing.Size(75, 77);
            this.grbPlayerColor.TabIndex = 16;
            this.grbPlayerColor.TabStop = false;
            // 
            // cfbPlayerColor
            // 
            this.cfbPlayerColor.Location = new System.Drawing.Point(6, 21);
            this.cfbPlayerColor.Name = "cfbPlayerColor";
            this.cfbPlayerColor.Size = new System.Drawing.Size(63, 50);
            this.cfbPlayerColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cfbPlayerColor.Style = ChessFabrickFormsApp.ChessFieldBox.BoxStyle.None;
            this.cfbPlayerColor.TabIndex = 10;
            this.cfbPlayerColor.TabStop = false;
            // 
            // labLastMove
            // 
            this.labLastMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labLastMove.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labLastMove.Location = new System.Drawing.Point(462, 95);
            this.labLastMove.Name = "labLastMove";
            this.labLastMove.Size = new System.Drawing.Size(151, 45);
            this.labLastMove.TabIndex = 17;
            this.labLastMove.Text = "Waiting...";
            this.labLastMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labLastMove.MouseEnter += new System.EventHandler(this.labLastMove_MouseEnter);
            this.labLastMove.MouseLeave += new System.EventHandler(this.labLastMove_MouseLeave);
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
            // ChessOnlineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 634);
            this.Controls.Add(this.labLastMove);
            this.Controls.Add(this.btnAddBot);
            this.Controls.Add(this.grbPlayerColor);
            this.Controls.Add(this.grbPlaying);
            this.Controls.Add(this.labPlayerNames);
            this.Controls.Add(this.txbMessage);
            this.Controls.Add(this.pieceListPanelWhite);
            this.Controls.Add(this.pieceListPanelBlack);
            this.Controls.Add(this.panTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChessOnlineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChessFabrick";
            this.Load += new System.EventHandler(this.ChessForm_Load);
            this.grbPlaying.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlaying)).EndInit();
            this.grbPlayerColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfbPlayerColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panTable;
        private ChessFieldBox cfbPlaying;
        private PieceListPanel pieceListPanelBlack;
        private PieceListPanel pieceListPanelWhite;
        private ChessFieldBox cfbPlayerColor;
        private System.Windows.Forms.TextBox txbMessage;
        private System.Windows.Forms.Label labPlayerNames;
        private System.Windows.Forms.Button btnAddBot;
        private System.Windows.Forms.GroupBox grbPlaying;
        private System.Windows.Forms.GroupBox grbPlayerColor;
        private System.Windows.Forms.Label labLastMove;
    }
}

