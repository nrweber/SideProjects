using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_Queen
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
    public static void NoBlockers_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = state.PossibleMovesForLocation(loc);

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
                item => Assert.True(item.Equals(new Move(loc, new Location(4,7)))),

                // Up and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                // Up and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,6)))),

                // Down and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,7)))),

                // Down and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void OtherColorBlockers_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;

        Board[BoardArrayLocation(6,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(1,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(4,0)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(4,5)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(7,0)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(6,5)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,5)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(1,0)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: player);


        Location loc = new(4,3);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,5)))),

                // Up and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,0)))),

                // Up and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),

                // Down and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,5)))),

                // Down and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SameColorBlockers_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;

        Board[BoardArrayLocation(6,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(1,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(7,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(6,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(1,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);


        Location loc = new(4,3);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //up
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),

                //down
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),

                //left
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1)))),

                //right
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),

                // Up and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),

                // Up and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),

                // Down and Right
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4)))),

                // Down and Left
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,1))))
                );
    }
}
