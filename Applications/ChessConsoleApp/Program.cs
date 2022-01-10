using ChessLibrary.Engines;

var state = new BoardState();
HttpClient client = new HttpClient();


StockfishAPIClient apiClient = new(client);

Console.Clear();
ChessConsoleHelper.PrintBoard(state);
Console.WriteLine(ChessHelper.ToFENNotation(state));

bool run = true;
while(run)
{

    Console.Write("command > ");
    var input = Console.ReadLine();

    if(input == null)
        continue;


    String extraOutputText = "";
    switch(input)
    {
        case "q":
            run = false;
            break;
        case "m":
            extraOutputText = ChessConsoleHelper.GenerateMoves(state);
            break;
        case "c":
            state = ChessConsoleHelper.GetComputerMoves(apiClient, state);
            break;
        case "":
            break;
        default:
            (state, extraOutputText) = ChessConsoleHelper.MakeMove(state, input);
            break;

    }

    Console.Clear();
    ChessConsoleHelper.PrintBoard(state);
    if(extraOutputText != "")
        Console.WriteLine(extraOutputText);

    Console.WriteLine(ChessHelper.ToFENNotation(state));
}

Console.Clear();
Console.WriteLine("Thank you for playing!");



client.Dispose();


