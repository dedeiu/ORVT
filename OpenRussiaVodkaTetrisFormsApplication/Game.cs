using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class TetrisControls
    {
        private Graphics tSurface = null;
        private Label tScore = null;
        private Color tSurfaceColor = Color.Black;
        public const int SURFACE_COLS = 10;
        public const int SURFACE_ROWS = 20;
        public int[,] surfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
        public tPiece piece = null;
        private int score = 0, speed = 1;
        private Form1 tForm = null;
        public const int RED = 1, BLUE = 2, YELLOW = 3, GREEN = 4, ORANGE = 5;

        public void useGraphics(Form1 form)
        {
            this.tSurface = form.gameSurface.CreateGraphics();
            this.tScore = form.score; this.tScore.Text = score.ToString(); this.tForm = form;
            this.tSurface.Clear(this.tSurfaceColor);
            this.draw();
            this.tSurface.Dispose();
        }

        /* this method draws the surface matrix on board panel */
        public void draw()
        {
            /* for every full line, update score */
            while (this.verifyRows())
            {
                this.piece = null;
                score += 10 * speed;
            };
            this.tScore.Text = score.ToString();

            /* check the satus of current piece from board */
            this.checkCurrentPiece();

            /* get color objects for the board */
            Pen pen = new Pen(Color.Black, 1);
            Color colorObj = this.getColor(TetrisControls.RED);
            Brush brush = new SolidBrush(colorObj);

            /* preserve width and height from piece ratio */
            int blockWidth = Convert.ToInt32(tSurface.VisibleClipBounds.Width / SURFACE_COLS);
            int blockHeight = Convert.ToInt32(tSurface.VisibleClipBounds.Height / SURFACE_ROWS);

            /* populate tetris board with necessary data */
            for (int col = 0; col < SURFACE_COLS; col++)
            {
                for (int row = 0; row < SURFACE_ROWS; row++)
                {
                    if (surfaceMatrix[col, row] > 0)
                    {
                        /* set color */
                        colorObj = this.getColor(surfaceMatrix[col, row]);
                        brush = new SolidBrush(colorObj);
                        /* build block */
                        Rectangle block = new Rectangle();
                        block.X = col * blockWidth;
                        block.Y = row * blockHeight;
                        block.Width = blockWidth;
                        block.Height = blockHeight;
                        this.tSurface.FillRectangle(brush, block);
                        this.tSurface.DrawRectangle(pen, block);
                    }
                }
            }
        }

        /* get color index */
        private Color getColor(int color)
        {
            Color colorObj = Color.Red;
            switch (color) 
            {
                case 1:
                    colorObj = Color.Red;
                    break;
                case 2:
                    colorObj = Color.Blue;
                    break;
                case 3:
                    colorObj = Color.Yellow;
                    break;
                case 4:
                    colorObj = Color.Green;
                    break;
                case 5:
                    colorObj = Color.Orange;
                    break;
            }
            return colorObj;
        }

        /* check for each line. if the sum of all block from current line equal with board lenght then call removeStreightLine() */
        private bool verifyRows()
        {

            for (int row = 0; row < SURFACE_ROWS; row++)
            {
                int colSum = 0;
                for (int col = 0; col < SURFACE_COLS; col++)
                {
                    if (this.surfaceMatrix[col, row] > 0)
                    {
                        colSum++;
                    }
                }
                if (colSum == SURFACE_COLS)
                {
                    this.removeStreightLine(row);
                    return true;
                }
            }
            return false;
        }

        /* this method removes a full line from board */
        private void removeStreightLine(int currentRow)
        {
            for (int row = currentRow; row >= 0; row--)
            {
                for (int col = 0; col < SURFACE_COLS; col++)
                {
                    if ((row - 1) >= 0)
                    {
                        this.surfaceMatrix[col, row] = this.surfaceMatrix[col, (row - 1)];
                    }
                    else
                    {
                        this.surfaceMatrix[col, row] = 0;
                    }
                }
            }
        }

        /* this method checks if current piece exists on board */
        private void checkCurrentPiece()
        {
            if (piece == null)
            {
                tPieceFactory pieceFactory = new tPieceFactory(this);
                piece = pieceFactory.getRandomPiece();
            }
        }

        public void reset()
        {
            this.tForm.mainCycle.Enabled = false;
            this.surfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
            this.score = 0;
            tPieceFactory pieceFactory = new tPieceFactory(this);
            this.piece = pieceFactory.getRandomPiece();
            this.tForm.mainCycle.Start();
        }
    }
}
