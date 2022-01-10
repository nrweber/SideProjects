using ChessLibrary.Engines;

namespace ChessConsoleApp;

public static class ChessConsoleHelper
{
    public static Dictionary<PIECE, (char letter, ConsoleColor color)> PieceDisplayInfo = new(){
        {PIECE.WHITE_PAWN,   ('P', ConsoleColor.Yellow)},
        {PIECE.WHITE_ROOK,   ('R', ConsoleColor.Yellow)},
        {PIECE.WHITE_KNIGHT, ('N', ConsoleColor.Yellow)},
        {PIECE.WHITE_BISHOP, ('B', ConsoleColor.Yellow)},
        {PIECE.WHITE_QUEEN,  ('Q', ConsoleColor.Yellow)},
        {PIECE.WHITE_KING,   ('K', ConsoleColor.Yellow)},
        {PIECE.BLACK_PAWN,   ('p', ConsoleColor.Blue)},
        {PIECE.BLACK_ROOK,   ('r', ConsoleColor.Blue)},
        {PIECE.BLACK_KNIGHT, ('n', ConsoleColor.Blue)},
        {PIECE.BLACK_BISHOP, ('b', ConsoleColor.Blue)},
        {PIECE.BLACK_QUEEN,  ('q', ConsoleColor.Blue)},
        {PIECE.BLACK_KING,   ('k', ConsoleColor.Blue)},
        {PIECE.NONE,         (' ', ConsoleColor.White)}
    };

    public static void PrintBoard(BoardState state)
    {
        if(state.CurrentTurn == PLAYER.WHITE)
        {
            for(int r = 7; r >= 0; r--)
            {
                Console.WriteLine("   +---+---+---+---+---+---+---+---+");
                Console.Write($"{r+1}  ");
                for(int c = 0; c <= 7; c++)
                {
                    var displayData = PieceDisplayInfo[state.Board[r,c]];
                    Console.Write("| ");
                    Console.ForegroundColor = displayData.color;
                    Console.Write($"{displayData.letter} ");
                    Console.ResetColor();
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   +---+---+---+---+---+---+---+---+");
            Console.WriteLine("     a   b   c   d   e   f   g   h  ");
        }
        else
        {
            for(int r = 0; r <= 7; r++)
            {
                Console.WriteLine("   +---+---+---+---+---+---+---+---+");
                Console.Write($"{r+1}  ");
                for(int c = 7; c >= 0; c--)
                {
                    var displayData = PieceDisplayInfo[state.Board[r,c]];
                    Console.Write("| ");
                    Console.ForegroundColor = displayData.color;
                    Console.Write($"{displayData.letter} ");
                    Console.ResetColor();
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   +---+---+---+---+---+---+---+---+");
            Console.WriteLine("     h   g   f   e   d   c   b   a  ");
        }
    }

    public static string GenerateMoves(BoardState state)
    {
        var moves = ChessHelper.PossibleMoves(state);

        StringBuilder output = new();
        foreach(var m in moves)
        {
            output.Append($"{m.Algebraic}\n");
        }
        return output.ToString();
    }

    public static (BoardState newState, string outputText) MakeMove(BoardState state, string moveStr)
    {
        List<Move> moves = ChessHelper.PossibleMoves(state).Where(x => x.Algebraic == moveStr).ToList();

        if(moves.Count > 0)
        {
            var move = moves[0];
            var (_, newState) =  ChessHelper.MakeMove(state, move);
            return (newState, $"Made Move {move.Algebraic}");
        }
        else
        {
            return (state, "Invalid move");
        }
    }


    public static BoardState GetComputerMoves(StockfishAPIClient client, BoardState state)
    {
        var result = client.GetComputerMoves(ChessHelper.ToFENNotation(state)).Result;
        if(result.Moves.Count != 0)
        {
            var move = result.Moves[0].Move;
            (_, var newState) = ChessHelper.MakeMove(state, move);
            return newState;
        }
        else
        {
            return state;
        }
    }
}
