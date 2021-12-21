using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_Queen
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
    public static void NoBlockers_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;

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
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;

        state.Board[6,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[1,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[4,0] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[4,5] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[7,0] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[6,5] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[2,5] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[1,0] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;


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
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;

        state.Board[6,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[1,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[7,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[6,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[2,5] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[1,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;


        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
