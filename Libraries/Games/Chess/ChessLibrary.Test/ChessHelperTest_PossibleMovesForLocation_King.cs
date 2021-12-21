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
        state.WhiteCanKingCastle = false;
        state.WhiteCanQueenCastle = false;
        state.BlackCanKingCastle = false;
        state.BlackCanQueenCastle = false;

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
    public static void PieceOfOtherColorOnDiagonals_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[5,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,4))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void PieceOfOtherColorOnSameColumnAttacks_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,4))))
                );
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void PieceOfOtherColorOnSameRowAttacks_4_3(PLAYER player)
    {
        BoardState state = CreateStateWithBlankBoard(player);
        state.Board[4,3] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        state.Board[5,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        state.Board[3,3] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;


        state.Board[5,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[5,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[4,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,2] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        state.Board[3,4] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,3))))
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

    [Fact]
    public static void WhiteCastelKingSide_NoBlocks()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5)))),

                //castling
                item => Assert.True(item.Equals(new Move(loc, new Location(0,6))))
                );
    }

    [Fact]
    public static void WhiteCastelKingSide_BlockNextToKing()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        state.Board[0,5] = PIECE.WHITE_BISHOP;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3))))
                );
    }

    [Fact]
    public static void WhiteCastelKingSide_BlockNextToRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        state.Board[0,6] = PIECE.WHITE_KNIGHT;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelKingSide_NoBlocksButStateSaysItIsNotAllowed()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = false;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelKingSide_NoBlocks_StateSaysAllowedButKingInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.Board[0,3] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        Location loc = new(0,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,4))))
                );
    }

    [Fact]
    public static void WhiteCastelKingSide_NoBlocks_StateSaysAllowedButRookInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[1,7] = PIECE.WHITE_ROOK; // Can't do 0,6 or 0,5 because those are seen as blockers

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_NoBlocks()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5)))),

                //castling
                item => Assert.True(item.Equals(new Move(loc, new Location(0,2))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_BlockNextToKing()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        state.Board[0,3] = PIECE.WHITE_QUEEN;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_BlockInMiddle()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        state.Board[0,2] = PIECE.WHITE_BISHOP;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_BlockNextToRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        state.Board[0,1] = PIECE.WHITE_KNIGHT;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_NoBlocksButStateSaysItIsNotAllowed()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = false;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_NoBlocks_StateSaysAllowedButKingInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,5] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        Location loc = new(0,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,6))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_NoBlocks_StateSaysAllowedButRookInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;
        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[1,0] = PIECE.WHITE_ROOK; // Can't do 0,1 0,2 or 0,3 because those are seen as blockers

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_NoBlocks()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),

                //castling
                item => Assert.True(item.Equals(new Move(loc, new Location(7,6))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_BlockNextToKing()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        state.Board[7,5] = PIECE.BLACK_BISHOP;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_BlockNextToRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        state.Board[7,6] = PIECE.BLACK_KNIGHT;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_NoBlocksButStateSaysItIsNotAllowed()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = false;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_NoBlocks_StateSaysAllowedButKingInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.Board[7,3] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        Location loc = new(7,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,2)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4))))
                );
    }

    [Fact]
    public static void BlackCastelKingSide_NoBlocks_StateSaysAllowedButRookInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[6,7] = PIECE.BLACK_ROOK; // Can't do 7,6 or 7,5 because those are seen as blockers

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_NoBlocks()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),

                //castling
                item => Assert.True(item.Equals(new Move(loc, new Location(7,2))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_BlockNextToKing()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        state.Board[7,3] = PIECE.BLACK_QUEEN;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_BlockInMiddle()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        state.Board[7,2] = PIECE.BLACK_BISHOP;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_BlockNextToRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        state.Board[7,1] = PIECE.BLACK_KNIGHT;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_NoBlocksButStateSaysItIsNotAllowed()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = false;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_NoBlocks_StateSaysAllowedButKingInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,5] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        Location loc = new(7,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,6)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,6))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_NoBlocks_StateSaysAllowedButRookInWrongSpot()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;
        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[6,0] = PIECE.BLACK_ROOK; // Can't do 0,1 0,2 or 0,3 because those are seen as blockers

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }


    // Can't castel if current in check
    // Can't castel if the spot the king moves through is under attack by the other color.


}
