using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "R";
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
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.line--;
            }
            //down
            pos.SetValues(position.line + 1, position.column);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.line++;
            }
            //right
            pos.SetValues(position.line, position.column + 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.column++;
            }
            //left
            pos.SetValues(position.line, position.column - 1);
            while (board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.Piece(pos) != null && board.Piece(pos).color != color)
                {
                    break;
                }
                pos.column--;
            }
            return mat;
        }
    }
}