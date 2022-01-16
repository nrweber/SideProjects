using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_King
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
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(5,0)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,7)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Board[BoardArrayLocation(5,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

        Location loc = new(4,3);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(PLAYER.WHITE)]
    [InlineData(PLAYER.BLACK)]
    public static void PieceOfOtherColorOnDiagonals_4_3(PLAYER player)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Board[BoardArrayLocation(5,2)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,4)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,2)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,4)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Board[BoardArrayLocation(4,2)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(4,4)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,3)] = (player == PLAYER.WHITE) ? PIECE.BLACK_PAWN : PIECE.WHITE_PAWN;

        Board[BoardArrayLocation(5,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_KING : PIECE.BLACK_KING;

        Board[BoardArrayLocation(5,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,3)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,2)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(3,4)] = (player == PLAYER.WHITE) ? PIECE.WHITE_PAWN : PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: player);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(0,5)] = PIECE.WHITE_BISHOP;
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(0,6)] = PIECE.WHITE_KNIGHT;
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanKingCastle: false);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,3)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(1,7)] = PIECE.WHITE_ROOK; // Can't do 0,6 or 0,5 because those are seen as blockers
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
    public static void WhiteCastelKingSide_CantCastelInCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(1,3)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, WhiteCanKingCastle: true);

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
    public static void WhiteCastelKingSide_CantCastelIfMoveThroughCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(4,5)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, WhiteCanKingCastle: true);

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //normal without column 5 and king side castel
                item => Assert.True(item.Equals(new Move(loc, new Location(1,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,3))))
                );
    }

    [Fact]
    public static void WhiteCastelQueenSide_NoBlocks()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(0,3)] = PIECE.WHITE_QUEEN;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(0,2)] = PIECE.WHITE_BISHOP;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(0,1)] = PIECE.WHITE_KNIGHT;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanQueenCastle: false);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,5)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(1,0)] = PIECE.WHITE_ROOK; // Can't do 0,1 0,2 or 0,3 because those are seen as blockers
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
    public static void WhiteCastelQueenSide_CantCastleIfInCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(1,5)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

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
    public static void WhiteCastelQueenSide_CantCastleIfWouldMoveThroughCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;

        Board[BoardArrayLocation(3,3)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, WhiteCanQueenCastle: true);

        Location loc = new(0,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves without column 3
                item => Assert.True(item.Equals(new Move(loc, new Location(1,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(1,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,5))))
                );
    }


    [Fact]
    public static void BlackCastelKingSide_NoBlocks()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(7,5)] = PIECE.BLACK_BISHOP;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(7,6)] = PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: false);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,3)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(6,7)] = PIECE.BLACK_ROOK; // Can't do 7,6 or 7,5 because those are seen as blockers
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
    public static void BlackCastelKingSide_CantCastelIfInCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(6,3)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

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
    public static void BlackCastelKingSide_CantCastelIfWouldMoveThroughCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(4,5)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanKingCastle: true);

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves with column 5
                item => Assert.True(item.Equals(new Move(loc, new Location(7,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,3)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4))))
                );
    }

    [Fact]
    public static void BlackCastelQueenSide_NoBlocks()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(7,3)] = PIECE.BLACK_QUEEN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(7,2)] = PIECE.BLACK_BISHOP;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(7,1)] = PIECE.BLACK_KNIGHT;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: false);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,5)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(6,0)] = PIECE.BLACK_ROOK; // Can't do 0,1 0,2 or 0,3 because those are seen as blockers
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
    public static void BlackCastelQueenSide_CantCastelIfInCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(6,5)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

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
    public static void BlackCastelQueenSide_CantCastelIfWouldMoveThroughCheck()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;

        Board[BoardArrayLocation(4,3)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, BlackCanQueenCastle: true);

        Location loc = new(7,4);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //regular moves
                item => Assert.True(item.Equals(new Move(loc, new Location(7,5)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,4)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(6,5))))
                );
    }
}
