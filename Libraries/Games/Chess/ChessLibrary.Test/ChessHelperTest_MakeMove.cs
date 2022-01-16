using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_MakeMove
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


    [Fact]
    public void ValidMove_TrueAndNewStateReturned()
    {
        BoardState state = new();

        Move m = new Move(new Location(1,4), new Location(3,4));

        var (moveMade, newState) = ChessHelper.MakeMove(state,m);

        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, newState.PieceAt(1,4));
        Assert.Equal(PIECE.WHITE_PAWN, newState.PieceAt(3,4));
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

        Assert.Equal(PIECE.WHITE_PAWN, state.PieceAt(1,4));
        Assert.Equal(PIECE.NONE, state.PieceAt(3,4));
        Assert.Equal(PLAYER.WHITE, state.CurrentTurn);

        Assert.Equal(PIECE.NONE, newState.PieceAt(1,4));
        Assert.Equal(PIECE.WHITE_PAWN, newState.PieceAt(3,4));
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
        BoardState state = new()
        {
            HalfMovesSinceLastCaptureOrPawnMove = 5
        };

        {
            Move m = new Move(new Location(1,1), new Location(2,1));
            (var moveMade, state) = ChessHelper.MakeMove(state,m);
            Assert.True(moveMade);
            Assert.Equal(0, state.HalfMovesSinceLastCaptureOrPawnMove);
        }

        state = state with
        {
            HalfMovesSinceLastCaptureOrPawnMove = 5
        };

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
        var Board = new BoardState().Board;
        Board[BoardArrayLocation(1,0)] = PIECE.BLACK_PAWN;
        BoardState state = new(Board, HalfMovesSinceLastCaptureOrPawnMove: 5);

        //Rook takes pawn
        Move m = new Move(new Location(0,0), new Location(1,0));


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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );


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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );


        Move m = new Move(new Location(7,4), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.BlackCanKingCastle);
        Assert.False(state.BlackCanQueenCastle);
    }


    [Fact]
    public void WhiteKingCastlingFalseAfterRookMovesOffg1Square_QueenCastlingStaysTrue()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );


        Move m = new Move(new Location(0,7), new Location(1,7));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.WhiteCanKingCastle);
        Assert.True(state.WhiteCanQueenCastle);
    }

    [Fact]
    public void WhiteQueenCastlingFalseAfterRookMovesOffa1Square_KingCastlingStaysTrue()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );


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
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(startRow,startCol)] = movingPiece;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );


        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.WhiteCanKingCastle);
        Assert.True(state.WhiteCanQueenCastle);
    }

    [Fact]
    public void WhiteKingSideCastleMovesKingAndRook()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,7)] = PIECE.WHITE_ROOK;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );

        Move m = new Move(new Location(0,4), new Location(0,6));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.WHITE_KING, state.PieceAt(0,6));
        Assert.Equal(PIECE.WHITE_ROOK, state.PieceAt(0,5));
        Assert.Equal(PIECE.NONE, state.PieceAt(0,7));
    }

    [Fact]
    public void WhiteQueenSideCastleMovesKingAndRook()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(0,4)] = PIECE.WHITE_KING;
        Board[BoardArrayLocation(0,0)] = PIECE.WHITE_ROOK;
        BoardState state = new(
            Board,
            WhiteCanKingCastle: true,
            WhiteCanQueenCastle: true
        );

        Move m = new Move(new Location(0,4), new Location(0,2));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.WHITE_KING, state.PieceAt(0,2));
        Assert.Equal(PIECE.WHITE_ROOK, state.PieceAt(0,3));
        Assert.Equal(PIECE.NONE, state.PieceAt(0,0));
    }


    [Fact]
    public void BlackKingCastlingFalseAfterRookMovesOffg8Square_QueenCastlingStaysTrue()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );

        Move m = new Move(new Location(7,7), new Location(6,7));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.False(state.BlackCanKingCastle);
        Assert.True(state.BlackCanQueenCastle);
    }

    [Fact]
    public void BlackQueenCastlingFalseAfterRookMovesOffa8Square_KingCastlingStaysTrue()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );


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
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(startRow,startCol)] = movingPiece;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );


        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.True(state.BlackCanKingCastle);
        Assert.True(state.BlackCanQueenCastle);
    }

    [Fact]
    public void BlackKingSideCastleMovesKingAndRook()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,7)] = PIECE.BLACK_ROOK;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );


        Move m = new Move(new Location(7,4), new Location(7,6));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.BLACK_KING, state.PieceAt(7,6));
        Assert.Equal(PIECE.BLACK_ROOK, state.PieceAt(7,5));
        Assert.Equal(PIECE.NONE, state.PieceAt(7,7));
    }

    [Fact]
    public void BlackQueenSideCastleMovesKingAndRook()
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(7,4)] = PIECE.BLACK_KING;
        Board[BoardArrayLocation(7,0)] = PIECE.BLACK_ROOK;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            BlackCanKingCastle: true,
            BlackCanQueenCastle: true
        );


        Move m = new Move(new Location(7,4), new Location(7,2));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.BLACK_KING, state.PieceAt(7,2));
        Assert.Equal(PIECE.BLACK_ROOK, state.PieceAt(7,3));
        Assert.Equal(PIECE.NONE, state.PieceAt(7,0));
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
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startCol)] = movingPiece;
        BoardState state = new(
            Board,
            CurrentTurn: (movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK,
            EnPassanteSquare: null
        );


        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Null(state.EnPassanteSquare);
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
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startCol)] = movingPiece;
        BoardState state = new(
            Board,
            CurrentTurn: (movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK,
            EnPassanteSquare: null
        );


        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(new Location(enpRow, enpCol), state.EnPassanteSquare);
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
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startCol)] = movingPiece;
        BoardState state = new(
            Board,
            CurrentTurn: (movingPiece == PIECE.WHITE_PAWN) ? PLAYER.WHITE : PLAYER.BLACK,
            EnPassanteSquare: new Location(2,3)
        );


        Move m = new Move(new Location(startRow,startCol), new Location(endRow,endCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Null(state.EnPassanteSquare);
    }


    [Theory]
    [InlineData(4,3,  5,4, 4,4)]
    [InlineData(4,3,  5,2, 4,2)]
    public void EnPassante_White_AttackedPieceIsRemoved(int startRow, int startCol, int epRow, int epCol, int otherRow, int otherCol)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startCol)] = PIECE.WHITE_PAWN;
        Board[BoardArrayLocation(otherRow,otherCol)] = PIECE.BLACK_PAWN;

        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.WHITE,
            EnPassanteSquare: new Location(epRow,epCol)
        );

        Move m = new Move(new Location(startRow,startCol), new Location(epRow,epCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, state.PieceAt(startRow,startCol));
        Assert.Equal(PIECE.NONE, state.PieceAt(otherRow,otherCol));
        Assert.Equal(PIECE.WHITE_PAWN, state.PieceAt(epRow,epCol));

    }

    [Theory]
    [InlineData(3,5,  2,6, 3,6)]
    [InlineData(3,5,  2,4, 3,4)]
    public void EnPassante_Black_AttackedPieceIsRemoved(int startRow, int startCol, int epRow, int epCol, int otherRow, int otherCol)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(startRow,startCol)] = PIECE.BLACK_PAWN;
        Board[BoardArrayLocation(otherRow,otherCol)] = PIECE.WHITE_PAWN;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK,
            EnPassanteSquare: new Location(epRow,epCol)
        );


        Move m = new Move(new Location(startRow,startCol), new Location(epRow,epCol));
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.True(moveMade);
        Assert.Equal(PIECE.NONE, state.PieceAt(startRow,startCol));
        Assert.Equal(PIECE.NONE, state.PieceAt(otherRow,otherCol));
        Assert.Equal(PIECE.BLACK_PAWN, state.PieceAt(epRow,epCol));

    }

    [Theory]
    [InlineData(1, PROMOTION_PIECE.QUEEN, PIECE.WHITE_QUEEN)]
    [InlineData(3, PROMOTION_PIECE.ROOK, PIECE.WHITE_ROOK)]
    [InlineData(5, PROMOTION_PIECE.KNIGHT, PIECE.WHITE_KNIGHT)]
    [InlineData(7, PROMOTION_PIECE.BISHOP, PIECE.WHITE_BISHOP)]
    public void WhitePromotion_PawnRemovedAndNewPiecePlaces(int col, PROMOTION_PIECE promotoTo, PIECE endPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(6,col)] = PIECE.WHITE_PAWN;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.WHITE
        );


        Move m = new Move(new Location(6,col), new Location(7,col), promotoTo);
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.Equal(PIECE.NONE, state.PieceAt(6,col));
        Assert.Equal(endPiece, state.PieceAt(7,col));
        Assert.True(moveMade);
    }

    [Theory]
    [InlineData(1, PROMOTION_PIECE.QUEEN, PIECE.BLACK_QUEEN)]
    [InlineData(3, PROMOTION_PIECE.ROOK, PIECE.BLACK_ROOK)]
    [InlineData(5, PROMOTION_PIECE.KNIGHT, PIECE.BLACK_KNIGHT)]
    [InlineData(7, PROMOTION_PIECE.BISHOP, PIECE.BLACK_BISHOP)]
    public void BlackPromotion_PawnRemovedAndNewPiecePlaces(int col, PROMOTION_PIECE promotoTo, PIECE endPiece)
    {
        var Board = BlankBoard();
        Board[BoardArrayLocation(1,col)] = PIECE.BLACK_PAWN;
        BoardState state = new(
            Board,
            CurrentTurn: PLAYER.BLACK
        );


        Move m = new Move(new Location(1,col), new Location(0,col), promotoTo);
        (var moveMade, state) = ChessHelper.MakeMove(state,m);
        Assert.Equal(PIECE.NONE, state.PieceAt(1,col));
        Assert.Equal(endPiece, state.PieceAt(0,col));
        Assert.True(moveMade);
    }
}
