using XadrezConsole.board;


namespace XadrezConsole.chess
{
    internal class King : Piece
    {
        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) : base(board, color) 
        { 
            this.match = match; 
        }

        public override string ToString()
        {
            return "K";
        }
         private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
        }

        private bool TestRookCastling(Position pos)
        {
            Piece p = board.Piece(pos);
            return p != null && p is Rook && p.color == color && p.moveCount == 0;
        }
        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];
            Position pos = new Position(0, 0);
            // up
            pos.SetValues(position.line - 1, position.column);
            if(board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // ne
            pos.SetValues(position.line - 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // right
            pos.SetValues(position.line, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // se
            pos.SetValues(position.line + 1, position.column + 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // down
            pos.SetValues(position.line + 1, position.column);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // sw
            pos.SetValues(position.line + 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // left
            pos.SetValues(position.line, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // nw
            pos.SetValues(position.line - 1, position.column - 1);
            if (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            //castling
             if (moveCount == 0 && !match.check)
            {
                //castling kingside
                Position posT1 = new Position(position.line, position.column + 3);
                if (TestRookCastling(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (board.Piece(p1) == null && board.Piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                //castling queenside
                Position posT2 = new Position(position.line, position.column - 4);
                if (TestRookCastling(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (board.Piece(p1) == null && board.Piece(p2) == null && board.Piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
