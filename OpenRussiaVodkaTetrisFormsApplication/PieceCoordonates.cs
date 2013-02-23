using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ORVT
{
    class PieceCoordonates
    {
        #region Properties

        public List<PieceCoordonatesStructure> CoordonateList = new List<PieceCoordonatesStructure>();
        public int Top = -1, Left = -1, PieceWidth = 0, PieceHeight = 0;

        #endregion

        #region Constructor

        public PieceCoordonates(int col, int row, int width, int height)
        {
            this.PieceWidth = width; 
            this.PieceHeight = height;
            this.Left = col; 
            this.Top = row;
            this.Set(col, row);
        }

        #endregion

        #region Public Methods

        public void Set(int col, int row)
        {
            CoordonateList.Add(new PieceCoordonatesStructure(col, row));
        }

        public void ResetCoordonates()
        {
            CoordonateList.Clear();
        }

        public void Update(Keys direction, TetrisControls tBoard)
        {
            List<PieceCoordonatesStructure> newCoordList = new List<PieceCoordonatesStructure>();
            int deltaY = (this.Top + this.PieceHeight);
            int deltaX = (this.Left + this.PieceWidth);
            int newTop = this.Top;
            int newLeft = this.Left;
            int newPHeight = this.PieceHeight;
            int newPWidth = this.PieceWidth;

            switch (direction)
            {
                case Keys.Down:
                    foreach (PieceCoordonatesStructure pCoord in CoordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new PieceCoordonatesStructure(newCol, ++newRow));
                    }
                    ++deltaY; newTop++;
                    break;
                case Keys.Left:
                    foreach (PieceCoordonatesStructure pCoord in CoordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new PieceCoordonatesStructure(--newCol, newRow));
                    }
                    deltaX = Left; --deltaX; newLeft--;
                    break;
                case Keys.Right:
                    foreach (PieceCoordonatesStructure pCoord in CoordonateList)
                    {
                        int newCol = pCoord.X, newRow = pCoord.Y;
                        newCoordList.Add(new PieceCoordonatesStructure(++newCol, newRow));
                    }
                    ++deltaX; newLeft++;
                    break;
                case Keys.Space:
                    newCoordList = new List<PieceCoordonatesStructure>(this.createTempRotatedPieceCoordonates(tBoard.Piece.Color));
                    newPHeight = this.PieceWidth; 
                    newPWidth = this.PieceHeight;
                    deltaY = (this.Top + this.PieceWidth); 
                    deltaX = (this.Left + this.PieceHeight);
                    break;
                default:
                    newCoordList = new List<PieceCoordonatesStructure>(this.CoordonateList);
                    break;
            }

            if (!Piece.WillCollide(newCoordList, deltaX, deltaY, tBoard, true))
            {
                this.CoordonateList = new List<PieceCoordonatesStructure>(newCoordList);
                this.Top = newTop; 
                this.Left = newLeft;
                this.PieceHeight = newPHeight; 
                this.PieceWidth = newPWidth;
            }
            else if (direction == Keys.Down)
            {
                tBoard.Piece = null;
            }
        }

        #endregion

        #region Private Methods

        private List<PieceCoordonatesStructure> createTempRotatedPieceCoordonates(int color)
        {
            List<PieceCoordonatesStructure> tempCoordList = new List<PieceCoordonatesStructure>();
            int[,] tempMatrix = new int[PieceWidth, PieceHeight];

            foreach (PieceCoordonatesStructure pCoord in this.CoordonateList)
            {
                tempMatrix[(pCoord.X - Left), (pCoord.Y - Top)] = color;
            }

            for (int i = 0; i < tempMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < tempMatrix.GetLength(1); j++)
                {
                    int newCol = (tempMatrix.GetLength(1) - j - 1) + Left;
                    int newRow = i + Top;
                    if (tempMatrix[i, j] > 0)
                    {
                        tempCoordList.Add(new PieceCoordonatesStructure(newCol, newRow));
                    }
                }
            }
            return tempCoordList;
        }

        #endregion
    }

}
