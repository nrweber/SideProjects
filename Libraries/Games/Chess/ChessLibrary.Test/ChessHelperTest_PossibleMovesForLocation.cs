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
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(5,4)]
    [InlineData(4,0)]
    [InlineData(3,3)]
    public static void NoMovesforEmptySquare_BlackTurn(int row, int col)
    {
        BoardState state = new();
        state.CurrentTurn = PLAYER.BLACK;
        Location loc = new(row,col);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }
}
