using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_Rooks
{
    private static int BoardArrayLocation(int row, int col)
    {
        return (row*8)+col;
    }

    private static PIECE[] BlankBoard()
    {
        return new PIECE[]
        {
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
        };

    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_4_3__NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_3_5__NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(3,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(3,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,0)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void TopOfBoard_7_3_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(7,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up - none

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(7,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(7,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void LeftOfBoard_5_0_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(5,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(5,0);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(6,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,0)))),

                //left - none

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void RightOfBoard_4_7_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,7)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,7);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,7)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,7)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0))))

                //right - none
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void BottomOfBoard_0_5_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(0,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),

                //down - none

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(0,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,0)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(0,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void BlockersOfOtherColor_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(6,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        Board[BoardArrayLocation(2,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        Board[BoardArrayLocation(4,1)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        Board[BoardArrayLocation(4,6)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void CenterOfBoard_BlockersSameColor(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(6,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        Board[BoardArrayLocation(2,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        Board[BoardArrayLocation(4,1)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        Board[BoardArrayLocation(4,6)] = (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5))))
                );
    }
}
