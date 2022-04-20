namespace ChessLibrary.Services.Engines;

public record struct UciInfo(int? Depth, int? CPScore, int? MateScore, int? MultiPV, Move Move)
{
}
