using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_Knight
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
    public static void CenterOfBoard_4_3_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_5_0_NoBlockers(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(5,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(5,0);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(7,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_4_7_NoBlockers_bonds_checking(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,7)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,7);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_7_2_NoBlockers_bonds_checking(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(7,2);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,0))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SideOfBoard_0_3_NoBlockers_bonds_checking(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(0,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,1))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void AllBlockedBySameColorPieces_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;

        Board[BoardArrayLocation(6,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(6,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,1)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,1)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void SomeBlockedBySameColorPieces_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;

        Board[BoardArrayLocation(6,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(6,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,5)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1))))
                );
    }

    [Fact]
    public static void WhiteKnight_AllLocationsHavePieceOfOtherColor_4_3()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = PIECE.WHITE_KNIGHT;

        Board[BoardArrayLocation(6,2)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(6,4)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,5)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,5)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,4)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,2)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,1)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,1)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.WHITE);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1))))
                );
    }

    [Fact]
    public static void BlackKnight_AllLocationsHavePieceOfOtherColor_4_3()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = PIECE.BLACK_KNIGHT;

        Board[BoardArrayLocation(6,2)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(6,4)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,5)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,5)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,4)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,2)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,1)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,1)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,1))))
                );
    }
}
