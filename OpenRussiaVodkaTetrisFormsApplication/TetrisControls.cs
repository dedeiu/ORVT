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
        # region Properties

        private Graphics GameSurface = null;
        private Label GameScore = null;
        private Color GameSurfaceColor = Color.Black;
        public const int SURFACE_COLS = 10, SURFACE_ROWS = 20;
        public int[,] SurfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
        public Piece Piece = null;
        private int score = 0, speed = 1000 * 1;
        private ORVTForm tForm = null;
        public const int RED = 1, BLUE = 2, YELLOW = 3, GREEN = 4, ORANGE = 5;
        private Timer timer = new Timer();

        #endregion

        #region Public Methods

        public void Init(ORVTForm form)
        {
            if (this.GameSurface == null)
            {
                createGraphics(form);
                this.timer.Tick += new EventHandler(timerTick);
                this.timer.Interval = speed;
                this.timer.Enabled = true;
                this.timer.Stop();
            }
        }

        public void Start()
        {
            if (this.GameSurface == null)
            {
                throw new Exception("ORVT must be initialized first. Please use TetrisControls.Init(your_form_instance) method.");
            }

            PieceFactory pieceFactory = new PieceFactory(this);
            this.Piece = pieceFactory.GetRandomPiece();

            this.timer.Start();

            createGraphics(this.tForm);
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Reset()
        {
            Stop();
            this.SurfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
            this.score = 0;
            Start();
        }

        public bool IsRunning()
        {
            return (this.timer.Enabled == true ? true : false);
        }

        public void UpdatePiecePosition(Keys key, ORVTForm form)
        {
            this.checkTetrisPiece();
            Piece.ChangePosition(key);
            this.createGraphics(form);
        }

        #endregion

        #region Private Methods

        private void timerTick(object sender, EventArgs e)
        {
            UpdatePiecePosition(Keys.Down, this.tForm);
        }

        private void createGraphics(ORVTForm form)
        {
            this.GameSurface = form.gameSurface.CreateGraphics();
            this.GameScore = form.score; 
            this.GameScore.Text = this.score.ToString(); 
            this.tForm = form;
            this.GameSurface.Clear(this.GameSurfaceColor);
            this.draw();
            this.GameSurface.Dispose();
        }

        private void draw()
        {
            if (!pieceCanMove())
            {
                while (this.verifyRows())
                {
                    this.score += 10 * this.speed / 100;
                };
                this.Piece = null;
            }
            this.GameScore.Text = score.ToString();

            this.checkTetrisPiece();

            Pen pen = new Pen(Color.Black, 1);
            Color colorObj = this.getColor(TetrisControls.RED);
            Brush brush = new SolidBrush(colorObj);

            int blockWidth = Convert.ToInt32(GameSurface.VisibleClipBounds.Width / SURFACE_COLS);
            int blockHeight = Convert.ToInt32(GameSurface.VisibleClipBounds.Height / SURFACE_ROWS);

            for (int col = 0; col < SURFACE_COLS; col++)
            {
                for (int row = 0; row < SURFACE_ROWS; row++)
                {
                    if (SurfaceMatrix[col, row] > 0)
                    {

                        colorObj = this.getColor(SurfaceMatrix[col, row]);
                        brush = new SolidBrush(colorObj);

                        Rectangle block = new Rectangle();
                        block.X = col * blockWidth;
                        block.Y = row * blockHeight;
                        block.Width = blockWidth;
                        block.Height = blockHeight;
                        this.GameSurface.FillRectangle(brush, block);
                        this.GameSurface.DrawRectangle(pen, block);
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
                    if (this.SurfaceMatrix[col, row] > 0)
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
                        this.SurfaceMatrix[col, row] = this.SurfaceMatrix[col, (row - 1)];
                    }
                    else
                    {
                        this.SurfaceMatrix[col, row] = 0;
                    }
                }
            }
        }

        private void checkTetrisPiece()
        {
            if (Piece == null)
            {
                PieceFactory pieceFactory = new PieceFactory(this);
                Piece = pieceFactory.GetRandomPiece();
            }
        }

        private bool pieceCanMove()
        {
            
            if (Piece != null)
            {
                var coordObj = Piece.PieceCoordonates.CoordonateList.Find(coord => coord.Y == SURFACE_ROWS);
                if (coordObj.Y == 0 && coordObj.Y != SURFACE_ROWS)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        #endregion
    }
}
