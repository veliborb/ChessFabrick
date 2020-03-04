namespace ChessFabrickFormsApp
{
    partial class GameSelectorForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbNewGames = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbActiveGames = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnSpectate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.grbColor = new System.Windows.Forms.GroupBox();
            this.rdbBlack = new System.Windows.Forms.RadioButton();
            this.rdbWhite = new System.Windows.Forms.RadioButton();
            this.txbGameId = new System.Windows.Forms.TextBox();
            this.lbYourGames = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRejoin = new System.Windows.Forms.Button();
            this.txbMessages = new System.Windows.Forms.TextBox();
            this.grbColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "New games";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbNewGames
            // 
            this.lbNewGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbNewGames.FormattingEnabled = true;
            this.lbNewGames.Location = new System.Drawing.Point(12, 38);
            this.lbNewGames.Name = "lbNewGames";
            this.lbNewGames.Size = new System.Drawing.Size(256, 212);
            this.lbNewGames.TabIndex = 1;
            this.lbNewGames.SelectedIndexChanged += new System.EventHandler(this.lbNewGames_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(274, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Active games";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbActiveGames
            // 
            this.lbActiveGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbActiveGames.FormattingEnabled = true;
            this.lbActiveGames.Location = new System.Drawing.Point(274, 38);
            this.lbActiveGames.Name = "lbActiveGames";
            this.lbActiveGames.Size = new System.Drawing.Size(256, 238);
            this.lbActiveGames.TabIndex = 3;
            this.lbActiveGames.SelectedIndexChanged += new System.EventHandler(this.lbActiveGames_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(715, 363);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnJoin.Enabled = false;
            this.btnJoin.Location = new System.Drawing.Point(193, 282);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 5;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnSpectate
            // 
            this.btnSpectate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpectate.Enabled = false;
            this.btnSpectate.Location = new System.Drawing.Point(455, 282);
            this.btnSpectate.Name = "btnSpectate";
            this.btnSpectate.Size = new System.Drawing.Size(75, 23);
            this.btnSpectate.TabIndex = 6;
            this.btnSpectate.Text = "Spectate";
            this.btnSpectate.UseVisualStyleBackColor = true;
            this.btnSpectate.Click += new System.EventHandler(this.btnSpectate_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(77, 39);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // grbColor
            // 
            this.grbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbColor.Controls.Add(this.rdbBlack);
            this.grbColor.Controls.Add(this.btnCreate);
            this.grbColor.Controls.Add(this.rdbWhite);
            this.grbColor.Location = new System.Drawing.Point(12, 316);
            this.grbColor.Name = "grbColor";
            this.grbColor.Size = new System.Drawing.Size(158, 70);
            this.grbColor.TabIndex = 9;
            this.grbColor.TabStop = false;
            this.grbColor.Text = "Create game";
            // 
            // rdbBlack
            // 
            this.rdbBlack.AutoSize = true;
            this.rdbBlack.Location = new System.Drawing.Point(6, 42);
            this.rdbBlack.Name = "rdbBlack";
            this.rdbBlack.Size = new System.Drawing.Size(52, 17);
            this.rdbBlack.TabIndex = 1;
            this.rdbBlack.TabStop = true;
            this.rdbBlack.Text = "Black";
            this.rdbBlack.UseVisualStyleBackColor = true;
            // 
            // rdbWhite
            // 
            this.rdbWhite.AutoSize = true;
            this.rdbWhite.Checked = true;
            this.rdbWhite.Location = new System.Drawing.Point(6, 19);
            this.rdbWhite.Name = "rdbWhite";
            this.rdbWhite.Size = new System.Drawing.Size(53, 17);
            this.rdbWhite.TabIndex = 0;
            this.rdbWhite.TabStop = true;
            this.rdbWhite.Text = "White";
            this.rdbWhite.UseVisualStyleBackColor = true;
            // 
            // txbGameId
            // 
            this.txbGameId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbGameId.Location = new System.Drawing.Point(12, 256);
            this.txbGameId.Name = "txbGameId";
            this.txbGameId.Size = new System.Drawing.Size(256, 20);
            this.txbGameId.TabIndex = 10;
            this.txbGameId.TextChanged += new System.EventHandler(this.txbGameId_TextChanged);
            // 
            // lbYourGames
            // 
            this.lbYourGames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbYourGames.FormattingEnabled = true;
            this.lbYourGames.Location = new System.Drawing.Point(536, 38);
            this.lbYourGames.Name = "lbYourGames";
            this.lbYourGames.Size = new System.Drawing.Size(256, 238);
            this.lbYourGames.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(536, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Your games";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRejoin
            // 
            this.btnRejoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRejoin.Enabled = false;
            this.btnRejoin.Location = new System.Drawing.Point(717, 282);
            this.btnRejoin.Name = "btnRejoin";
            this.btnRejoin.Size = new System.Drawing.Size(75, 23);
            this.btnRejoin.TabIndex = 13;
            this.btnRejoin.Text = "Rejoin";
            this.btnRejoin.UseVisualStyleBackColor = true;
            this.btnRejoin.Click += new System.EventHandler(this.btnRejoin_Click);
            // 
            // txbMessages
            // 
            this.txbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMessages.Location = new System.Drawing.Point(176, 316);
            this.txbMessages.Multiline = true;
            this.txbMessages.Name = "txbMessages";
            this.txbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txbMessages.Size = new System.Drawing.Size(533, 70);
            this.txbMessages.TabIndex = 14;
            // 
            // GameSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 398);
            this.Controls.Add(this.txbMessages);
            this.Controls.Add(this.btnRejoin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbYourGames);
            this.Controls.Add(this.txbGameId);
            this.Controls.Add(this.grbColor);
            this.Controls.Add(this.btnSpectate);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lbActiveGames);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbNewGames);
            this.Controls.Add(this.label1);
            this.Name = "GameSelectorForm";
            this.Text = "GameSelectorForm";
            this.Load += new System.EventHandler(this.GameSelectorForm_Load);
            this.grbColor.ResumeLayout(false);
            this.grbColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbNewGames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbActiveGames;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnSpectate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox grbColor;
        private System.Windows.Forms.RadioButton rdbBlack;
        private System.Windows.Forms.RadioButton rdbWhite;
        private System.Windows.Forms.TextBox txbGameId;
        private System.Windows.Forms.ListBox lbYourGames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRejoin;
        private System.Windows.Forms.TextBox txbMessages;
    }
}