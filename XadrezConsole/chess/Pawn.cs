using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "P";
        }

        public bool HasEnemy(Position pos)
        {
            Piece p = board.Piece(pos);
            return p != null && p.color != color;
        }

        private bool Free(Position pos)
        {
            return board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.SetValues(position.line - 1, position.column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 2, position.column);
                if (board.ValidPosition(pos) && Free(pos) && moveCount == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column - 1);
                if (board.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column + 1);
                if (board.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
            }
            else
            {
                pos.SetValues(position.line + 1, position.column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 2, position.column);
                if (board.ValidPosition(pos) && Free(pos) && moveCount == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column - 1);
                if (board.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column + 1);
                if (board.ValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
            }
            return mat;
        }


    }
}
