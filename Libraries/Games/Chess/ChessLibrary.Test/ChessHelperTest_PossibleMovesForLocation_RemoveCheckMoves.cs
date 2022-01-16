using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_RemoveCheckMoves
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
    public void BishopRookQueenTest(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        Board[BoardArrayLocation(1,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;

        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: player);


        Location loc = new(1,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);


    }

    [Theory]
    [InlineData(PLAYER.WHITE, PIECE.WHITE_ROOK)]
    [InlineData(PLAYER.BLACK, PIECE.BLACK_ROOK)]
    [InlineData(PLAYER.WHITE, PIECE.WHITE_QUEEN)]
    [InlineData(PLAYER.BLACK, PIECE.BLACK_QUEEN)]
    public void RookQueenTest(PLAYER player, PIECE movingPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        Board[BoardArrayLocation(1,3)] = movingPiece;

        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: player);


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
