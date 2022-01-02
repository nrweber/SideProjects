using Microsoft.AspNetCore.Mvc;
using ChessLibrary;
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
    public MovesResult Get(String Fen)
    {
        (var validState, _) = ChessHelper.ToBoardState(Fen);

        if(validState == false)
        {
            return new MovesResult()
            {
                Successful = false,
                ErrorMessage = "FEN is not valid",
                Fen = Fen
            };

        }

        var moves = Stockfish.GetBestMoves(Fen, 12, 10);
        return new MovesResult()
        {
            Fen = Fen,
            Moves = moves
        };
    }
}

public class MovesResult
{
    public bool Successful {get; set;} = true;
    public String ErrorMessage {get; set;} = "";
    public String Fen {get; set;} = "";
    public IEnumerable<UciInfo> Moves {get; set;} = new UciInfo[0];
}
