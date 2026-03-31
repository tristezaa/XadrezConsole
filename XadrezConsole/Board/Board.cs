namespace XadrezConsole.board
{
    internal class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }

        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public bool PieceExists(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }
        public void PlacePiece(Piece p, Position pos)
        { if(PieceExists(pos))
            {
                throw new BoardException("A piece already exists in that position.");
            }
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if(Piece(pos) == null)
            {
                return null;
            }
            Piece aux = Piece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool ValidPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
        }
}
