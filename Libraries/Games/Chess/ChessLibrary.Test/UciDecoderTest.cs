using Xunit;

namespace ChessLibrary.Test;

public class UciDecoderTest
{
    [Fact]
    public void DepthNullIfNoDepthKeyword()
    {
        string input = $"info";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Null(info.Depth);
    }


    [Theory]
    [InlineData(4)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(628)]
    public void InfoDepthKeyword(int number)
    {
        string input = $"info depth {number}";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(number, info.Depth);
    }



    [Theory]
    [InlineData(40)]
    [InlineData(126)]
    [InlineData(1538)]
    [InlineData(628)]
    public void CPScore(int score)
    {
        string input = $"info score cp {score}";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(score, info.CPScore);
    }



    [Theory]
    [InlineData(4)]
    [InlineData(1)]
    [InlineData(5)]
    public void MateScore(int mate)
    {
        string input = $"info score mate {mate}";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(mate, info.MateScore);
    }





    [Fact]
    public void InfoComplexText()
    {
        string input = $"info score cp 45 depth 4";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(4, info.Depth);
    }
}
