using XadrezConsole.board;
using XadrezConsole;
using XadrezConsole.chess;
try
{
    ChessMatch chessMatch = new ChessMatch();

    while (!chessMatch.finished)
    {
        Console.Clear();
        Screen.PrintBoard(chessMatch.board);

        Console.Write("Enter the origin position: ");
        Position origin = Screen.ReadChessPosition().ToPosition();
        Console.Write("Enter the destination position: ");
        Position destination = Screen.ReadChessPosition().ToPosition();

        chessMatch.ExecuteMove(origin, destination);
    }

}
catch (BoardException e)
{
    Console.WriteLine(e.Message);
}