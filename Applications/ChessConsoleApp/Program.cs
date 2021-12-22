var state = new BoardState();

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








