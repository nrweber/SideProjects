using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PossibleMovesForLocation
{
    private static BoardState CreateStateWithBlankBoard()
    {
        BoardState state = new();

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
    [InlineData(5,4)]
    [InlineData(4,0)]
    [InlineData(3,3)]
    public static void NoMovesforEmptySquare(int row, int col)
    {
        BoardState state = new();
        Location loc = new(row,col);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,column] = PIECE.WHITE_PAWN;
        state.Board[3,column] = blockingPiece;


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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,column] = PIECE.WHITE_PAWN;
        state.Board[2,column] = blockingPiece;


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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
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
    [InlineData(1,7, 2,6, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(2,6, 3,5, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(3,5, 4,4, PIECE.BLACK_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(4,4, 5,3, PIECE.BLACK_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(5,3, 6,2, PIECE.BLACK_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(1,2, 2,1, PIECE.BLACK_PAWN, PIECE.WHITE_QUEEN)]
    [InlineData(2,1, 3,0, PIECE.BLACK_PAWN, PIECE.WHITE_KING)]
    public static void WhitePawn_Row1to5_Col1to7_WhitePieceInLeftAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
        state.Board[attackedRow,startColumn] = blockingPiece;
        state.Board[attackedRow,attackedColumn] = otherPiece;


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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
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
    [InlineData(1,5, 2,6, PIECE.BLACK_PAWN, PIECE.WHITE_KING)]
    [InlineData(2,4, 3,5, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(4,3, 5,4, PIECE.BLACK_PAWN, PIECE.WHITE_PAWN)]
    [InlineData(3,2, 4,3, PIECE.BLACK_PAWN, PIECE.WHITE_ROOK)]
    [InlineData(2,1, 3,2, PIECE.BLACK_PAWN, PIECE.WHITE_KNIGHT)]
    [InlineData(0,5, 1,6, PIECE.BLACK_PAWN, PIECE.WHITE_BISHOP)]
    [InlineData(1,6, 2,7, PIECE.BLACK_PAWN, PIECE.WHITE_QUEEN)]
    public static void WhitePawn_Row1to5_Col0to6_WhitePieceInRightAttackSquare_BlockOneAhead_NoMovesReturned(int startRow, int startColumn, int attackedRow, int attackedColumn, PIECE blockingPiece, PIECE otherPiece)
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
    public static void WhitePawn_Row1_Col1to6_BothAttacks_NoBlocks_BothMovesAndBothAttacksAreReturned(int column, int attackColumn1, int attackColumn2)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,column] = PIECE.WHITE_PAWN;
        state.Board[2,attackColumn1] = PIECE.BLACK_PAWN;
        state.Board[2,attackColumn2] = PIECE.BLACK_PAWN;


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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[1,column] = PIECE.WHITE_PAWN;
        state.Board[2,attackColumn] = PIECE.BLACK_PAWN;


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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;

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
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
        state.Board[blockRow,startColumn] = PIECE.BLACK_PAWN;

        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.Empty(moves);
    }

    [Theory]
    [InlineData(3,1, 4, 4,0, 4,2)]
    [InlineData(2,3, 3, 3,2, 3,4)]
    [InlineData(4,6, 5, 5,5, 5,7)]
    [InlineData(5,2, 6, 6,1, 6,3)]
    public static void WhitePawn_Row2to5_Col1to6_BothAttacks_NoBlocks_BothMovesAndBothAttacksAreReturned(int startRow, int startColumn, int endRow, int attackRow1, int attackColumn1, int attackRow2,int attackColumn2)
    {
        BoardState state = CreateStateWithBlankBoard();

        state.Board[startRow,startColumn] = PIECE.WHITE_PAWN;
        state.Board[attackRow1,attackColumn1] = PIECE.BLACK_PAWN;
        state.Board[attackRow2,attackColumn2] = PIECE.BLACK_PAWN;

        Location loc = new(startRow,startColumn);
        var moves = ChessHelper.PossibleMovesForLocation(state, loc);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                item => Assert.True(item.Equals(new Move(loc, new Location(endRow,startColumn)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(attackRow1,attackColumn1)))),
                item => Assert.True(item.Equals(new Move(loc, new Location(attackRow2,attackColumn2))))
                );
    }

    //pawn
    //   White

    //      row   blocks  attacks     number of moves
    //      1     0       0           2
    //      1     2up     0           1
    //      1     1up     0           0
    //      1to6  1up     1left       1   //testing just attacks
    //      1to6  1up     1right      1   //testing just attacks
    //      1     0       2           4   //testing all
    //      2to5  0       0           1
    //      2to5  1up     0           0

    //      row 6: promotion on 1 square move and any attacks
    //   Black
    //      row 6: 1 square and 2 square moves and any attacks
    //      row 5 through 3: 1 square moves and any attacks
    //      row 2: 1 square move and any attacks including en passante
    //      row 1: promotion on 1 square move and any attacks

}
