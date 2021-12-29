using Microsoft.AspNetCore.Mvc;
using ChessLibrary.Engines;

namespace StockfishAPI.Controllers;


[ApiController]
[Route("/api/stockfish")]
public class StockfishController : ControllerBase
{
    private readonly ILogger<StockfishController> _logger;

    public StockfishController(ILogger<StockfishController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public MovesResult Get(MoveRequest request)
    {
        //TODO: Turn fen into state to check if Fen is valid

        var moves = Stockfish.GetBestMoves(request.Fen, 12, 10);
        Console.WriteLine($"here - {moves.Length}");
        return new MovesResult()
        {
            Fen = request.Fen,
            Moves = moves
        };
    }
}



public class MoveRequest
{
    public String Fen {get; set;} = "";
}

public class MovesResult
{
    public bool Successful {get; set;} = true;
    public String ErrorMessage {get; set;} = "";
    public String Fen {get; set;} = "";
    public IEnumerable<UciInfo> Moves {get; set;} = new UciInfo[0];
}
