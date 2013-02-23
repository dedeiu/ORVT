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
        public Piece NextPiece = null;
        private int score = 0, speed = 1000 * 1;
        private ORVTForm GameForm = null;
        public const int RED = 1, BLUE = 2, YELLOW = 3, GREEN = 4, ORANGE = 5;
        private Timer timer = new Timer();
        private PieceFactory pieceFactory = null;

        #endregion

        #region Public Methods

        public void Init(ORVTForm form)
        {
            if (this.GameForm == null)
            {
                this.pieceFactory = new PieceFactory(this);

                GameScore = form.score;
                GameScore.Text = this.score.ToString();
                GameForm = form;
                this.timer.Tick += new EventHandler(timerTick);
                this.timer.Interval = speed;
                this.timer.Enabled = true;
                this.timer.Stop();
            }
        }

        public void Start()
        {
            if (this.GameForm == null)
            {
                throw new Exception("ORVT must be initialized first. Please use TetrisControls.Init(your_form_instance) method.");
            }

            Piece = null;
            NextPiece = null;
            SurfaceMatrix = new int[SURFACE_COLS, SURFACE_ROWS];
            this.score = 0;
            this.createGraphics(this.GameForm);

            this.timer.Start();
        }

        public void Stop()
        {
            Piece = null;
            NextPiece = null;
            this.timer.Stop();
        }

        public void Reset()
        {
            Stop();
            Start();
        }

        public bool IsRunning()
        {
            return (this.timer.Enabled == true ? true : false);
        }

        public void UpdatePiecePosition(Keys key, ORVTForm form)
        {
            Piece.ChangePosition(key);
            this.createGraphics(form);
        }

        #endregion

        #region Private Methods

        private void timerTick(object sender, EventArgs e)
        {
            if (Piece != null)
            {
                UpdatePiecePosition(Keys.Down, this.GameForm);
            }
        }

        private void createGraphics(ORVTForm form)
        {
            this.draw(form.gameSurface);
            this.drawNextPiece(form.nextPiecePanel);
        }

        private void draw(Panel GamePanel)
        {
            GameSurface = GamePanel.CreateGraphics();
            GameSurface.Clear(this.GameSurfaceColor);
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
            GameSurface.Dispose();
        }

        private void drawNextPiece(Panel nextPiecePanel)
        {
            if (NextPiece == null) return;
            GameSurface = nextPiecePanel.CreateGraphics();
            GameSurface.Clear(this.GameSurfaceColor);

            int blockWidth = Convert.ToInt32(GameSurface.VisibleClipBounds.Width / 4);
            int blockHeight = Convert.ToInt32(GameSurface.VisibleClipBounds.Height / 4);

            Pen pen = new Pen(Color.Black, 1);
            Color colorObj = this.getColor(NextPiece.Color);
            Brush brush = new SolidBrush(colorObj);

            foreach (PieceCoordonatesStructure pCoords in NextPiece.PieceCoordonates.CoordonateList)
            {
                Rectangle block = new Rectangle();
                block.X = (pCoords.X - 5) * blockWidth;
                block.Y = pCoords.Y * blockHeight;
                block.Width = blockWidth;
                block.Height = blockHeight;
                this.GameSurface.FillRectangle(brush, block);
                this.GameSurface.DrawRectangle(pen, block);
            }

            GameSurface.Dispose();
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
            if (NextPiece == null)
            {
                NextPiece = this.pieceFactory.GetRandomPiece();
            }

            if (Piece == null)
            {
                Piece = NextPiece;
                NextPiece = this.pieceFactory.GetRandomPiece();
                int deltaY = (Piece.PieceCoordonates.Top + Piece.PieceCoordonates.PieceHeight);
                int deltaX = (Piece.PieceCoordonates.Left + Piece.PieceCoordonates.PieceWidth);
                if (Piece.WillCollide(Piece.PieceCoordonates.CoordonateList, deltaX, deltaY, this, false))
                {
                    this.Stop();
                }
                else
                {
                    Piece.UpdatePieceOnTetrisBoard(Piece.Color);
                }
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
