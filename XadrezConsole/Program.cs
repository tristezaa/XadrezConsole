using XadrezConsole.board;
using XadrezConsole;
using XadrezConsole.chess;
try
{
    Board board = new Board(8, 8);

    board.placePiece(new Rook(board, Color.Black), new Position(0, 0));
    board.placePiece(new Rook(board, Color.Black), new Position(1, 3));
    board.placePiece(new King(board, Color.White), new Position(1, 1));

    Screen.printBoard(board);
}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}

//PositionChess pos = new PositionChess('c', 7);

//Console.WriteLine(pos);

//Console.WriteLine(pos.toPosition());