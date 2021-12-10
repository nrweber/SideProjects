using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation_BlackPawn
{
    private static BoardState CreateStateWithBlankBoard()
    {
        BoardState state = new();
        state.CurrentTurn = PLAYER.BLACK;

        for(int r = 0; r <= 7; r++)
        {
            for(int c = 0; c <= 7; c++)
            {
                state.Board[r,c] = PIECE.NONE;
            }
        }

        return state;
    }

    [Theory]
    [InlineData(0)] // covers that edge doesn't crash looking for attackers
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(7)] // covers that edge doesn't crash looking for attackers
    public static void BlackPawn_Row6_NoAttacks_NoBlocks_OneAndTwoSquareMovesReturned(int column)
    {
        BoardState state = new(); // starting board works for this test, just set it to balck's turn
        state.CurrentTurn = PLAYER.BLACK;

        Location loc = new(6,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[6,column] = PIECE.BLACK_PAWN;
        state.Board[4,column] = blockingPiece;


        Location loc = new(6,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[6,column] = PIECE.BLACK_PAWN;
        state.Board[5,column] = blockingPiece;


        Location loc = new(6,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }


    [Theory]
    [InlineData(6,1, 5,0, PIECE.WHITE_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(5,3, 4,2, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(4,5, 3,4, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(3,5, 2,4, PIECE.WHITE_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(5,5, 4,4, PIECE.WHITE_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(4,7, 3,6, PIECE.WHITE_PAWN, PIECE.WHITE_ROOK)]
    public static void WhitePawn_Row6to3_Col1to7_AttackWhiteLeft_BlockOneAhead_OnlyAttackMoveReturned(int startRow, int startColumn, int endRow, int endColumn, PIECE blockingPiece, PIECE attackedPiece)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;
        state.Board[endRow,startColumn] = blockingPiece;
        state.Board[endRow,endColumn] = attackedPiece;


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;
        state.Board[attackedRow,startColumn] = blockingPiece;
        state.Board[attackedRow,attackedColumn] = otherPiece;


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;
        state.Board[endRow,startColumn] = blockingPiece;
        state.Board[endRow,endColumn] = attackedPiece;


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
        state.Board[attackedRow,startColumn] = blockingPiece;
        state.Board[attackedColumn,attackedColumn] = otherPiece;


        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(1, 0, 2)]
    [InlineData(3, 2, 4)]
    [InlineData(6, 5, 7)]
    public static void BlackPawn_Row1_Col1to6_BothAttacks_NoBlocks_BothMovesAndBothAttacksAreReturned(int column, int attackColumn1, int attackColumn2)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[6,column] = PIECE.BLACK_PAWN;
        state.Board[5,attackColumn1] = PIECE.WHITE_PAWN;
        state.Board[5,attackColumn2] = PIECE.WHITE_PAWN;


        Location loc = new(6,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[6,column] = PIECE.BLACK_PAWN;
        state.Board[5,attackColumn] = PIECE.WHITE_PAWN;


        Location loc = new(6,column);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;

        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;
        state.Board[blockRow,startColumn] = PIECE.WHITE_PAWN;

        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(3,1, 2, 2,0, 2,2)]
    [InlineData(2,3, 1, 1,2, 1,4)]
    [InlineData(4,6, 3, 3,5, 3,7)]
    [InlineData(5,2, 4, 4,1, 4,3)]
    public static void BlackPawn_Row5to2_Col1to6_BothAttacks_NoBlocks_ForwardMoveAndBothAttacksAreReturned(int startRow, int startColumn, int endRow, int attackRow1, int attackColumn1, int attackRow2,int attackColumn2)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.BLACK_PAWN;
        state.Board[attackRow1,attackColumn1] = PIECE.WHITE_PAWN;
        state.Board[attackRow2,attackColumn2] = PIECE.WHITE_PAWN;

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
    public static void BlackPawn_Row1_Col0to7_NoAttacks_NoBlocks_OneSquareMoveForEachPromotionPieceReturned(int startColumn)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,startColumn] = PIECE.BLACK_PAWN;

        Location loc = new(1,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,startColumn] = PIECE.BLACK_PAWN;
        state.Board[0,startColumn] = PIECE.WHITE_ROOK;
        state.Board[0,attackColumn] = PIECE.WHITE_PAWN;

        Location loc = new(1,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,startColumn] = PIECE.BLACK_PAWN;
        state.Board[0,startColumn] = PIECE.WHITE_ROOK;
        state.Board[0,attackColumn] = PIECE.WHITE_PAWN;

        Location loc = new(1,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[3,startColumn] = PIECE.BLACK_PAWN;
        state.Board[2,startColumn] = PIECE.WHITE_ROOK;

        state.EnPassantSquare = new Location(2, attackColumn);

        Location loc = new(3,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[3,startColumn] = PIECE.BLACK_PAWN;
        state.Board[2,startColumn] = PIECE.WHITE_ROOK;

        state.EnPassantSquare = new Location(2, attackColumn);

        Location loc = new(3,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(2,attackColumn))))
                );
    }

    [Fact]
    public static void WhitePawn_NoMovesOnBlacksTurn()
    {
        BoardState state = CreateStateWithBlankBoard();
        state.CurrentTurn = PLAYER.WHITE;

        state.Board[5,5] = PIECE.BLACK_PAWN;;


        Location loc = new(5,5);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }
}
