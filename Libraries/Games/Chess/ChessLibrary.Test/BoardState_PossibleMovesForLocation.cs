using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation
{

    [Theory]
    [InlineData(5,4)]
    [InlineData(4,0)]
    [InlineData(3,3)]
    public static void NoMovesforEmptySquare_WhiteTurn(int row, int col)
    {
        BoardState state = new();
        Location loc = new(row,col);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(5,4)]
    [InlineData(4,0)]
    [InlineData(3,3)]
    public static void NoMovesforEmptySquare_BlackTurn(int row, int col)
    {
        BoardState state = new(){ CurrentTurn = PLAYER.BLACK};
        Location loc = new(row,col);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }


    [Theory]
    [InlineData(-1, 1)]
    [InlineData(8, 1)]
    [InlineData(3, -1)]
    [InlineData(5, 8)]
    [InlineData(9, 9)]
    [InlineData(-1,-1)]
    public static void EmptyListReturnedForInvalidLocations(int row, int col)
    {
        BoardState state = new(){ CurrentTurn = PLAYER.BLACK};
        Location loc = new(row,col);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);

    }

}
