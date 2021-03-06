using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_Bishops
{
    private static BoardState CreateStateWithBlankBoard(PLAYER player)
    {
        BoardState state = new();

        for(int r = 0; r <= 7; r++)
        {
            for(int c = 0; c <= 7; c++)
            {
                state.Board[r,c] = PIECE.NONE;
            }
        }

        state.CurrentTurn = player;

        return state;
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_4_3__NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,6)))),

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,7)))),

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_3_5__NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[3,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(3,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,1)))),

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,7)))),

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(2,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,7)))),

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,2))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void TopOfBoard_7_3_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[7,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(7,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left - none

                //up and right - none

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,7)))),

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void LeftOfBoard_5_0_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[5,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(5,0);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left - none

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,2)))),

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))

                //down and left - none
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void RightOfBoard_4_7_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,7] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(4,7);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,4)))),

                //up and right - none

                //down and right - none

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void BottomOfBoard_0_5_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[0,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Location loc = new(0,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,0)))),

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(1,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,7))))

                //down and right - none

                //down and left - none
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void BlockersOfOtherColor_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        state.Board[7,0] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        state.Board[6,5] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        state.Board[1,6] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        state.Board[1,0] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,6)))),

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_BlockersSameColor(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        state.Board[7,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        state.Board[6,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        state.Board[1,6] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        state.Board[1,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up and left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),

                //up and right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),

                //down and right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),

                //down and left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1))))
                );
    }
}
