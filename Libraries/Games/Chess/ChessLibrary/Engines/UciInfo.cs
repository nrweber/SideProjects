namespace ChessLibrary.Engines;

public record struct UciInfo
{
    public int? Depth { get; set; }
    public int? CPScore { get; set; }
    public int? MateScore { get; set; }
    public int? MultiPV { get; set; }
    public Move Move  { get; set; } = new Move();
}
