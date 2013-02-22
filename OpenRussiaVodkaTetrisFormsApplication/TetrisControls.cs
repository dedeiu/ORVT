using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace ORVT
{
    class TetrisControls
    {
        private Graphics tSurface = null;
        private Label tScore = null;
        private Color tSurfaceColor = Color.Black;
        public const int SURFACE_COLS = 10, SURFACE_ROWS = 20;
        public int[,] surfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
        public tPiece piece = null;
        private int score = 0, speed = 1;
        private ORVTForm tForm = null;
        public const int RED = 1, BLUE = 2, YELLOW = 3, GREEN = 4, ORANGE = 5;

        public void useGraphics(ORVTForm form)
        {
            this.tSurface = form.gameSurface.CreateGraphics();
            this.tScore = form.score; this.tScore.Text = score.ToString(); this.tForm = form;
            this.tSurface.Clear(this.tSurfaceColor);
            this.draw();
            this.tSurface.Dispose();
        }

        public void draw()
        {
            if (!pieceCanMove())
            {
                while (this.verifyRows())
                {
                    this.piece = null;
                    score += 10 * speed;
                };
            }
            this.tScore.Text = score.ToString();

            this.checkCurrentPiece();

            Pen pen = new Pen(Color.Black, 1);
            Color colorObj = this.getColor(TetrisControls.RED);
            Brush brush = new SolidBrush(colorObj);

            int blockWidth = Convert.ToInt32(tSurface.VisibleClipBounds.Width / SURFACE_COLS);
            int blockHeight = Convert.ToInt32(tSurface.VisibleClipBounds.Height / SURFACE_ROWS);

            for (int col = 0; col < SURFACE_COLS; col++)
            {
                for (int row = 0; row < SURFACE_ROWS; row++)
                {
                    if (surfaceMatrix[col, row] > 0)
                    {

                        colorObj = this.getColor(surfaceMatrix[col, row]);
                        brush = new SolidBrush(colorObj);

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

        private void checkCurrentPiece()
        {
            if (piece == null)
            {
                tPieceFactory pieceFactory = new tPieceFactory(this);
                piece = pieceFactory.getRandomPiece();
            }
        }

        private bool pieceCanMove()
        {
            if (piece != null && piece.tPieceCoordonates.coordonateList.Find(coord => coord.Y != SURFACE_ROWS).Y != null)
            {
                return true;
            }
            return false;
        }

        public void updatePiecePosition(Keys key, ORVTForm form)
        {
            checkCurrentPiece();
            piece.changePosition(key);
            useGraphics(form);
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
