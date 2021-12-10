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

public enum PLAYER
{
    WHITE,
    BLACK
};


public record class BoardState
{
    // Chess boards and arrays are layed out different so the rows in the board are backwards. The ROWS not the cells.
    // This is how a real board would look:
    //    Board[7]
    //    Board[6]
    //    Board[5]
    //    Board[4]
    //    Board[3]
    //    Board[2]
    //    Board[1]
    //    Board[0]

    public PIECE[,] Board = new PIECE[,]
    {
        {PIECE.WHITE_ROOK, PIECE.WHITE_KNIGHT, PIECE.WHITE_BISHOP, PIECE.WHITE_QUEEN, PIECE.WHITE_KING, PIECE.WHITE_BISHOP, PIECE.WHITE_KNIGHT, PIECE.WHITE_ROOK},
        {PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN, PIECE.WHITE_PAWN},
        {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
        {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
        {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
        {PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE, PIECE.NONE},
        {PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN, PIECE.BLACK_PAWN},
        {PIECE.BLACK_ROOK, PIECE.BLACK_KNIGHT, PIECE.BLACK_BISHOP, PIECE.BLACK_QUEEN, PIECE.BLACK_KING, PIECE.BLACK_BISHOP, PIECE.BLACK_KNIGHT, PIECE.BLACK_ROOK}
    };

    public PLAYER CurrentTurn = PLAYER.WHITE;

    public bool WhiteCanKingCastle = true;
    public bool WhiteCanQueenCastle = true;
    public bool BlackCanKingCastle = true;
    public bool BlackCanQueenCastle = true;

    public Location? EnPassantSquare = null;

    public int HalfMovesSinceLastCaptureOrPawnMove = 0;
    public int MoveNumber = 1;
}
