namespace ChessLibrary;

public record struct Move(Location Start, Location End, PROMOTION_PIECE promotion = PROMOTION_PIECE.NONE)
{

    public String Algebraic
    {
        get
        {
            return $"{Start.Algebraic}{End.Algebraic}";
        }
    }
}
