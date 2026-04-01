using XadrezConsole;
using XadrezConsole.board;
using XadrezConsole.chess;
try
{
    ChessMatch chessMatch = new ChessMatch();

    while (!chessMatch.finished)
    {
        try
        {
            Console.Clear();
            Screen.PrintMatch(chessMatch);

            Console.WriteLine();
            Console.Write("Enter the origin position: ");
            Position origin = Screen.ReadChessPosition().ToPosition();
            chessMatch.ValidateOriginPosition(origin);

            bool[,] possibleMoves = chessMatch.board.Piece(origin).PossibleMoves();

            Console.Clear();
            Screen.PrintBoard(chessMatch.board, possibleMoves);

            Console.Write("Enter the destination position: ");
            Position destination = Screen.ReadChessPosition().ToPosition();
            chessMatch.ValidateDestinationPosition(origin, destination);

            chessMatch.MakeMove(origin, destination);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

    }
    Console.Clear();
    Screen.PrintMatch(chessMatch);
}

catch (BoardException e)
{
    Console.WriteLine(e.Message);
}