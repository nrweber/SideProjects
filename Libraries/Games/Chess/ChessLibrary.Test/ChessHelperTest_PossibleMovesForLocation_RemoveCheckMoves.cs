using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_RemoveCheckMoves
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
    public void BishopRookQueenTest(PLAYER playerColor)
    {
        BoardState state = CreateStateWithBlankBoard(playerColor);

        state.Board[0,3] = (playerColor == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        state.Board[1,3] = (playerColor == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        state.Board[5,3] = (playerColor == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;

        Location loc = new(1,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);


    }

    [Theory]
    [InlineData(PLAYER.WHITE, PIECE.WHITE_ROOK)]
    [InlineData(PLAYER.BLACK, PIECE.BLACK_ROOK)]
    [InlineData(PLAYER.WHITE, PIECE.WHITE_QUEEN)]
    [InlineData(PLAYER.BLACK, PIECE.BLACK_QUEEN)]
    public void RookQueenTest(PLAYER playerColor, PIECE movingPiece)
    {
        BoardState state = CreateStateWithBlankBoard(playerColor);

        state.Board[0,3] = (playerColor == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        state.Board[1,3] = movingPiece;

        state.Board[5,3] = (playerColor == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;

        Location loc = new(1,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3))))
                );


    }
}
