namespace XadrezConsole.board
{
    abstract internal class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moveCount { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.moveCount = 0;
        }

        public void IncrementMoveCount()
        {
            moveCount++;
        }

        public void DecrementMoveCount()
        {
            moveCount--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] mat = PossibleMoves();
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoves()[pos.line, pos.column];
        }

        public abstract bool[,] PossibleMoves();

    }
}
