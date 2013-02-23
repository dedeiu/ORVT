using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORVT
{
    class PieceFactory
    {
        #region Properties

        private List<Piece> possiblePieces = new List<Piece>();
        private Random randomNumber = new Random();
        private TetrisControls board = null;
        private int colPosition = Convert.ToInt32(TetrisControls.SURFACE_COLS / 2);
        private int rowPosition = 0;
        private enum TetrisPieceEnum { L, Line, Cube, S }
        #endregion

        #region Public methods

        public PieceFactory(TetrisControls board)
        {
            this.board = board;
        }

        public Piece GetRandomPiece()
        {
            Array values = Enum.GetValues(typeof(TetrisPieceEnum));
            TetrisPieceEnum randomShape = (TetrisPieceEnum)values.GetValue(randomNumber.Next(values.Length));
            Piece randomPiece = (Piece) this.generatePiece(randomShape, this.colPosition, this.rowPosition, this.board);
            return randomPiece;
        }

        private Piece generatePiece(TetrisPieceEnum shape, int colPosition, int rowPosition, TetrisControls board)
        {
            Piece piece = null;
            switch (shape)
            {
                case TetrisPieceEnum.L:
                    PieceCoordonates pShapeCoordonates = new PieceCoordonates(colPosition, rowPosition, 2, 3);
                    pShapeCoordonates.Set(colPosition, ++rowPosition);
                    pShapeCoordonates.Set(colPosition, ++rowPosition);
                    pShapeCoordonates.Set(++colPosition, rowPosition);
                    piece = new Piece(board, pShapeCoordonates, TetrisControls.BLUE);
                    break;
                case TetrisPieceEnum.Line:
                    PieceCoordonates pLineCoordonates = new PieceCoordonates(colPosition, rowPosition, 4, 1);
                    pLineCoordonates.Set(++colPosition, rowPosition);
                    pLineCoordonates.Set(++colPosition, rowPosition);
                    pLineCoordonates.Set(++colPosition, rowPosition);
                    piece = new Piece(board, pLineCoordonates, TetrisControls.RED);
                    break;
                case TetrisPieceEnum.Cube:
                    PieceCoordonates pCubeCoordonates = new PieceCoordonates(colPosition, rowPosition, 2, 2);
                    pCubeCoordonates.Set(++colPosition, rowPosition);
                    pCubeCoordonates.Set(--colPosition, ++rowPosition);
                    pCubeCoordonates.Set(++colPosition, rowPosition);
                    piece = new Piece(board, pCubeCoordonates, TetrisControls.YELLOW);
                    break;
                case TetrisPieceEnum.S:
                    PieceCoordonates pSCoordonates = new PieceCoordonates(colPosition, rowPosition, 3, 2);
                    pSCoordonates.Set(++colPosition, rowPosition);
                    pSCoordonates.Set(colPosition, ++rowPosition);
                    pSCoordonates.Set(++colPosition, rowPosition);
                    piece = new Piece(board, pSCoordonates, TetrisControls.ORANGE);
                    break;

                //TODO: add all pieces
            }
            return piece;
        }

        #endregion
    }
}
