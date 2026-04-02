using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color) { }

    public override string ToString()
    {
        return "N";
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
        
        pos.SetValues(position.line - 1, position.column - 2);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line - 2, position.column - 1);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line - 2, position.column + 1);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
       
        pos.SetValues(position.line - 1, position.column + 2);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line + 1, position.column + 2);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line + 1, position.column - 2);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line + 2, position.column - 1);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        
        pos.SetValues(position.line + 2, position.column + 1);
        if (board.ValidPosition(pos) && CanMove(pos))
        {
            mat[pos.line, pos.column] = true;
        }
        return mat;
    }
}
}
