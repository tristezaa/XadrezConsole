using XadrezConsole.board;

namespace XadrezConsole.chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            vulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece p = board.RemovePiece(origin);
            p.IncrementMoveCount();
            Piece capturedPiece = board.RemovePiece(destination);
            board.PlacePiece(p, destination);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            //Castling kingside
            if (p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinationR = new Position(origin.line, origin.column + 1);
                Piece R = board.RemovePiece(originR);
                R.IncrementMoveCount();
                board.PlacePiece(R, destinationR);
            }
            //Castling queenside
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinationR = new Position(origin.line, origin.column - 1);
                Piece R = board.RemovePiece(originR);
                R.IncrementMoveCount();
                board.PlacePiece(R, destinationR);
            }
            // En passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(destination.line + 1, destination.column);
                    }
                    else
                    {
                        posP = new Position(destination.line - 1, destination.column);
                    }
                    capturedPiece = board.RemovePiece(posP);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = board.RemovePiece(destination);
            p.DecrementMoveCount();
            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            board.PlacePiece(p, origin);

            //Castling kingside
            if (p is King && destination.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinationR = new Position(origin.line, origin.column + 1);
                Piece R = board.RemovePiece(destinationR);
                R.DecrementMoveCount();
                board.PlacePiece(R, originR);
            }
            //Castling queenside
            if (p is King && destination.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinationR = new Position(origin.line, origin.column - 1);
                Piece R = board.RemovePiece(destinationR);
                R.DecrementMoveCount();
                board.PlacePiece(R, originR);
            }
            // En passant
            if (p is Pawn)
            {
                if (origin.column != destination.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawn = board.RemovePiece(destination);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, destination.column);
                    }
                    else
                    {
                        posP = new Position(4, destination.column);
                    }
                    board.PlacePiece(pawn, posP);
                }
            }
        }

        public void MakeMove(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMove(origin, destination);
            if (IsInCheck(currentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in check.");
            }
            if (IsInCheck(Opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if (IsCheckmate(Opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                ChangePlayer();
            }
            
            Piece p = board.Piece(destination);

            // En passant
            if (p is Pawn && (destination.line == origin.line - 2 || destination.line == origin.line + 2))
            {
                vulnerableEnPassant = p;
            }
            else
            {
                vulnerableEnPassant = null;
            }

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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece R = King(color);
            if (R == null)
            {
                throw new BoardException("There is no " + color + " king on the board.");
            }
            foreach (Piece x in PiecesInGame(Opponent(color)))
            {
                bool[,] mat = x.PossibleMoves();
                if (mat[R.position.line, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckmate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMoves();
                for (int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destination);
                            bool checkTest = IsInCheck(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlaceNewPiece(char column, int line, Piece piece)
        {
            board.PlacePiece(piece, new PositionChess(column, line).ToPosition());
            pieces.Add(piece);
        }

        private void PlacePieces()
        {
            // White pieces
            PlaceNewPiece('a', 1, new Rook(board, Color.White));
            PlaceNewPiece('b', 1, new Knight(board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(board, Color.White));
            PlaceNewPiece('d', 1, new Queen(board, Color.White));
            PlaceNewPiece('e', 1, new King(board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(board, Color.White));
            PlaceNewPiece('g', 1, new Knight(board, Color.White));
            PlaceNewPiece('h', 1, new Rook(board, Color.White));

            PlaceNewPiece('a', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(board, Color.White, this));

            // Black pieces
            PlaceNewPiece('a', 8, new Rook(board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(board, Color.Black));
            PlaceNewPiece('e', 8, new King(board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(board, Color.Black));

            PlaceNewPiece('a', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
