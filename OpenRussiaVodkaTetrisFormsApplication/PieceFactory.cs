using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORVT
{
    class PieceFactory
    {
        #region Properties

        private List<Piece> PossiblePieces = new List<Piece>();
        private Random RandomNumber = new Random();
        private TetrisControls Board = null;

        #endregion

        #region Public methods

        public PieceFactory(TetrisControls board)
        {
            this.Board = board;

            int colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2), rowPosition = 0;

            // L shape
            PieceCoordonates pShapeCoordonates = new PieceCoordonates(colPosition, rowPosition, 2, 3);
            pShapeCoordonates.Set(colPosition, ++rowPosition);
            pShapeCoordonates.Set(colPosition, ++rowPosition);
            pShapeCoordonates.Set(++colPosition, rowPosition);
            PossiblePieces.Add(new Piece(board, pShapeCoordonates, TetrisControls.BLUE));

            // Line shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            PieceCoordonates pLineCoordonates = new PieceCoordonates(colPosition, rowPosition, 4, 1);
            pLineCoordonates.Set(++colPosition, rowPosition);
            pLineCoordonates.Set(++colPosition, rowPosition);
            pLineCoordonates.Set(++colPosition, rowPosition);
            PossiblePieces.Add(new Piece(board, pLineCoordonates, TetrisControls.RED));

            // Cube shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            PieceCoordonates pCubeCoordonates = new PieceCoordonates(colPosition, rowPosition, 2, 2);
            pCubeCoordonates.Set(++colPosition, rowPosition);
            pCubeCoordonates.Set(--colPosition, ++rowPosition);
            pCubeCoordonates.Set(++colPosition, rowPosition);
            PossiblePieces.Add(new Piece(board, pCubeCoordonates, TetrisControls.YELLOW));

            // S shape
            colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
            rowPosition = 0;
            PieceCoordonates pSCoordonates = new PieceCoordonates(colPosition, rowPosition, 3, 2);
            pSCoordonates.Set(++colPosition, rowPosition);
            pSCoordonates.Set(colPosition, ++rowPosition);
            pSCoordonates.Set(++colPosition, rowPosition);
            PossiblePieces.Add(new Piece(board, pSCoordonates, TetrisControls.ORANGE));

            //TODO: add all pieces
        }

        public Piece GetRandomPiece()
        {
            Piece randomPiece = PossiblePieces[RandomNumber.Next(PossiblePieces.Count())];
            int deltaY = (randomPiece.PieceCoordonates.Top + randomPiece.PieceCoordonates.PieceHeight);
            int deltaX = (randomPiece.PieceCoordonates.Left + randomPiece.PieceCoordonates.PieceWidth);
            if (Piece.WillCollide(randomPiece.PieceCoordonates.CoordonateList, deltaX, deltaY, this.Board, false))
            {
                this.Board.Reset();
                randomPiece.UpdatePieceOnTetrisBoard(0);
                randomPiece = PossiblePieces[RandomNumber.Next(PossiblePieces.Count())];
            }
            randomPiece.UpdatePieceOnTetrisBoard(randomPiece.Color);
            return randomPiece;
        }

        #endregion
    }
}
