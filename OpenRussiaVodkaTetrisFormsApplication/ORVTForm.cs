using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ORVT
{
    public partial class ORVTForm : Form
    {
        private TetrisControls tetris;

        public ORVTForm()
        {
            InitializeComponent();
            GameMenu.BringToFront();
        }

        private void gameSurface_Paint(object sender, PaintEventArgs e)
        {
            if (tetris != null)
            {
                tetris.useGraphics(this);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tetris != null)
            {
                tetris.updatePiecePosition(e.KeyCode, this);
            }
        }

        private void mainCycle_Tick(object sender, EventArgs e)
        {
            if (tetris != null)
            {
                //tetris.updatePiecePosition(Keys.Down, this);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tetris == null)
            {
                tetris = new TetrisControls();
                tetris.updatePiecePosition(Keys.Down, this);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to start a new game ? This will reset your current game progress. ", "Confirm new game", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {
                    tetris.reset();
                    tetris.useGraphics(this);
                }
            }
        }
    }
}
