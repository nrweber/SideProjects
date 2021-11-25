using Xunit;

namespace ChessLibrary.Test;

public class LocationTest
{
    [Theory]
    [InlineData(0,0, "a1")]
    [InlineData(0,7, "h1")]
    [InlineData(1,6, "g2")]
    [InlineData(2,5, "f3")]
    [InlineData(3,4, "e4")]
    [InlineData(4,3, "d5")]
    [InlineData(5,2, "c6")]
    [InlineData(6,1, "b7")]
    [InlineData(7,0, "a8")]
    public void RandomAlgebraicTests(int row, int column, string expectedOutput)
    {
        Location l = new()
        {
            Row = row,
            Column = column
        };


        Assert.Equal(expectedOutput, l.Algebraic);
    }
}
