using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace ORVT
{
    class Piece
    {
        #region Properties

        public PieceCoordonates PieceCoordonates = null;
        public TetrisControls Board = null;
        public int Color = 1;

        #endregion

        #region Public Methods

        public Piece(TetrisControls board, PieceCoordonates pieceCoordonates, int color)
        {
            PieceCoordonates = pieceCoordonates;
            this.Board = board; 
            this.Color = color;
        }

        public void UpdatePieceOnTetrisBoard(int color)
        {
            foreach (PieceCoordonatesStructure pCoord in this.PieceCoordonates.CoordonateList)
            {
                this.Board.SurfaceMatrix[pCoord.X, pCoord.Y] = color;
            }
        }

        public void ChangePosition(Keys direction)
        {
            this.UpdatePieceOnTetrisBoard(0);
            this.PieceCoordonates.Update(direction, this.Board);
            this.UpdatePieceOnTetrisBoard(this.Color);
        }
        
        public static bool WillCollide(List<PieceCoordonatesStructure> pieceCoordonates, int deltaX, int deltaY, TetrisControls tBoard, bool checkBorders)
        {
            if (checkBorders && (deltaX > TetrisControls.SURFACE_COLS || deltaX < 0 || deltaY > TetrisControls.SURFACE_ROWS))
            {
                return true;
            }

            foreach (PieceCoordonatesStructure pCoord in pieceCoordonates)
            {
                if(tBoard.SurfaceMatrix[pCoord.X, pCoord.Y] > 0) 
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}