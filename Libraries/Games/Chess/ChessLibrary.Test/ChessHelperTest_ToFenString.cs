using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest
{
    [Fact]
    public void ToFENNotation_AllFieldsCorrectForStandardNewGameOfChessFromDefaultBoardState()
    {
        string target = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";


        BoardState state = new();

        string result = ChessHelper.ToFENNotation(state);


        Assert.Equal(target, result);
    }


    [Fact]
    public void ToFENNotation_PawnsAcrossBoardForAllPossibleBlanksOnSides()
    {
        //Not A Legal board
        string target = "p7/1p6/2p5/3p4/4P3/5P2/6P1/7P w KQkq - 0 1";


        BoardState state = new()
        {
            Board = new PIECE[,]
            {
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.WHITE_PAWN},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.WHITE_PAWN, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.WHITE_PAWN, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.WHITE_PAWN, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.BLACK_PAWN, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.NONE, PIECE.BLACK_PAWN, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.NONE, PIECE.BLACK_PAWN, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
                {PIECE.BLACK_PAWN, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
            }
        };

        string result = ChessHelper.ToFENNotation(state);


        Assert.Equal(target, result);
    }


    [Fact]
    public void ToFENNotation_WhenCurrentTurnIsBlackFENShowsLowerCaseBForCurrentPlayer()
    {
        string target = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1";

        BoardState state = new(){ CurrentTurn = PLAYER.BLACK};

        string result = ChessHelper.ToFENNotation(state);


        Assert.Equal(target, result);
    }

    [Theory]
    [InlineData(true, true, true, true, "KQkq")]
    [InlineData(true, true, true, false, "KQk")]
    [InlineData(true, true, false, true, "KQq")]
    [InlineData(true, true, false, false, "KQ")]
    [InlineData(true, false, true, true, "Kkq")]
    [InlineData(true, false, true, false, "Kk")]
    [InlineData(true, false, false, true, "Kq")]
    [InlineData(true, false, false, false, "K")]
    [InlineData(false, true, true, true, "Qkq")]
    [InlineData(false, true, true, false, "Qk")]
    [InlineData(false, true, false, true, "Qq")]
    [InlineData(false, true, false, false, "Q")]
    [InlineData(false, false, true, true, "kq")]
    [InlineData(false, false, true, false, "k")]
    [InlineData(false, false, false, true, "q")]
    [InlineData(false, false, false, false, "-")]
    public void ToFENNotation_CastleSectionOnlyShowsThoseThatArePossible_DashOtherwise(bool WK, bool WQ, bool BK, bool BQ, string expectedOutput)
    {
        string target = $"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w {expectedOutput} - 0 1";

        BoardState state = new()
        {
            WhiteCanKingCastle = WK,
            WhiteCanQueenCastle = WQ,
            BlackCanKingCastle = BK,
            BlackCanQueenCastle = BQ
        };

        string result = ChessHelper.ToFENNotation(state);

        Assert.Equal(target, result);
    }

    [Theory]
    [InlineData(1,0,"a2")]
    [InlineData(7,3,"d8")]
    [InlineData(4,7,"h5")]
    [InlineData(2,2,"c3")]
    [InlineData(5,1,"b6")]
    public void ToFENNotation_EnPassantIsAlgebraicNotaionOfLocationOfEnPassant(int LocRow, int LocColumn, string expectedOutput)
    {
        string target = $"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq {expectedOutput} 0 1";

        BoardState state = new()
        {
            EnPassantSquare = new Location(){ Row = LocRow, Column = LocColumn}
        };

        string result = ChessHelper.ToFENNotation(state);

        Assert.Equal(target, result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    public void ToFENNotation_MovesSinceLastCaptureIsFromState(int num)
    {
        string target = $"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - {num} 1";

        BoardState state = new()
        {
            HalfMovesSinceLastCaptureOrPawnMove = num
        };

        string result = ChessHelper.ToFENNotation(state);

        Assert.Equal(target, result);
    }

    [Theory]
    [InlineData(35)]
    [InlineData(4)]
    public void ToFENNotation_MoveNumberIsFromState(int num)
    {
        string target = $"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 {num}";

        BoardState state = new()
        {
            MoveNumber = num
        };

        string result = ChessHelper.ToFENNotation(state);

        Assert.Equal(target, result);
    }
}
