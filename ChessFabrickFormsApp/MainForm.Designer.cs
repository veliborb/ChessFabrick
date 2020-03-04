namespace ChessFabrickFormsApp
{
    partial class MainForm
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
            this.btnOfflineChess = new System.Windows.Forms.Button();
            this.btnSockets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOfflineChess
            // 
            this.btnOfflineChess.Location = new System.Drawing.Point(12, 12);
            this.btnOfflineChess.Name = "btnOfflineChess";
            this.btnOfflineChess.Size = new System.Drawing.Size(340, 23);
            this.btnOfflineChess.TabIndex = 0;
            this.btnOfflineChess.Text = "Offline Chess";
            this.btnOfflineChess.UseVisualStyleBackColor = true;
            this.btnOfflineChess.Click += new System.EventHandler(this.btnOfflineChess_Click);
            // 
            // btnSockets
            // 
            this.btnSockets.Location = new System.Drawing.Point(12, 41);
            this.btnSockets.Name = "btnSockets";
            this.btnSockets.Size = new System.Drawing.Size(340, 23);
            this.btnSockets.TabIndex = 1;
            this.btnSockets.Text = "SocketMan";
            this.btnSockets.UseVisualStyleBackColor = true;
            this.btnSockets.Click += new System.EventHandler(this.btnSockets_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 398);
            this.Controls.Add(this.btnSockets);
            this.Controls.Add(this.btnOfflineChess);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOfflineChess;
        private System.Windows.Forms.Button btnSockets;
    }
}