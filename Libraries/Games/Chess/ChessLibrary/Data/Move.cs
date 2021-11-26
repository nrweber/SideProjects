namespace ChessLibrary;

public record struct Move(Location Start, Location End)
{

    public String Algebraic
    {
        get
        {
            return $"{Start.Algebraic}{End.Algebraic}";
        }
    }
}
