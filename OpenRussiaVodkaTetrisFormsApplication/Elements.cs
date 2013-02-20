using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class tPiece
    {
        public tPieceCoordonates tPieceCoordonates = null;
        public TetrisControls tBoard = null;
        public int tColor = 1;

        public tPiece(TetrisControls board, tPieceCoordonates pieceCoordonates, int color)
        {
            tPieceCoordonates = pieceCoordonates;
            this.tBoard = board; this.tColor = color;
        }

        /* this method updates coordonates on tetris board for current piece */
        public void updatePieceOnTetrisBoard(int color)
        {
            foreach (pCoords pCoord in this.tPieceCoordonates.coordonateList)
            {
                this.tBoard.surfaceMatrix[pCoord.X, pCoord.Y] = color;
            }
        }

        /* this method updates tetris pieces on board */
        public void changePosition(Keys direction)
        {
            this.updatePieceOnTetrisBoard(0);
            this.tPieceCoordonates.update(direction, this.tBoard);
            this.updatePieceOnTetrisBoard(this.tColor);
        }
        
        /* this method checks every collision between tetris pieces and board */
        public static bool willCollide(List<pCoords> pieceCoordonates, int deltaX, int deltaY, TetrisControls tBoard, bool checkBorders)
        {
            /* check borders */
            if (checkBorders && (deltaX > TetrisControls.SURFACE_COLS || deltaX < 0 || deltaY > TetrisControls.SURFACE_ROWS))
            {
                return true;
            }

            /* check existing elements on tetris board */
            foreach (pCoords pCoord in pieceCoordonates)
            {
                if(tBoard.surfaceMatrix[pCoord.X, pCoord.Y] > 0) 
                {
                    return true;
                }
            }
            return false;
        }
    }

    public struct pCoords
    {
        public int X, Y;
        public pCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class tPieceCoordonates
    {
        public List<pCoords> coordonateList = new List<pCoords>();
        public int top = -1, left = -1, pWidth = 0, pHeight = 0;

        public tPieceCoordonates(int col, int row, int width, int height)
        {
            this.pWidth = width; this.pHeight = height;
            this.left = col; this.top = row;
            this.set(col, row);
        }

        /* this method adds coordonates for current piece */
        public void set(int col, int row)
        {
            coordonateList.Add(new pCoords(col, row));
        }

        /* this method resets coordonates for current piece */
        public void resetCoordonates()
        {
            coordonateList.Clear();
        }

        /* this method updates, if necessary, the position of current tetris piece depending on key down eveniment */
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

        /* this method creates coordonates for 90 degree rotation of current piece */
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
    }

    class tPieceFactory
    {
        private List<tPiece> possiblePieces = new List<tPiece>();
        private Random rnd = new Random();
        private TetrisControls tBoard = null;

        public tPieceFactory(TetrisControls board)
        {
            this.tBoard = board;

            int colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2), rowPosition = 0;
            // L shape
            tPieceCoordonates pShapeCoordonates = new tPieceCoordonates(colPosition, rowPosition, 2, 3);
            pShapeCoordonates.set(colPosition, ++rowPosition);
            pShapeCoordonates.set(colPosition, ++rowPosition);
            pShapeCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pShapeCoordonates, TetrisControls.BLUE));

            // Line shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            tPieceCoordonates pLineCoordonates = new tPieceCoordonates(colPosition, rowPosition, 4, 1);
            pLineCoordonates.set(++colPosition, rowPosition);
            pLineCoordonates.set(++colPosition, rowPosition);
            pLineCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pLineCoordonates, TetrisControls.RED));

            // Cube shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            tPieceCoordonates pCubeCoordonates = new tPieceCoordonates(colPosition, rowPosition, 2, 2);
            pCubeCoordonates.set(++colPosition, rowPosition);
            pCubeCoordonates.set(--colPosition, ++rowPosition);
            pCubeCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pCubeCoordonates, TetrisControls.YELLOW));

            // S shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            tPieceCoordonates pSCoordonates = new tPieceCoordonates(colPosition, rowPosition, 3, 2);
            pSCoordonates.set(++colPosition, rowPosition);
            pSCoordonates.set(colPosition, ++rowPosition);
            pSCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pSCoordonates, TetrisControls.ORANGE));
        }

        public tPiece getRandomPiece()
        {
            tPiece randomPiece = possiblePieces[rnd.Next(possiblePieces.Count())];
            int deltaY = (randomPiece.tPieceCoordonates.top + randomPiece.tPieceCoordonates.pHeight), deltaX = (randomPiece.tPieceCoordonates.left + randomPiece.tPieceCoordonates.pWidth);
            if (tPiece.willCollide(randomPiece.tPieceCoordonates.coordonateList, deltaX, deltaY, this.tBoard, false))
            {
                this.tBoard.reset();
                randomPiece.updatePieceOnTetrisBoard(0);
                randomPiece = possiblePieces[rnd.Next(possiblePieces.Count())];
            }
            randomPiece.updatePieceOnTetrisBoard(randomPiece.tColor);
            return randomPiece;
        }
    }
}