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
