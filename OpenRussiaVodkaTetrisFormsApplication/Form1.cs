﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private TetrisControls tetris = new TetrisControls();

        public Form1()
        {
            InitializeComponent();
        }

        private void gameSurface_Paint(object sender, PaintEventArgs e)
        {

            tetris.useGraphics(this);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            tetris.piece.changePosition(e.KeyCode);
            tetris.useGraphics(this);
        }

        private void mainCycle_Tick(object sender, EventArgs e)
        {
            tetris.piece.changePosition(Keys.Down);
            tetris.useGraphics(this);
        }
    }
}
