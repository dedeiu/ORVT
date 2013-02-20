namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.gameSurface = new System.Windows.Forms.Panel();
            this.mainCycle = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameSurface
            // 
            this.gameSurface.Location = new System.Drawing.Point(12, 12);
            this.gameSurface.Name = "gameSurface";
            this.gameSurface.Size = new System.Drawing.Size(300, 480);
            this.gameSurface.TabIndex = 0;
            this.gameSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.gameSurface_Paint);
            // 
            // mainCycle
            // 
            this.mainCycle.Enabled = true;
            this.mainCycle.Interval = 1500;
            this.mainCycle.Tick += new System.EventHandler(this.mainCycle_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Score:";
            // 
            // score
            // 
            this.score.AccessibleName = "score";
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(372, 12);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(13, 13);
            this.score.TabIndex = 2;
            this.score.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 518);
            this.Controls.Add(this.score);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameSurface);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel gameSurface;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label score;
        public System.Windows.Forms.Timer mainCycle;
    }
}

