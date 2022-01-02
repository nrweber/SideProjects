namespace ChessLibrary.Engines;

public class MovesResult
{
    public bool Successful {get; set;} = true;
    public String ErrorMessage {get; set;} = "";
    public String Fen {get; set;} = "";
    public IEnumerable<UciInfo> Moves {get; set;} = new UciInfo[0];
}
