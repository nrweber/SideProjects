namespace ChessLibrary;

public enum PIECE
{
    NONE,
    BLACK_ROOK,
    BLACK_KNIGHT,
    BLACK_BISHOP,
    BLACK_QUEEN,
    BLACK_KING,
    BLACK_PAWN,
    WHITE_ROOK,
    WHITE_KNIGHT,
    WHITE_BISHOP,
    WHITE_QUEEN,
    WHITE_KING,
    WHITE_PAWN
};

public enum PROMOTION_PIECE
{
    NONE,
    QUEEN,
    BISHOP,
    KNIGHT,
    ROOK
};

public enum LOCATION_COLOR
{
    NO_PIECE,
    WHITE,
    BLACK
};


public enum PLAYER
{
    WHITE,
    BLACK
};

public record BoardState(
        PIECE[] Board,
        PLAYER CurrentTurn = PLAYER.WHITE, 
        bool WhiteCanKingCastle = true, 
        bool WhiteCanQueenCastle = true, 
        bool BlackCanKingCastle = true, 
        bool BlackCanQueenCastle = true, 
        Location? EnPassanteSquare = null,
        int HalfMovesSinceLastCaptureOrPawnMove = 0,
        int MoveNumber = 1
        )
{
    public BoardState() :  this((PIECE[])BoardState.ClassicBoardSetup.Clone())
    {

    }
    
    private static readonly PIECE[] ClassicBoardSetup = 
    {
        PIECE.WHITE_ROOK, PIECE.WHITE_KNIGHT, PIECE.WHITE_BISHOP, PIECE.WHITE_QUEEN, PIECE.WHITE_KING, PIECE.WHITE_BISHOP, PIECE.WHITE_KNIGHT, PIECE.WHITE_ROOK,
            PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE,
            PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN,
            PIECE.BLACK_ROOK, PIECE.BLACK_KNIGHT, PIECE.BLACK_BISHOP, PIECE.BLACK_QUEEN, PIECE.BLACK_KING, PIECE.BLACK_BISHOP, PIECE.BLACK_KNIGHT, PIECE.BLACK_ROOK
    };

    PIECE[] _board = Board;

    public PIECE[] Board
    {
        get
        {
            //If you know a better way of making an array immutable here
            // please let me know!!
            return (PIECE[])_board.Clone();
        }

        init
        {
            _board = value;
        }
    }


    public PIECE PieceAt(int row, int col)
    {
        if(row < 0 || row > 7 || col < 0 || col > 7)
            throw new ArgumentException($"Index out of bounds ({row},{col})");

        int location = (row*8)+col;
        return Board[location];
    }
}
