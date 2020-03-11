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
            this.lbNewGames = new System.Windows.Forms.ListBox();
            this.lbActiveGames = new System.Windows.Forms.ListBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.grbColor = new System.Windows.Forms.GroupBox();
            this.rdbBots = new System.Windows.Forms.RadioButton();
            this.rdbBlack = new System.Windows.Forms.RadioButton();
            this.rdbWhite = new System.Windows.Forms.RadioButton();
            this.lbYourGames = new System.Windows.Forms.ListBox();
            this.txbMessages = new System.Windows.Forms.TextBox();
            this.bthHelp = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabNewGames = new System.Windows.Forms.TabPage();
            this.tabActiveGames = new System.Windows.Forms.TabPage();
            this.tabYourGames = new System.Windows.Forms.TabPage();
            this.tabSearchedGames = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.lbSearchedGames = new System.Windows.Forms.ListBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.grbColor.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabNewGames.SuspendLayout();
            this.tabActiveGames.SuspendLayout();
            this.tabYourGames.SuspendLayout();
            this.tabSearchedGames.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbNewGames
            // 
            this.lbNewGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNewGames.FormattingEnabled = true;
            this.lbNewGames.Location = new System.Drawing.Point(0, 0);
            this.lbNewGames.Name = "lbNewGames";
            this.lbNewGames.Size = new System.Drawing.Size(340, 290);
            this.lbNewGames.TabIndex = 1;
            this.lbNewGames.SelectedIndexChanged += new System.EventHandler(this.lbNewGames_SelectedIndexChanged);
            // 
            // lbActiveGames
            // 
            this.lbActiveGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbActiveGames.FormattingEnabled = true;
            this.lbActiveGames.Location = new System.Drawing.Point(0, 0);
            this.lbActiveGames.Name = "lbActiveGames";
            this.lbActiveGames.Size = new System.Drawing.Size(340, 290);
            this.lbActiveGames.TabIndex = 3;
            this.lbActiveGames.SelectedIndexChanged += new System.EventHandler(this.lbActiveGames_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 335);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(6, 89);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // grbColor
            // 
            this.grbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbColor.Controls.Add(this.rdbBots);
            this.grbColor.Controls.Add(this.rdbBlack);
            this.grbColor.Controls.Add(this.btnCreate);
            this.grbColor.Controls.Add(this.rdbWhite);
            this.grbColor.Location = new System.Drawing.Point(366, 12);
            this.grbColor.Name = "grbColor";
            this.grbColor.Size = new System.Drawing.Size(88, 118);
            this.grbColor.TabIndex = 4;
            this.grbColor.TabStop = false;
            this.grbColor.Text = "Create game";
            // 
            // rdbBots
            // 
            this.rdbBots.AutoSize = true;
            this.rdbBots.Location = new System.Drawing.Point(6, 65);
            this.rdbBots.Name = "rdbBots";
            this.rdbBots.Size = new System.Drawing.Size(63, 17);
            this.rdbBots.TabIndex = 2;
            this.rdbBots.TabStop = true;
            this.rdbBots.Text = "Bot only";
            this.rdbBots.UseVisualStyleBackColor = true;
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
            // lbYourGames
            // 
            this.lbYourGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbYourGames.FormattingEnabled = true;
            this.lbYourGames.Location = new System.Drawing.Point(0, 0);
            this.lbYourGames.Name = "lbYourGames";
            this.lbYourGames.Size = new System.Drawing.Size(340, 290);
            this.lbYourGames.TabIndex = 11;
            this.lbYourGames.SelectedIndexChanged += new System.EventHandler(this.lbYourGames_SelectedIndexChanged);
            // 
            // txbMessages
            // 
            this.txbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMessages.Location = new System.Drawing.Point(12, 364);
            this.txbMessages.Multiline = true;
            this.txbMessages.Name = "txbMessages";
            this.txbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txbMessages.Size = new System.Drawing.Size(348, 70);
            this.txbMessages.TabIndex = 6;
            // 
            // bthHelp
            // 
            this.bthHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bthHelp.Location = new System.Drawing.Point(379, 411);
            this.bthHelp.Name = "bthHelp";
            this.bthHelp.Size = new System.Drawing.Size(75, 23);
            this.bthHelp.TabIndex = 5;
            this.bthHelp.Text = "Help";
            this.bthHelp.UseVisualStyleBackColor = true;
            this.bthHelp.Click += new System.EventHandler(this.bthHelp_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabNewGames);
            this.tabControl.Controls.Add(this.tabActiveGames);
            this.tabControl.Controls.Add(this.tabYourGames);
            this.tabControl.Controls.Add(this.tabSearchedGames);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(348, 317);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabNewGames
            // 
            this.tabNewGames.Controls.Add(this.lbNewGames);
            this.tabNewGames.Location = new System.Drawing.Point(4, 22);
            this.tabNewGames.Name = "tabNewGames";
            this.tabNewGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewGames.Size = new System.Drawing.Size(340, 291);
            this.tabNewGames.TabIndex = 0;
            this.tabNewGames.Text = "New Games";
            this.tabNewGames.UseVisualStyleBackColor = true;
            // 
            // tabActiveGames
            // 
            this.tabActiveGames.Controls.Add(this.lbActiveGames);
            this.tabActiveGames.Location = new System.Drawing.Point(4, 22);
            this.tabActiveGames.Name = "tabActiveGames";
            this.tabActiveGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabActiveGames.Size = new System.Drawing.Size(340, 291);
            this.tabActiveGames.TabIndex = 1;
            this.tabActiveGames.Text = "Active Games";
            this.tabActiveGames.UseVisualStyleBackColor = true;
            // 
            // tabYourGames
            // 
            this.tabYourGames.Controls.Add(this.lbYourGames);
            this.tabYourGames.Location = new System.Drawing.Point(4, 22);
            this.tabYourGames.Name = "tabYourGames";
            this.tabYourGames.Size = new System.Drawing.Size(340, 291);
            this.tabYourGames.TabIndex = 2;
            this.tabYourGames.Text = "Your Games";
            this.tabYourGames.UseVisualStyleBackColor = true;
            // 
            // tabSearchedGames
            // 
            this.tabSearchedGames.Controls.Add(this.label1);
            this.tabSearchedGames.Controls.Add(this.btnSearch);
            this.tabSearchedGames.Controls.Add(this.txbSearch);
            this.tabSearchedGames.Controls.Add(this.lbSearchedGames);
            this.tabSearchedGames.Location = new System.Drawing.Point(4, 22);
            this.tabSearchedGames.Name = "tabSearchedGames";
            this.tabSearchedGames.Size = new System.Drawing.Size(340, 291);
            this.tabSearchedGames.TabIndex = 3;
            this.tabSearchedGames.Text = "Search";
            this.tabSearchedGames.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Player name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(265, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txbSearch
            // 
            this.txbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSearch.Location = new System.Drawing.Point(77, 3);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(182, 20);
            this.txbSearch.TabIndex = 0;
            this.txbSearch.TextChanged += new System.EventHandler(this.txbSearch_TextChanged);
            this.txbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbSearch_KeyDown);
            // 
            // lbSearchedGames
            // 
            this.lbSearchedGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearchedGames.FormattingEnabled = true;
            this.lbSearchedGames.Location = new System.Drawing.Point(0, 26);
            this.lbSearchedGames.Name = "lbSearchedGames";
            this.lbSearchedGames.Size = new System.Drawing.Size(340, 264);
            this.lbSearchedGames.TabIndex = 2;
            this.lbSearchedGames.SelectedIndexChanged += new System.EventHandler(this.lbSearch_SelectedIndexChanged);
            // 
            // btnJoin
            // 
            this.btnJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJoin.Location = new System.Drawing.Point(285, 335);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 23);
            this.btnJoin.TabIndex = 2;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // GameSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 446);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.bthHelp);
            this.Controls.Add(this.txbMessages);
            this.Controls.Add(this.grbColor);
            this.Controls.Add(this.btnRefresh);
            this.Name = "GameSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GameSelectorForm";
            this.Load += new System.EventHandler(this.GameSelectorForm_Load);
            this.grbColor.ResumeLayout(false);
            this.grbColor.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabNewGames.ResumeLayout(false);
            this.tabActiveGames.ResumeLayout(false);
            this.tabYourGames.ResumeLayout(false);
            this.tabSearchedGames.ResumeLayout(false);
            this.tabSearchedGames.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbNewGames;
        private System.Windows.Forms.ListBox lbActiveGames;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox grbColor;
        private System.Windows.Forms.RadioButton rdbBlack;
        private System.Windows.Forms.RadioButton rdbWhite;
        private System.Windows.Forms.ListBox lbYourGames;
        private System.Windows.Forms.TextBox txbMessages;
        private System.Windows.Forms.Button bthHelp;
        private System.Windows.Forms.RadioButton rdbBots;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabNewGames;
        private System.Windows.Forms.TabPage tabActiveGames;
        private System.Windows.Forms.TabPage tabYourGames;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.TabPage tabSearchedGames;
        private System.Windows.Forms.ListBox lbSearchedGames;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Label label1;
    }
}