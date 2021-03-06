﻿using System;
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
        private TetrisControls tetris = new TetrisControls();

        public ORVTForm()
        {
            InitializeComponent();
            GameMenu.BringToFront();
            tetris.Init(this);
            score.Visible = false;
            scorelabel.Visible = false;
        }

        private void OVRTForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (tetris.IsRunning())
            {
                tetris.UpdatePiecePosition(e.KeyCode, this);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tetris.IsRunning())
            {
                tetris.Pause();
                if (MessageBox.Show("Are you sure you want to Start a new game ? This will Reset your current game progress. ", "Confirm new game", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 0, false) == DialogResult.Yes)
                {
                    tetris.Reset();
                }
                else
                {
                    tetris.Resume();
                }
            }
            else
            {
                score.Visible = true;
                scorelabel.Visible = true;
                tetris.Start();
            }
        }
    }
}
