using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished { get; set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            PlacePieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece p = board.RemovePiece(origin);
            p.IncrementMoveCount();
            Piece capturedPiece = board.RemovePiece(destination);
            board.PlacePiece(p, destination);
        }

        private void PlacePieces()
        {
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('c', 1).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('c', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('d', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('e', 2).ToPosition());
            board.PlacePiece(new King(board, Color.White), new PositionChess('d', 1).ToPosition());

            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('c', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('c', 8).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('d', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('e', 7).ToPosition());
            board.PlacePiece(new King(board, Color.Black), new PositionChess('d', 8).ToPosition());

        }
    }
}
