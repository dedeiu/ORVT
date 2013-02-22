using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ORVT
{
    class tPieceCoordonates
    {
        #region Properties

        public List<pCoords> coordonateList = new List<pCoords>();
        public int top = -1, left = -1, pWidth = 0, pHeight = 0;

        #endregion

        #region Constructor

        public tPieceCoordonates(int col, int row, int width, int height)
        {
            this.pWidth = width; this.pHeight = height;
            this.left = col; this.top = row;
            this.set(col, row);
        }

        #endregion

        #region Public Methods

        public void set(int col, int row)
        {
            coordonateList.Add(new pCoords(col, row));
        }

        public void resetCoordonates()
        {
            coordonateList.Clear();
        }

        public void update(Keys direction, TetrisControls tBoard)
        {
            List<pCoords> newCoordList = new List<pCoords>();
            int deltaY = (this.top + this.pHeight), deltaX = (this.left + this.pWidth);
            int newTop = this.top, newLeft = this.left;
            int newPHeight = this.pHeight, newPWidth = this.pWidth;

            switch (direction)
            {
                case Keys.Down:
                    foreach (pCoords pCoord in coordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new pCoords(newCol, ++newRow));
                    }
                    ++deltaY; newTop++;
                    break;
                case Keys.Left:
                    foreach (pCoords pCoord in coordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new pCoords(--newCol, newRow));
                    }
                    deltaX = left; --deltaX; newLeft--;
                    break;
                case Keys.Right:
                    foreach (pCoords pCoord in coordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new pCoords(++newCol, newRow));
                    }
                    ++deltaX; newLeft++;
                    break;
                case Keys.Space:
                    newCoordList = new List<pCoords>(this.createTempRotatedPieceCoordonates(tBoard.piece.tColor));
                    newPHeight = this.pWidth; newPWidth = this.pHeight;
                    deltaY = (this.top + this.pWidth); deltaX = (this.left + this.pHeight);
                    break;
                default:
                    newCoordList = new List<pCoords>(this.coordonateList);
                    break;
            }

            if (!tPiece.willCollide(newCoordList, deltaX, deltaY, tBoard, true))
            {
                this.coordonateList = new List<pCoords>(newCoordList);
                this.top = newTop; this.left = newLeft;
                this.pHeight = newPHeight; this.pWidth = newPWidth;
            }
            else if (direction == Keys.Down)
            {
                tBoard.piece = null;
            }
        }

        #endregion

        #region Private Methods

        private List<pCoords> createTempRotatedPieceCoordonates(int color)
        {
            List<pCoords> tempCoordList = new List<pCoords>();
            int[,] tempMatrix = new int[pWidth, pHeight];

            foreach (pCoords pCoord in this.coordonateList)
            {
                tempMatrix[(pCoord.X - left), (pCoord.Y - top)] = color;
            }

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    int newCol = (tempMatrix.GetLength(1) - j - 1) + left;
                    int newRow = i + top;
                    if (tempMatrix[i, j] > 0)
                    {
                        tempCoordList.Add(new pCoords(newCol, newRow));
                    }
                }
            }
            return tempCoordList;
        }

        #endregion
    }

}
