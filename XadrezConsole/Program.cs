using XadrezConsole.board;
using XadrezConsole;
using XadrezConsole.chess;

Board board = new Board(8, 8);

board.placePiece(new Rook(board, Color.Black), new Position(0, 0));
board.placePiece(new Rook(board, Color.Black), new Position(1, 3));
board.placePiece(new King(board, Color.Black), new Position(2, 4));

Screen.PrintBoard(board);
