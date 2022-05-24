using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_BlackPawn
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
    public static void BlackPawn_Row6_NoAttacks_NoBlocks_OneAndTwoSquareMovesReturned(int column)
    {
        BoardState state = new(){CurrentTurn = PLAYER.BLACK}; // starting board works for this test, just set it to balck's turn

        Location loc = new(6,column);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,column))))
                );
    }

    [Theory]
    [InlineData(1, PIECE.WHITE_PAWN)]
    [InlineData(3, PIECE.WHITE_PAWN)]
    [InlineData(5, PIECE.WHITE_PAWN)]
    public static void BlackPawn_Row6_NoAttacks_BlockTwoAhead_OneSquareMoveReturnedOnly(int column, PIECE blockingPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,column)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(4,column)] = blockingPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(6,column);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,column))))
                );
    }

    [Theory]
    [InlineData(2, PIECE.WHITE_PAWN)]
    [InlineData(5, PIECE.WHITE_PAWN)]
    [InlineData(7, PIECE.WHITE_PAWN)]
    public static void BlackPawn_Row6_NoAttacks_BlockOneAhead_NoMovesReturned(int column, PIECE blockingPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,column)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,column)] = blockingPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(6,column);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }


    [Theory]
    [InlineData(6,1, 5,0, PIECE.WHITE_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(5,3, 4,2, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(4,5, 3,4, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(3,5, 2,4, PIECE.WHITE_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(5,5, 4,4, PIECE.WHITE_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(4,7, 3,6, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    public static void BlackPawn_Row6to3_Col1to7_AttackWhiteLeft_BlockOneAhead_OnlyAttackMoveReturned(int startRow, int startColumn, int endRow, int endColumn, PIECE blockingPiece, PIECE attackedPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(endRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(endRow,endColumn)] = attackedPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,endColumn))))
                );
    }


    [Theory]
    [InlineData(6,7, 5,6, PIECE.WHITE_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(5,6, 4,5, PIECE.WHITE_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(4,5, 3,4, PIECE.WHITE_PAWN, PIECE.BLACK_ROOK)]
    [InlineData(3,4, 2,3, PIECE.WHITE_PAWN, PIECE.BLACK_KNIGHT)]
    [InlineData(5,3, 4,2, PIECE.WHITE_PAWN, PIECE.BLACK_BISHOP)]
    [InlineData(4,2, 3,1, PIECE.WHITE_PAWN, PIECE.BLACK_QUEEN)]
    [InlineData(3,1, 2,0, PIECE.WHITE_PAWN, PIECE.BLACK_KING)]
    public static void BlackPawn_Row6to3_Col1to7_BlackPieceInLeftAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(attackedRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(attackedRow,attackedColumn)] = otherPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(6,0, 5,1, PIECE.WHITE_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(5,1, 4,2, PIECE.WHITE_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(4,3, 3,4, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(3,5, 2,6, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(4,3, 3,4, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(5,5, 4,6, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    public static void BlackPawn_Row6to3_Col0to6_AttackWhiteRight_BlockOneAhead_AttackMoveReturned(int startRow, int startColumn, int endRow, int endColumn, PIECE blockingPiece, PIECE attackedPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(endRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(endRow,endColumn)] = attackedPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,endColumn))))
                );
    }

    [Theory]
    [InlineData(6,5, 5,6, PIECE.WHITE_PAWN, PIECE.BLACK_KING)]
    [InlineData(5,4, 4,5, PIECE.WHITE_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(4,3, 3,4, PIECE.WHITE_PAWN, PIECE.BLACK_PAWN)]
    [InlineData(3,2, 2,3, PIECE.WHITE_PAWN, PIECE.BLACK_ROOK)]
    [InlineData(4,1, 3,2, PIECE.WHITE_PAWN, PIECE.BLACK_KNIGHT)]
    [InlineData(3,5, 2,6, PIECE.WHITE_PAWN, PIECE.BLACK_BISHOP)]
    [InlineData(5,6, 4,7, PIECE.WHITE_PAWN, PIECE.BLACK_QUEEN)]
    public static void BlackPawn_Row6to3_Col0to6_BlackPieceInRightAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(attackedRow,startColumn)] = blockingPiece;
        Board[BoardArrayLocation(attackedColumn,attackedColumn)] = otherPiece;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(1, 0, 2)]
    [InlineData(3, 2, 4)]
    [InlineData(6, 5, 7)]
    public static void BlackPawn_Row1_Col1to6_BothAttacks_NoBlocks_BothMovesAndBothAttacksAreReturned(int column, int attackColumn1, int attackColumn2)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,column)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,attackColumn1)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(5,attackColumn2)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(6,column);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,attackColumn1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,attackColumn2))))
                );
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(7, 6)]
    public static void BlackPawn_Row6_Col0and7_OnlyPossibleAttacks_NoBlocks_BothMovesAndTheAttackMoveAreReturned(int column, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,column)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(5,attackColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(6,column);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(5,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(4,column)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(5,attackColumn))))
                );
    }

    [Theory]
    [InlineData(5,0, 4)] // covers that edge doesn't crash looking for attackers
    [InlineData(4,3, 3)]
    [InlineData(3,6, 2)]
    [InlineData(2,7, 1)] // covers that edge doesn't crash looking for attackers
    public static void BlackPawn_Row5to2_NoAttacks_NoBlocks_OneSquareMoveReturned(int startRow, int startColumn, int endRow)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,startColumn))))
                );
    }

    [Theory]
    [InlineData(2,0, 1)]
    [InlineData(4,3, 3)]
    [InlineData(5,6, 4)]
    [InlineData(3,7, 2)]
    public static void BlackPawn_Row5to2_NoAttacks_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int blockRow)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(blockRow,startColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(3,1, 2, 2,0, 2,2)]
    [InlineData(2,3, 1, 1,2, 1,4)]
    [InlineData(4,6, 3, 3,5, 3,7)]
    [InlineData(5,2, 4, 4,1, 4,3)]
    public static void BlackPawn_Row5to2_Col1to6_BothAttacks_NoBlocks_ForwardMoveAndBothAttacksAreReturned(int startRow, int startColumn, int endRow, int attackRow1, int attackColumn1, int attackRow2,int attackColumn2)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(attackRow1,attackColumn1)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(attackRow2,attackColumn2)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(startRow,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

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
    public static void BlackPawn_Row1_Col0to7_NoAttacks_NoBlocks_OneSquareMoveForEachPromotionPieceReturned(int startColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,startColumn)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(1,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(0,startColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,startColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,startColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,startColumn), PROMOTION_PIECE.ROOK)))
                );
    }

    [Theory]
    [InlineData(1,0)]
    [InlineData(3,2)]
    [InlineData(5,4)]
    [InlineData(7,6)]
    public static void BlackPawn_Row1_Col1to7_AttackLeft_BlockInFront_AttackForEachPromotionTypeReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(0,startColumn)] = PIECE.WHITE_ROOK;
        Board[BoardArrayLocation(0,attackColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(1,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.ROOK)))
                );
    }

    [Theory]
    [InlineData(0,1)]
    [InlineData(1,2)]
    [InlineData(4,5)]
    [InlineData(6,7)]
    public static void BlackPawn_Row1_Col0to6_AttackRight_BlockInFront_AttackForEachPromotionTypeReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(0,startColumn)] = PIECE.WHITE_ROOK;
        Board[BoardArrayLocation(0,attackColumn)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);


        Location loc = new(1,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.QUEEN))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.BISHOP))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.KNIGHT))),
                item => Assert.True(item.Equals(new Move(loc, new Location(0,attackColumn), PROMOTION_PIECE.ROOK)))
                );
    }


    [Theory]
    [InlineData(1,0)]
    [InlineData(4,3)]
    [InlineData(3,2)]
    [InlineData(6,5)]
    [InlineData(7,6)]
    public static void BlackPawn_Row3_Col1to7_AttackEnPassanteLeft_BlockInFront_AttackForEnPassanteReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(3,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,startColumn)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, EnPassanteSquare: new Location(2, attackColumn));

        Location loc = new(3,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn))))
                );
    }

    [Theory]
    [InlineData(0,1)]
    [InlineData(1,2)]
    [InlineData(4,5)]
    [InlineData(3,4)]
    [InlineData(6,7)]
    public static void BlackPawn_Row3_Col0to6_AttackEnPassanteRight_BlockInFront_AttackForEnPassanteReturned(int startColumn, int attackColumn)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(3,startColumn)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(2,startColumn)] = PIECE.WHITE_ROOK;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK, EnPassanteSquare: new Location(2, attackColumn));

        Location loc = new(3,startColumn);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn))))
                );
    }

    [Fact]
    public static void WhitePawn_NoMovesOnBlacksTurn()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(5,5)] = PIECE.WHITE_PAWN;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);



        Location loc = new(5,5);
        var moves = state.PossibleMovesForLocation(loc);

        Assert.Empty(moves);
    }
}
