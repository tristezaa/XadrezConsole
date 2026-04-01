using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }

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

        public void MakeMove(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen position.");
            }
            if (currentPlayer != board.Piece(pos).color)
            {
                throw new BoardException("The chosen piece is not yours.");
            }
            if (!board.Piece(pos).HasPossibleMoves())
            {
                throw new BoardException("There are no possible moves for the chosen piece.");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!board.Piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position.");
            }
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        private void PlacePieces()
        {
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('c', 1).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('c', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('d', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('e', 2).ToPosition());
            board.PlacePiece(new Rook(board, Color.White), new PositionChess('e', 1).ToPosition());
            board.PlacePiece(new King(board, Color.White), new PositionChess('d', 1).ToPosition());

            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('c', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('c', 8).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('d', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('e', 7).ToPosition());
            board.PlacePiece(new Rook(board, Color.Black), new PositionChess('e', 8).ToPosition());
            board.PlacePiece(new King(board, Color.Black), new PositionChess('d', 8).ToPosition());

        }
    }
}
