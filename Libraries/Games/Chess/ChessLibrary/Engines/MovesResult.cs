namespace ChessLibrary.Engines;

public class MovesResult
{
    public bool Successful {get; set;} = true;
    public String ErrorMessage {get; set;} = "";
    public String Fen {get; set;} = "";
    public List<UciInfo> Moves {get; set;} = new();
}
