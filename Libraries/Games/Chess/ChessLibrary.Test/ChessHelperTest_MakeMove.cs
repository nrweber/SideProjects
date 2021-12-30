using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_MakeMove
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

    [Fact]
    public void ValidMove_TrueAndNewStateReturned()
    {
        BoardState state = new();

        Move m = new Move(new Location(1,4), new Location(3,4));

        var (moveMade, newState) = ChessHelper.MakeMove(state,m);

        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, newState.Board[1,4]);
        Assert.Equal(PIECE.WHITE_PAWN, newState.Board[3,4]);
        Assert.Equal(PLAYER.BLACK, newState.CurrentTurn);
    }

    [Fact]
    public void InvalidMoveNotAllowed_FalseAndOldStateReturned()
    {
        BoardState state = new();

        Move m = new Move(new Location(1,4), new Location(4,4));

        var (moveMade, newState) = ChessHelper.MakeMove(state,m);

        Assert.False(moveMade);
        Assert.Equal(state, newState);
    }

    [Fact]
    public void ValuesOfPassedInBoardStateAreNotChanged()
    {
        BoardState state = new();

        Move m = new Move(new Location(1,4), new Location(3,4));

        var (_, newState) = ChessHelper.MakeMove(state,m);

        Assert.Equal(PIECE.WHITE_PAWN, state.Board[1,4]);
        Assert.Equal(PIECE.NONE, state.Board[3,4]);
        Assert.Equal(PLAYER.WHITE, state.CurrentTurn);

        Assert.Equal(PIECE.NONE, newState.Board[1,4]);
        Assert.Equal(PIECE.WHITE_PAWN, newState.Board[3,4]);
        Assert.Equal(PLAYER.BLACK, newState.CurrentTurn);
    }


    [Fact]
    public void HalfMoveIncreasedAfterEachNonePawnMove()
    {
        BoardState state = new();

        {
            Move m = new Move(new Location(0,1), new Location(2,2));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(1, state.HalfMovesSinceLastCaptureOrPawnMove);
        }

        {
            Move m = new Move(new Location(7,1), new Location(5,2));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(2, state.HalfMovesSinceLastCaptureOrPawnMove);
        }
    }

    [Fact]
    public void HalfMoveRestToZeroOnPawnMove()
    {
        BoardState state = new();
        state.HalfMovesSinceLastCaptureOrPawnMove = 5;

        {
            Move m = new Move(new Location(1,1), new Location(2,1));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(0, state.HalfMovesSinceLastCaptureOrPawnMove);
        }

        state.HalfMovesSinceLastCaptureOrPawnMove = 5;

        {
            Move m = new Move(new Location(6,4), new Location(5,4));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(0, state.HalfMovesSinceLastCaptureOrPawnMove);
        }
    }

    [Fact]
    public void HalfMoveRestToZeroAfterCapture()
    {
        BoardState state = new();

        //Rook takes pawn
        state.Board[1,0] = PIECE.BLACK_PAWN;
        Move m = new Move(new Location(0,0), new Location(1,0));

        state.HalfMovesSinceLastCaptureOrPawnMove = 5;

        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(0, state.HalfMovesSinceLastCaptureOrPawnMove);
    }

    [Theory]
    [InlineData(0,5)]
    [InlineData(1,4)]
    [InlineData(1,3)]
    public void WhiteCastlingBothFalseAfterKingMovesOffStartSquare(int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.WhiteCanQueenCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;

        Move m = new Move(new Location(0,4), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.WhiteCanKingCastle);
        Assert.False(state.WhiteCanQueenCastle);
    }

    [Theory]
    [InlineData(7,5)]
    [InlineData(6,4)]
    [InlineData(6,3)]
    public void BlackCastlingBothFalseAfterKingMovesOffStartSquare(int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.BlackCanQueenCastle = true;

        state.Board[7,4] = PIECE.BLACK_KING;

        Move m = new Move(new Location(7,4), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.BlackCanKingCastle);
        Assert.False(state.BlackCanQueenCastle);
    }


    [Fact]
    public void WhiteKingCastlingFalseAfterRookMovesOffg1Square_QueenCastlingStaysTrue()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.WhiteCanQueenCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        Move m = new Move(new Location(0,7), new Location(1,7));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.WhiteCanKingCastle);
        Assert.True(state.WhiteCanQueenCastle);
    }


    [Fact]
    public void WhiteQueenCastlingFalseAfterRookMovesOffa1Square_KingCastlingStaysTrue()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.WhiteCanQueenCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        Move m = new Move(new Location(0,0), new Location(1,0));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.WhiteCanKingCastle);
        Assert.False(state.WhiteCanQueenCastle);
    }

    [Theory]
    [InlineData(PIECE.WHITE_PAWN,    1,4,  2,4)]
    [InlineData(PIECE.WHITE_KNIGHT,  2,3,  4,4)]
    [InlineData(PIECE.WHITE_QUEEN,   5,6,  5,2)]
    public void WhiteCastlingStaysTrueForNoneKingRookMoves(PIECE movingPiece, int startRow, int startCol, int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;
        state.WhiteCanQueenCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[startRow,startCol] = movingPiece;

        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.WhiteCanKingCastle);
        Assert.True(state.WhiteCanQueenCastle);
    }

    [Fact]
    public void WhiteKingSideCastleMovesKingAndRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanKingCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,7] = PIECE.WHITE_ROOK;

        Move m = new Move(new Location(0,4), new Location(0,6));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.WHITE_KING, state.Board[0,6]);
        Assert.Equal(PIECE.WHITE_ROOK, state.Board[0,5]);
        Assert.Equal(PIECE.NONE, state.Board[0,7]);
    }

    [Fact]
    public void WhiteQueenSideCastleMovesKingAndRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.WhiteCanQueenCastle = true;

        state.Board[0,4] = PIECE.WHITE_KING;
        state.Board[0,0] = PIECE.WHITE_ROOK;

        Move m = new Move(new Location(0,4), new Location(0,2));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.WHITE_KING, state.Board[0,2]);
        Assert.Equal(PIECE.WHITE_ROOK, state.Board[0,3]);
        Assert.Equal(PIECE.NONE, state.Board[0,0]);
    }


    [Fact]
    public void BlackKingCastlingFalseAfterRookMovesOffg8Square_QueenCastlingStaysTrue()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.BlackCanQueenCastle = true;

        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        Move m = new Move(new Location(7,7), new Location(6,7));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.BlackCanKingCastle);
        Assert.True(state.BlackCanQueenCastle);
    }

    [Fact]
    public void BlackQueenCastlingFalseAfterRookMovesOffa8Square_KingCastlingStaysTrue()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.BlackCanQueenCastle = true;

        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        Move m = new Move(new Location(7,0), new Location(6,0));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.BlackCanKingCastle);
        Assert.False(state.BlackCanQueenCastle);
    }

    [Theory]
    [InlineData(PIECE.BLACK_PAWN,    6,4,  4,4)]
    [InlineData(PIECE.BLACK_KNIGHT,  2,3,  4,4)]
    [InlineData(PIECE.BLACK_QUEEN,   5,6,  5,2)]
    public void BlackCastlingStaysTrueForNoneKingRookMoves(PIECE movingPiece, int startRow, int startCol, int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;
        state.BlackCanQueenCastle = true;

        state.Board[0,4] = PIECE.BLACK_KING;
        state.Board[startRow,startCol] = movingPiece;

        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.BlackCanKingCastle);
        Assert.True(state.BlackCanQueenCastle);
    }

    [Fact]
    public void BlackKingSideCastleMovesKingAndRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanKingCastle = true;

        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,7] = PIECE.BLACK_ROOK;

        Move m = new Move(new Location(7,4), new Location(7,6));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.BLACK_KING, state.Board[7,6]);
        Assert.Equal(PIECE.BLACK_ROOK, state.Board[7,5]);
        Assert.Equal(PIECE.NONE, state.Board[7,7]);
    }

    [Fact]
    public void BlackQueenSideCastleMovesKingAndRook()
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.BlackCanQueenCastle = true;

        state.Board[7,4] = PIECE.BLACK_KING;
        state.Board[7,0] = PIECE.BLACK_ROOK;

        Move m = new Move(new Location(7,4), new Location(7,2));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.BLACK_KING, state.Board[7,2]);
        Assert.Equal(PIECE.BLACK_ROOK, state.Board[7,3]);
        Assert.Equal(PIECE.NONE, state.Board[7,0]);
    }

    [Fact]
    public void MoveCountIncreasedAfterBlackMove()
    {
        BoardState state = new();
        Assert.Equal(1, state.MoveNumber);

        {
            Move m = new Move(new Location(0,1), new Location(2,2));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(1, state.MoveNumber);
        }

        {
            Move m = new Move(new Location(7,1), new Location(5,2));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(2, state.MoveNumber);
        }

        {
            Move m = new Move(new Location(1,4), new Location(2,4));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(2, state.MoveNumber);
        }

        {
            Move m = new Move(new Location(6,3), new Location(4,3));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(3, state.MoveNumber);
        }
    }

    [Theory]
    [InlineData(PIECE.WHITE_PAWN, 1,4,  2,4)]
    [InlineData(PIECE.WHITE_PAWN, 1,3,  2,3)]
    [InlineData(PIECE.WHITE_PAWN, 1,6,  2,6)]
    [InlineData(PIECE.BLACK_PAWN, 6,2,  5,2)]
    [InlineData(PIECE.BLACK_PAWN, 6,5,  5,5)]
    [InlineData(PIECE.BLACK_PAWN, 6,7,  5,7)]
    public void EnPassanteLocationNullAfterSingleSquareMoveFromStartingPosition(PIECE movingPiece, int startRow, int startCol, int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard((movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK);
        state.EnPassantSquare = null;

        state.Board[startRow,startCol] = movingPiece;

        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Null(state.EnPassantSquare);
    }

    [Theory]
    [InlineData(PIECE.WHITE_PAWN, 1,4,  3,4, 2,4)]
    [InlineData(PIECE.WHITE_PAWN, 1,3,  3,3, 2,3)]
    [InlineData(PIECE.WHITE_PAWN, 1,6,  3,6, 2,6)]
    [InlineData(PIECE.BLACK_PAWN, 6,2,  4,2, 5,2)]
    [InlineData(PIECE.BLACK_PAWN, 6,5,  4,5, 5,5)]
    [InlineData(PIECE.BLACK_PAWN, 6,7,  4,7, 5,7)]
    public void EnPassanteLocationSetToMidSquareAfterTwoSquareMove(PIECE movingPiece, int startRow, int startCol, int endRow, int endCol, int enpRow, int enpCol)
    {
        BoardState state = CreateStateWithBlankBoard((movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK);
        state.EnPassantSquare = null;

        state.Board[startRow,startCol] = movingPiece;

        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(new Location(enpRow, enpCol), state.EnPassantSquare);
    }

    [Theory]
    [InlineData(PIECE.WHITE_PAWN, 1,4,  2,4)]
    [InlineData(PIECE.WHITE_PAWN, 1,3,  2,3)]
    [InlineData(PIECE.WHITE_PAWN, 1,6,  2,6)]
    [InlineData(PIECE.BLACK_PAWN, 6,2,  5,2)]
    [InlineData(PIECE.BLACK_PAWN, 6,5,  5,5)]
    [InlineData(PIECE.BLACK_PAWN, 6,7,  5,7)]
    public void EnPassanteLocationResetToNullAnyMoveThatDoesNotCauseEnPasante(PIECE movingPiece, int startRow, int startCol, int endRow, int endCol)
    {
        BoardState state = CreateStateWithBlankBoard((movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK);
        state.EnPassantSquare = new Location(2,3);

        state.Board[startRow,startCol] = movingPiece;

        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Null(state.EnPassantSquare);
    }


    [Theory]
    [InlineData(4,3,  5,4, 4,4)]
    [InlineData(4,3,  5,2, 4,2)]
    public void EnPassante_White_AttackedPieceIsRemoved(int startRow, int startCol, int epRow, int epCol, int otherRow, int otherCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.WHITE);
        state.EnPassantSquare = new Location(epRow,epCol);

        state.Board[startRow,startCol] = PIECE.WHITE_PAWN;
        state.Board[otherRow,otherCol] = PIECE.BLACK_PAWN;

        Move m = new Move(new Location(startRow,startCol), new Location(epRow,epCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, state.Board[startRow,startCol]);
        Assert.Equal(PIECE.NONE, state.Board[otherRow,otherCol]);
        Assert.Equal(PIECE.WHITE_PAWN, state.Board[epRow,epCol]);

    }

    [Theory]
    [InlineData(3,5,  2,6, 3,6)]
    [InlineData(3,5,  2,4, 3,4)]
    public void EnPassante_Black_AttackedPieceIsRemoved(int startRow, int startCol, int epRow, int epCol, int otherRow, int otherCol)
    {
        BoardState state = CreateStateWithBlankBoard(PLAYER.BLACK);
        state.EnPassantSquare = new Location(epRow,epCol);

        state.Board[startRow,startCol] = PIECE.BLACK_PAWN;
        state.Board[otherRow,otherCol] = PIECE.WHITE_PAWN;

        Move m = new Move(new Location(startRow,startCol), new Location(epRow,epCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, state.Board[startRow,startCol]);
        Assert.Equal(PIECE.NONE, state.Board[otherRow,otherCol]);
        Assert.Equal(PIECE.BLACK_PAWN, state.Board[epRow,epCol]);

    }


}
