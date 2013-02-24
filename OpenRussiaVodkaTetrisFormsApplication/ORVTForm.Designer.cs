namespace ORVT
{
    partial class ORVTForm
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
            this.scorelabel = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.GameMenu = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPiecePanel = new System.Windows.Forms.Panel();
            this.gameOverLabel = new System.Windows.Forms.Label();
            this.gameSurface = new System.Windows.Forms.Panel();
            this.GameMenu.SuspendLayout();
            this.gameSurface.SuspendLayout();
            this.SuspendLayout();
            // 
            // scorelabel
            // 
            this.scorelabel.AccessibleName = "scoretext";
            this.scorelabel.AutoSize = true;
            this.scorelabel.Location = new System.Drawing.Point(318, 130);
            this.scorelabel.Name = "scorelabel";
            this.scorelabel.Size = new System.Drawing.Size(38, 13);
            this.scorelabel.TabIndex = 1;
            this.scorelabel.Text = "Score:";
            // 
            // score
            // 
            this.score.AccessibleName = "scorelabel";
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(362, 130);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(13, 13);
            this.score.TabIndex = 2;
            this.score.Text = "0";
            // 
            // GameMenu
            // 
            this.GameMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.GameMenu.Location = new System.Drawing.Point(0, 0);
            this.GameMenu.Name = "GameMenu";
            this.GameMenu.Size = new System.Drawing.Size(736, 24);
            this.GameMenu.TabIndex = 3;
            this.GameMenu.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // nextPiecePanel
            // 
            this.nextPiecePanel.Location = new System.Drawing.Point(318, 27);
            this.nextPiecePanel.Name = "nextPiecePanel";
            this.nextPiecePanel.Size = new System.Drawing.Size(106, 100);
            this.nextPiecePanel.TabIndex = 4;
            // 
            // gameOverLabel
            // 
            this.gameOverLabel.Enabled = false;
            this.gameOverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameOverLabel.ForeColor = System.Drawing.Color.White;
            this.gameOverLabel.Location = new System.Drawing.Point(0, 171);
            this.gameOverLabel.Name = "gameOverLabel";
            this.gameOverLabel.Size = new System.Drawing.Size(300, 100);
            this.gameOverLabel.TabIndex = 1;
            this.gameOverLabel.Text = "Game Over";
            // 
            // gameSurface
            // 
            this.gameSurface.Controls.Add(this.gameOverLabel);
            this.gameSurface.Location = new System.Drawing.Point(12, 26);
            this.gameSurface.Name = "gameSurface";
            this.gameSurface.Size = new System.Drawing.Size(300, 480);
            this.gameSurface.TabIndex = 0;
            // 
            // ORVTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 518);
            this.Controls.Add(this.nextPiecePanel);
            this.Controls.Add(this.score);
            this.Controls.Add(this.scorelabel);
            this.Controls.Add(this.gameSurface);
            this.Controls.Add(this.GameMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.GameMenu;
            this.MaximizeBox = false;
            this.Name = "ORVTForm";
            this.Text = "ORVTForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OVRTForm_KeyDown);
            this.GameMenu.ResumeLayout(false);
            this.GameMenu.PerformLayout();
            this.gameSurface.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label scorelabel;
        public System.Windows.Forms.Label score;
        private System.Windows.Forms.MenuStrip GameMenu;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.Panel nextPiecePanel;
        public System.Windows.Forms.Label gameOverLabel;
        public System.Windows.Forms.Panel gameSurface;
    }
}

