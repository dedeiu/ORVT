using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace ORVT
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
            /*tPieceCoordonates pShapeCoordonates = new tPieceCoordonates(colPosition, rowPosition, 2, 3);
            pShapeCoordonates.set(colPosition, ++rowPosition);
            pShapeCoordonates.set(colPosition, ++rowPosition);
            pShapeCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pShapeCoordonates, TetrisControls.BLUE));*/

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
            /*colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            tPieceCoordonates pSCoordonates = new tPieceCoordonates(colPosition, rowPosition, 3, 2);
            pSCoordonates.set(++colPosition, rowPosition);
            pSCoordonates.set(colPosition, ++rowPosition);
            pSCoordonates.set(++colPosition, rowPosition);
            possiblePieces.Add(new tPiece(board, pSCoordonates, TetrisControls.ORANGE));*/
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