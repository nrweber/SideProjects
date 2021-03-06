using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_IsBlackInCheck
{
    [Fact]
    public void BlackIsInCheck_NoCheck()
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.NONE,         PIECE.NONE,       PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.NONE,         PIECE.NONE,       PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.NONE,         PIECE.WHITE_KING, PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.BLACK_KING, PIECE.NONE, PIECE.NONE,         PIECE.NONE,       PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.WHITE_PAWN, PIECE.NONE, PIECE.WHITE_QUEEN,  PIECE.NONE,       PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.WHITE_KNIGHT, PIECE.NONE,       PIECE.NONE},
                {PIECE.NONE,         PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.NONE,         PIECE.NONE,       PIECE.NONE},
                {PIECE.WHITE_BISHOP, PIECE.NONE, PIECE.NONE, PIECE.NONE,       PIECE.NONE, PIECE.NONE,         PIECE.NONE,       PIECE.NONE},
            }
        };

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.False(result);
    }


    [Theory]
    //down and to the left
    [InlineData(3,4,2,3)]
    [InlineData(5,6,4,5)]
    //down and to the right
    [InlineData(3,4,2,5)]
    [InlineData(5,6,4,7)]
    // Edge of board tests to make sure these do not crash
    [InlineData(5,7,4,6)]
    [InlineData(6,0,5,1)]
    public void BlackIsInCheck_ByPawnAttack(int kingRow, int kingCol, int pawnRow, int pawnCol)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[pawnRow,pawnCol] = PIECE.WHITE_PAWN;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.True(result);
    }

    [Theory]
    //From the left
    [InlineData(3,4,3,3, PIECE.WHITE_QUEEN)]
    [InlineData(3,4,3,2, PIECE.WHITE_ROOK)]
    [InlineData(3,4,3,1, PIECE.WHITE_QUEEN)]
    [InlineData(3,4,3,0, PIECE.WHITE_ROOK)]
    [InlineData(5,6,5,5, PIECE.WHITE_QUEEN)]
    [InlineData(5,6,5,4, PIECE.WHITE_ROOK)]
    [InlineData(5,6,5,3, PIECE.WHITE_QUEEN)]
    [InlineData(5,6,5,2, PIECE.WHITE_ROOK)]
    [InlineData(5,6,5,1, PIECE.WHITE_QUEEN)]
    [InlineData(5,6,5,0, PIECE.WHITE_ROOK)]
    //From the right
    [InlineData(3,4,3,5, PIECE.WHITE_QUEEN)]
    [InlineData(3,4,3,6, PIECE.WHITE_ROOK)]
    [InlineData(3,4,3,7, PIECE.WHITE_QUEEN)]
    [InlineData(5,2,5,3, PIECE.WHITE_QUEEN)]
    [InlineData(5,2,5,4, PIECE.WHITE_ROOK)]
    [InlineData(5,2,5,5, PIECE.WHITE_QUEEN)]
    [InlineData(5,2,5,6, PIECE.WHITE_ROOK)]
    [InlineData(5,2,5,7, PIECE.WHITE_QUEEN)]
    //From above
    [InlineData(4,3,5,3, PIECE.WHITE_QUEEN)]
    [InlineData(4,3,6,3, PIECE.WHITE_ROOK)]
    [InlineData(4,3,7,3, PIECE.WHITE_QUEEN)]
    [InlineData(1,2,2,2, PIECE.WHITE_QUEEN)]
    [InlineData(1,2,3,2, PIECE.WHITE_ROOK)]
    [InlineData(1,2,4,2, PIECE.WHITE_QUEEN)]
    [InlineData(1,2,5,2, PIECE.WHITE_ROOK)]
    [InlineData(1,2,6,2, PIECE.WHITE_QUEEN)]
    [InlineData(1,2,7,2, PIECE.WHITE_QUEEN)]
    //From below
    [InlineData(4,3,3,3, PIECE.WHITE_QUEEN)]
    [InlineData(4,3,2,3, PIECE.WHITE_ROOK)]
    [InlineData(4,3,1,3, PIECE.WHITE_QUEEN)]
    [InlineData(4,3,0,3, PIECE.WHITE_QUEEN)]
    [InlineData(7,2,6,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2,5,2, PIECE.WHITE_ROOK)]
    [InlineData(7,2,4,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2,3,2, PIECE.WHITE_ROOK)]
    [InlineData(7,2,2,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2,1,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2,0,2, PIECE.WHITE_QUEEN)]
    public void BlackIsInCheck_ByColumnOrRowAttack(int kingRow, int kingCol, int pieceRow, int pieceCol, PIECE attackingPiece)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[pieceRow, pieceCol] = attackingPiece;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.True(result);
    }


    [Theory]
    //From the left, other pieces is in the way
    [InlineData(3,4, 3,3,  3,2, PIECE.WHITE_QUEEN)]
    [InlineData(3,4, 3,2,  3,1, PIECE.WHITE_ROOK)]
    [InlineData(3,4, 3,1,  3,0, PIECE.WHITE_QUEEN)]
    [InlineData(5,6, 5,5, 5,4, PIECE.WHITE_ROOK)]
    [InlineData(5,6, 5,4, 5,3, PIECE.WHITE_QUEEN)]
    [InlineData(5,6, 5,3, 5,2, PIECE.WHITE_ROOK)]
    [InlineData(5,6, 5,2, 5,1, PIECE.WHITE_QUEEN)]
    [InlineData(5,6, 5,1, 5,0, PIECE.WHITE_ROOK)]
    //From the right, other piece is in the way
    [InlineData(3,4, 3,5, 3,6, PIECE.WHITE_ROOK)]
    [InlineData(3,4, 3,6, 3,7, PIECE.WHITE_QUEEN)]
    [InlineData(5,2, 5,3, 5,4, PIECE.WHITE_ROOK)]
    [InlineData(5,2, 5,4, 5,5, PIECE.WHITE_QUEEN)]
    [InlineData(5,2, 5,5, 5,6, PIECE.WHITE_ROOK)]
    [InlineData(5,2, 5,6, 5,7, PIECE.WHITE_QUEEN)]
    //From above, other piece is in the way
    [InlineData(4,3, 5,3, 6,3, PIECE.WHITE_ROOK)]
    [InlineData(4,3, 6,3, 7,3, PIECE.WHITE_QUEEN)]
    [InlineData(1,2, 2,2, 3,2, PIECE.WHITE_ROOK)]
    [InlineData(1,2, 3,2, 4,2, PIECE.WHITE_QUEEN)]
    [InlineData(1,2, 3,2, 5,2, PIECE.WHITE_ROOK)]
    [InlineData(1,2, 5,2, 6,2, PIECE.WHITE_QUEEN)]
    [InlineData(1,2, 6,2, 7,2, PIECE.WHITE_QUEEN)]
    //From below, other piece is in the way
    [InlineData(4,3, 3,3, 2,3, PIECE.WHITE_ROOK)]
    [InlineData(4,3, 2,3, 1,3, PIECE.WHITE_QUEEN)]
    [InlineData(4,3, 2,3, 0,3, PIECE.WHITE_QUEEN)]
    [InlineData(7,2, 6,2, 5,2, PIECE.WHITE_ROOK)]
    [InlineData(7,2, 5,2, 4,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2, 5,2, 3,2, PIECE.WHITE_ROOK)]
    [InlineData(7,2, 3,2, 2,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2, 2,2, 1,2, PIECE.WHITE_QUEEN)]
    [InlineData(7,2, 1,2, 0,2, PIECE.WHITE_QUEEN)]
    public void BlackIsInCheck_NotByColumnOrRowAttackBecausePieceBlockingAttack(int kingRow, int kingCol, int blockingRow, int blockingCol, int pieceRow, int pieceCol, PIECE attackingPiece)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[pieceRow, pieceCol] = attackingPiece;
        state.Board[blockingRow, blockingCol] = PIECE.WHITE_PAWN;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.False(result);
    }

    [Theory]
    // up left   r+2  c-1
    [InlineData(4,2,  6,1)]
    [InlineData(2,4,  4,3)]
    // up right  r+2  c+1
    [InlineData(3,6,  5,7)]
    [InlineData(1,2,  3,3)]
    // right up  r+1  c+2
    [InlineData(6,3,  7,5)]
    [InlineData(2,2,  3,4)]
    // right down  r-1  c+2
    [InlineData(7,5,  6,7)]
    [InlineData(6,2,  5,4)]
    // down right  r-2 c+1
    [InlineData(3,3,  1,4)]
    [InlineData(2,5,  0,4)]
    // down left  r-2 c-1
    [InlineData(6,6,  4,5)]
    [InlineData(3,2,  1,1)]
    // left down r-1 c-2
    [InlineData(7,3,  6,1)]
    [InlineData(4,5,  3,3)]
    // left up  r+1 c-2
    [InlineData(3,4,  4,2)]
    [InlineData(1,2,  2,0)]
    public void BlackIsInCheck_ByKnight(int kingRow, int kingCol, int knightRow, int knightCol)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[knightRow, knightCol] = PIECE.WHITE_KNIGHT;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.True(result);
    }

    [Theory]
    // up left   r+2  c-1
    [InlineData(4,2,  6,1, PIECE.WHITE_PAWN)]
    [InlineData(2,4,  4,3, PIECE.WHITE_QUEEN)]
    // up right  r+2  c+1
    [InlineData(3,6,  5,7, PIECE.WHITE_ROOK)]
    [InlineData(1,2,  3,3, PIECE.NONE)]
    // right up  r+1  c+2
    [InlineData(6,3,  7,5, PIECE.WHITE_BISHOP)]
    [InlineData(2,2,  3,4, PIECE.NONE)]
    // right down  r-1  c+2
    [InlineData(7,5,  6,7, PIECE.WHITE_PAWN)]
    [InlineData(6,2,  5,4, PIECE.WHITE_QUEEN)]
    // down right  r-2 c+1
    [InlineData(3,3,  1,4, PIECE.WHITE_ROOK)]
    [InlineData(2,5,  0,4, PIECE.NONE)]
    // down left  r-2 c-1
    [InlineData(6,6,  4,5, PIECE.WHITE_BISHOP)]
    [InlineData(3,2,  1,1, PIECE.NONE)]
    // left down r-1 c-2
    [InlineData(7,3,  6,1, PIECE.WHITE_PAWN)]
    [InlineData(4,5,  3,3, PIECE.WHITE_QUEEN)]
    // left up  r+1 c-2
    [InlineData(3,4,  4,2, PIECE.WHITE_BISHOP)]
    [InlineData(1,2,  2,0, PIECE.NONE)]
    public void BlackIsInCheck_FalseIfNotAKnightAKnightsMoveAway(int kingRow, int kingCol, int otherRow, int otherCol, PIECE otherPiece)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[otherRow, otherCol] = otherPiece;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.False(result);
    }

    [Theory]
    //Up and to the left
    [InlineData(3,4,4,3, PIECE.WHITE_QUEEN)]
    [InlineData(3,4,5,2, PIECE.WHITE_BISHOP)]
    [InlineData(3,4,6,1, PIECE.WHITE_QUEEN)]
    [InlineData(3,4,7,0, PIECE.WHITE_BISHOP)]
    [InlineData(0,7,1,6, PIECE.WHITE_QUEEN)]
    [InlineData(0,7,3,4, PIECE.WHITE_BISHOP)]
    [InlineData(0,7,5,2, PIECE.WHITE_QUEEN)]
    [InlineData(0,7,6,1, PIECE.WHITE_BISHOP)]
    [InlineData(0,7,7,0, PIECE.WHITE_BISHOP)]
    //up and to the right
    [InlineData(4,3,5,4, PIECE.WHITE_QUEEN)]
    [InlineData(4,3,6,5, PIECE.WHITE_BISHOP)]
    [InlineData(4,3,7,6, PIECE.WHITE_BISHOP)]
    [InlineData(0,1,1,2, PIECE.WHITE_QUEEN)]
    [InlineData(0,1,3,4, PIECE.WHITE_QUEEN)]
    [InlineData(0,1,5,6, PIECE.WHITE_BISHOP)]
    [InlineData(0,1,6,7, PIECE.WHITE_QUEEN)]
    //Down and to the right
    [InlineData(3,2,2,3, PIECE.WHITE_QUEEN)]
    [InlineData(3,2,1,4, PIECE.WHITE_BISHOP)]
    [InlineData(3,2,0,5, PIECE.WHITE_QUEEN)]
    [InlineData(7,0,6,1, PIECE.WHITE_QUEEN)]
    [InlineData(7,0,5,2, PIECE.WHITE_BISHOP)]
    [InlineData(7,0,3,4, PIECE.WHITE_BISHOP)]
    [InlineData(7,0,1,6, PIECE.WHITE_QUEEN)]
    [InlineData(7,0,0,7, PIECE.WHITE_QUEEN)]
    //Down and to the left
    [InlineData(4,3,3,2, PIECE.WHITE_QUEEN)]
    [InlineData(4,3,2,1, PIECE.WHITE_BISHOP)]
    [InlineData(4,3,1,0, PIECE.WHITE_QUEEN)]
    [InlineData(7,7,6,6, PIECE.WHITE_QUEEN)]
    [InlineData(7,7,5,5, PIECE.WHITE_BISHOP)]
    [InlineData(7,7,3,3, PIECE.WHITE_BISHOP)]
    [InlineData(7,7,0,0, PIECE.WHITE_QUEEN)]
    public void BlackIsInCheck_ByDiagonalAttack(int kingRow, int kingCol, int pieceRow, int pieceCol, PIECE attackingPiece)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[pieceRow, pieceCol] = attackingPiece;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.True(result);
    }

    [Theory]
    //Up and to the left
    [InlineData(3,4, 4,3, 5,2, PIECE.WHITE_BISHOP)]
    [InlineData(3,4, 5,2, 6,1, PIECE.WHITE_QUEEN)]
    [InlineData(3,4, 5,2, 7,0, PIECE.WHITE_BISHOP)]
    [InlineData(0,7, 1,6, 3,4, PIECE.WHITE_BISHOP)]
    [InlineData(0,7, 3,4, 5,2, PIECE.WHITE_QUEEN)]
    [InlineData(0,7, 5,2, 6,1, PIECE.WHITE_BISHOP)]
    [InlineData(0,7, 3,4, 7,0, PIECE.WHITE_BISHOP)]
    //up and to the right
    [InlineData(4,3, 5,4, 6,5, PIECE.WHITE_BISHOP)]
    [InlineData(4,3, 6,5, 7,6, PIECE.WHITE_BISHOP)]
    [InlineData(0,1, 1,2, 3,4, PIECE.WHITE_QUEEN)]
    [InlineData(0,1, 2,3, 5,6, PIECE.WHITE_BISHOP)]
    [InlineData(0,1, 5,6, 6,7, PIECE.WHITE_QUEEN)]
    //Down and to the right
    [InlineData(3,2, 2,3, 1,4, PIECE.WHITE_BISHOP)]
    [InlineData(3,2, 1,4, 0,5, PIECE.WHITE_QUEEN)]
    [InlineData(7,0, 6,1, 5,2, PIECE.WHITE_BISHOP)]
    [InlineData(7,0, 5,2, 3,4, PIECE.WHITE_BISHOP)]
    [InlineData(7,0, 4,3, 1,6, PIECE.WHITE_QUEEN)]
    [InlineData(7,0, 1,6, 0,7, PIECE.WHITE_QUEEN)]
    //Down and to the left
    [InlineData(4,3, 3,2, 2,1, PIECE.WHITE_BISHOP)]
    [InlineData(4,3, 2,1, 1,0, PIECE.WHITE_QUEEN)]
    [InlineData(7,7, 6,6, 5,5, PIECE.WHITE_BISHOP)]
    [InlineData(7,7, 5,5, 3,3, PIECE.WHITE_BISHOP)]
    [InlineData(7,7, 2,2, 0,0, PIECE.WHITE_QUEEN)]
    public void BlackIsInCheck_NotByDiagonalAttackBecausePieceBlockingAttack(int kingRow, int kingCol, int blockingRow, int blockingCol, int pieceRow, int pieceCol, PIECE attackingPiece)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[pieceRow, pieceCol] = attackingPiece;
        state.Board[blockingRow, blockingCol] = PIECE.WHITE_ROOK;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.False(result);
    }

    [Theory]
    [InlineData(4,3,  5,2)]
    [InlineData(4,3,  5,3)]
    [InlineData(4,3,  5,4)]
    [InlineData(4,3,  4,2)]
    [InlineData(4,3,  4,4)]
    [InlineData(4,3,  3,2)]
    [InlineData(4,3,  3,3)]
    [InlineData(4,3,  3,4)]
    //Bonds checking
    [InlineData(0,0,  0,1)]
    [InlineData(7,4,  6,5)]
    [InlineData(2,0,  2,1)]
    [InlineData(6,7,  7,7)]
    public void BlackIsInCheck_ByOtherKing(int kingRow, int kingCol, int attackingRow, int attackingCol)
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };
        state.Board[kingRow,kingCol] = PIECE.BLACK_KING;
        state.Board[attackingRow, attackingCol] = PIECE.WHITE_KING;

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.True(result);
    }

    [Fact]
    public void BlackIsInCheck_FalseIfNoKing()
    {
        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };

        bool result = ChessHelper.BlackIsInCheck(state);

        Assert.False(result);
    }
}
