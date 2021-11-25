namespace ChessLibrary;

public record struct Location
{
    public int Row = 0;
    public int Column = 0;


    private static readonly char[] columns = new[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};
    public String Algebraic
    {
        get
        {
            return $"{columns[Column]}{Row+1}";
        }
    }
}
