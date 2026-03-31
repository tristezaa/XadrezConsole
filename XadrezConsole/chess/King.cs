using XadrezConsole.board;


namespace XadrezConsole.chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "K";
        }
         private bool CanMove(Position pos)
        {
            Piece p = board.Piece(pos);
            return p == null || p.color != color;
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
            return mat;
        }
    }
}
