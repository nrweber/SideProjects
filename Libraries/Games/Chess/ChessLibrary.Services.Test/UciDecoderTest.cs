using Xunit;

using ChessLibrary.Services.Engines;

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


    [Theory]
    [InlineData(4)]
    [InlineData(1)]
    [InlineData(5)]
    public void MultiPv(int multiPv)
    {
        string input = $"info multipv {multiPv}";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(multiPv, info.MultiPV);
    }


    [Theory]
    [InlineData("e2e4 e7e5", 1,4,  3,4)]
    [InlineData("g2g3 e7e5", 1,6,  2,6)]
    public void GetMoveOutOfPV(string moves, int rowFrom, int colFrom, int rowTo, int colTo)
    {
        string input = $"info score cp 45 depth 4 pv {moves}";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(new Move(new Location(rowFrom, colFrom), new Location(rowTo, colTo)), info.Move);
    }

    [Fact]
    public void InfoComplexText()
    {
        string input = $"info score cp 45 multipv 5 depth 4 pv e2e4 e7e5";

        UciInfo info = UciDecoder.ParseUciInfoLine(input);

        Assert.Equal(45, info.CPScore);
        Assert.Equal(5, info.MultiPV);
        Assert.Equal(4, info.Depth);
        Assert.Equal(new Move(new Location(1, 4), new Location(3, 4)), info.Move);
    }




}
