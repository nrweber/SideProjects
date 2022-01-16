using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_WhitePawn
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
    [InlineData(0)] // covers that edge doesn't crash looking for attackers
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(7)] // covers that edge doesn't crash looking for attackers
    public static void WhitePawn_Row1_NoAttacks_NoBlocks_OneAndTwoSquareMovesReturned(int column)
    {
        BoardState state = new(); // starting board works for this test
        Location loc = new(1,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,column))))
                );
    }

    [Theory]
    [InlineData(1, PIECE.BLACK_PAWN)]
    [InlineData(3, PIECE.BLACK_PAWN)]
    [InlineData(5, PIECE.BLACK_PAWN)]
    public static void WhitePawn_Row1_NoAttacks_BlockTwoAhead_OneSquareMoveReturnedOnly(int column, PIECE blockingPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,column)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(3,column)] = blockingPiece;
        BoardState state = new(Board);



        Location loc = new(1,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,column))))
                );
    }

    [Theory]
    [InlineData(2, PIECE.BLACK_PAWN)]
    [InlineData(5, PIECE.BLACK_PAWN)]
    [InlineData(7, PIECE.BLACK_PAWN)]
    public static void WhitePawn_Row1_NoAttacks_BlockOneAhead_NoMovesReturned(int column, PIECE blockingPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,column)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,column)] = blockingPiece;
        BoardState state = new(Board);



        Location loc = new(1,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(1,1, 2,0, PIECE.BLACK_PAWN, PIECE.BLACK_QUEEN)]
    [InlineData(1,3, 2,2, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(1,5, 2,4, PIECE.BLACK_PAWN, PIECE.BLACK_ROOK)]
    [InlineData(3,5, 4,4, PIECE.BLACK_PAWN, PIECE.BLACK_KNIGHT)]
    [InlineData(5,5, 6,4, PIECE.BLACK_PAWN, PIECE.BLACK_BISHOP)]
    [InlineData(4,7, 5,6, PIECE.BLACK_PAWN, PIECE.BLACK_ROOK)]
    // Testing that column 0 doesn't crash is covered by WhitePawn_Row1_NoAttacks_NoBlocks_OneAndTwoSquareMovesReturned
    public static void WhitePawn_Row1to5_Col1to7_AttackBlackLeft_BlockOneAhead_OnlyAttackMoveReturned(int startRow, int startColumn, int endRow, int endColumn, PIECE blockingPiece, PIECE attackedPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(endRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(endRow,endColumn)] = attackedPiece;
        BoardState state = new(Board);



        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,endColumn))))
                );
    }


    [Theory]
    [InlineData(1,7, 2,6, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(2,6, 3,5, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(3,5, 4,4, PIECE.BLACK_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(4,4, 5,3, PIECE.BLACK_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(5,3, 6,2, PIECE.BLACK_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(1,2, 2,1, PIECE.BLACK_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(2,1, 3,0, PIECE.BLACK_PAWN, PIECE.WHITE_KING)]
    public static void WhitePawn_Row1to5_Col1to7_WhitePieceInLeftAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(attackedRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(attackedRow,attackedColumn)] = otherPiece;
        BoardState state = new(Board);


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(3,0, 4,1, PIECE.BLACK_PAWN, PIECE.BLACK_QUEEN)]
    [InlineData(1,1, 2,2, PIECE.BLACK_PAWN, PIECE.BLACK_QUEEN)]
    [InlineData(4,3, 5,4, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(2,5, 3,6, PIECE.BLACK_PAWN, PIECE.BLACK_ROOK)]
    [InlineData(2,3, 3,4, PIECE.BLACK_PAWN, PIECE.BLACK_ROOK)]
    [InlineData(1,5, 2,6, PIECE.BLACK_PAWN, PIECE.BLACK_ROOK)]
    // Testing that column 7 doesn't crash is covered by WhitePawn_Row1_NoAttacks_NoBlocks_OneAndTwoSquareMovesReturned
    public static void WhitePawn_Row1to5_Col0to6_AttackBlackRight_BlockOneAhead_AttackMoveReturned(int startRow, int startColumn, int endRow, int endColumn, PIECE blockingPiece, PIECE attackedPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(endRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(endRow,endColumn)] = attackedPiece;
        BoardState state = new(Board);



        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,endColumn))))
                );
    }

    [Theory]
    [InlineData(1,5, 2,6, PIECE.BLACK_PAWN, PIECE.WHITE_KING)]
    [InlineData(2,4, 3,5, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(4,3, 5,4, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(3,2, 4,3, PIECE.BLACK_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(2,1, 3,2, PIECE.BLACK_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(0,5, 1,6, PIECE.BLACK_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(1,6, 2,7, PIECE.BLACK_PAWN, PIECE.WHITE_QUEEN)]
    public static void WhitePawn_Row1to5_Col0to6_WhitePieceInRightAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(attackedRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(attackedColumn,attackedColumn)] = otherPiece;
        BoardState state = new(Board);



        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(1, 0, 2)]
    [InlineData(3, 2, 4)]
    [InlineData(6, 5, 7)]
    public static void WhitePawn_Row1_Col1to6_BothAttacks_NoBlocks_BothMovesAndBothAttacksAreReturned(int column, int attackColumn1, int attackColumn2)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,column)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,attackColumn1)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,attackColumn2)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board);



        Location loc = new(1,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn2))))
                );
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(7, 6)]
    public static void WhitePawn_Row1_Col0and7_OnlyPossibleAttacks_NoBlocks_BothMovesAndTheAttackMoveAreReturned(int column, int attackColumn)
    {
        var Board = BlankBoard();
        BoardState state = new(Board);

        Board[BoardArrayLocation(1,column)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(2,attackColumn)] = PIECE.BLACK_PAWN;


        Location loc = new(1,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(3,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn))))
                );
    }

    [Theory]
    [InlineData(2,0, 3)] // covers that edge doesn't crash looking for attackers
    [InlineData(4,3, 5)]
    [InlineData(5,6, 6)]
    [InlineData(3,7, 4)] // covers that edge doesn't crash looking for attackers
    public static void WhitePawn_Row2to5_NoAttacks_NoBlocks_OneSquareMoveReturned(int startRow, int startColumn, int endRow)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board);


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,startColumn))))
                );
    }

    [Theory]
    [InlineData(2,0, 3)]
    [InlineData(4,3, 5)]
    [InlineData(5,6, 6)]
    [InlineData(3,7, 4)]
    public static void WhitePawn_Row2to5_NoAttacks_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int blockRow)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(blockRow,startColumn)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board);


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(3,1, 4, 4,0, 4,2)]
    [InlineData(2,3, 3, 3,2, 3,4)]
    [InlineData(4,6, 5, 5,5, 5,7)]
    [InlineData(5,2, 6, 6,1, 6,3)]
    public static void WhitePawn_Row2to5_Col1to6_BothAttacks_NoBlocks_ForwardMoveAndBothAttacksAreReturned(int startRow, int startColumn, int endRow, int attackRow1, int attackColumn1, int attackRow2,int attackColumn2)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(attackRow1,attackColumn1)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(attackRow2,attackColumn2)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board);


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,startColumn)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(attackRow1,attackColumn1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(attackRow2,attackColumn2))))
                );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public static void WhitePawn_Row6_Col0to7_NoAttacks_NoBlocks_OneSquareMoveForEachPromotionPieceReturned(int startColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,startColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board);


        Location loc = new(6,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(7,startColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,startColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,startColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,startColumn), PROMOTION_PIECE.ROOK)))
                );
    }

    [Theory]
    [InlineData(1,0)]
    [InlineData(3,2)]
    [InlineData(5,4)]
    [InlineData(7,6)]
    public static void WhitePawn_Row6_Col1to7_AttackLeft_BlockInFront_AttackForEachPromotionTypeReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(7,startColumn)] = PIECE.BLACK_ROOK;
        Board[BoardArrayLocation(7,attackColumn)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board);


        Location loc = new(6,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.ROOK)))
                );
    }

    [Theory]
    [InlineData(0,1)]
    [InlineData(1,2)]
    [InlineData(4,5)]
    [InlineData(6,7)]
    public static void WhitePawn_Row6_Col0to6_AttackRight_BlockInFront_AttackForEachPromotionTypeReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(7,startColumn)] = PIECE.BLACK_ROOK;
        Board[BoardArrayLocation(7,attackColumn)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board);


        Location loc = new(6,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(7,attackColumn), PROMOTION_PIECE.ROOK)))
                );
    }


    [Theory]
    [InlineData(1,0)]
    [InlineData(4,3)]
    [InlineData(3,2)]
    [InlineData(6,5)]
    [InlineData(7,6)]
    public static void WhitePawn_Row4_Col1to7_AttackEnPassanteLeft_BlockInFront_AttackForEnPassanteReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,startColumn)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, EnPassanteSquare: new Location(5, attackColumn));

        Location loc = new(4,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,attackColumn))))
                );
    }

    [Theory]
    [InlineData(0,1)]
    [InlineData(1,2)]
    [InlineData(4,5)]
    [InlineData(3,4)]
    [InlineData(6,7)]
    public static void WhitePawn_Row5_Col0to6_AttackEnPassanteRight_BlockInFront_AttackForEnPassanteReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(4,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,startColumn)] = PIECE.BLACK_ROOK;
        BoardState state = new(Board, EnPassanteSquare: new Location(5, attackColumn));

        Location loc = new(4,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,attackColumn))))
                );
    }

    [Fact]
    public static void WhitePawn_NoMovesOnBlacksTurn()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(5,5)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);

        Location loc = new(5,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }
}
