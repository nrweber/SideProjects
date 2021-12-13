using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_King
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
    public static void CenterOfBoard_4_3_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_5_0_NoBlockers(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[5,0] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Location loc = new(5,0);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,0)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,1))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_4_7_NoBlockers_bonds_checking(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,7] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Location loc = new(4,7);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,7)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,7))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_7_2_NoBlockers_bonds_checking(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[7,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Location loc = new(7,2);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(7,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_0_3_NoBlockers_bonds_checking(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[0,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Location loc = new(0,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(1,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,4))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void AllBlockedBySameColorPieces_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void AllLocationsHavePieceOfOtherColor_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[5,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SomeBlockedBySameColorPieces_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3))))
                );
    }
}
