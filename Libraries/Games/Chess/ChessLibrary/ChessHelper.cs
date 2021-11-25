using System.Text;


namespace ChessLibrary;

public static class ChessHelper
{

    public static List<Move> PossibleMoves(BoardState state, Location loc)
    {
        //Generate all possible moves by the normal rules of the pieces
        // Remove any that result in the current color being in check.
        // Mate and Still Mate will have 0 moves returned.

        //Make sure you account for pawn promotion of pawns


        return new List<Move>();
    }

    public static bool MakeMove(BoardState state, Move newMove)
    {
        return false;
    }

    public static bool WhiteIsInCheck(BoardState state)
    {
        //Find the White King
        var loc = FindPiece(state, PIECE.WHITE_KING);
        if(loc.row == -1)
            throw new Exception("Could not find the white king piece");

        if(
                PawnAttackingKing(state, PLAYER.WHITE, loc.row, loc.col) ||
                KnightAttackingKing(state, PLAYER.WHITE, loc.row, loc.col) ||
                DiagonalAttackinOnKing(state, PLAYER.WHITE, loc.row, loc.col) ||
                ColumnRowAttackOnKing(state, PLAYER.WHITE, loc.row, loc.col)
          )
        {
            return true;
        }

        return false;
    }

    public static bool BlackIsInCheck(BoardState state)
    {
        //Find the Black King
        var loc = FindPiece(state, PIECE.BLACK_KING);
        if(loc.row == -1)
            throw new Exception("Could not find the white king piece");

        if(
                PawnAttackingKing(state, PLAYER.BLACK, loc.row, loc.col) ||
                KnightAttackingKing(state, PLAYER.BLACK, loc.row, loc.col) ||
                DiagonalAttackinOnKing(state, PLAYER.BLACK, loc.row, loc.col) ||
                ColumnRowAttackOnKing(state, PLAYER.BLACK, loc.row, loc.col)
          )
        {
            return true;
        }

        return false;
    }

    public static string ToFENNotation(BoardState state)
    {
        StringBuilder output = new StringBuilder();
        output.Append(FENBoardString(state));
        output.Append(" ");
        output.Append((state.CurrentTurn == PLAYER.WHITE) ? "w" : "b");
        output.Append(" ");
        if(state.WhiteCanKingCastle)
            output.Append("K");
        if(state.WhiteCanQueenCastle)
            output.Append("Q");
        if(state.BlackCanKingCastle)
            output.Append("k");
        if(state.BlackCanQueenCastle)
            output.Append("q");
        if(state.WhiteCanKingCastle == false && state.WhiteCanQueenCastle == false && state.BlackCanKingCastle == false && state.BlackCanQueenCastle == false)
            output.Append("-");
        output.Append(" ");
        if(state.EnPassantSquare is null)
            output.Append("-");
        else
            output.Append(state.EnPassantSquare?.Algebraic);
        output.Append(" ");
        output.Append(state.HalfMovesSinceLastCaptureOrPawnMove);
        output.Append(" ");
        output.Append(state.MoveNumber);
        return output.ToString();
    }

    private static string FENBoardString(BoardState state)
    {
        StringBuilder output = new StringBuilder();

        for(int i = 7; i >= 0; i--)
        {
            int blankCount = 0;
            for(int j = 0; j < 8; j++)
            {
                var piece = state.Board[i,j];
                if(piece == PIECE.NONE)
                {
                    blankCount += 1;
                }
                else
                {
                    if(blankCount != 0)
                    {
                        output.Append(blankCount);
                        blankCount = 0;
                    }

                    output.Append(PieceToFENString(piece));
                }
            }

            if(blankCount != 0)
                output.Append(blankCount);
            if(i != 0)
                output.Append("/");
        }
        return output.ToString();
    }

    private static string PieceToFENString(PIECE p)
    {
        switch(p)
        {
            case PIECE.BLACK_ROOK:
                return "r";
            case PIECE.BLACK_KNIGHT:
                return "n";
            case PIECE.BLACK_BISHOP:
                return "b";
            case PIECE.BLACK_QUEEN:
                return "q";
            case PIECE.BLACK_KING:
                return "k";
            case PIECE.BLACK_PAWN:
                return "p";
            case PIECE.WHITE_ROOK:
                return "R";
            case PIECE.WHITE_KNIGHT:
                return "N";
            case PIECE.WHITE_BISHOP:
                return "B";
            case PIECE.WHITE_QUEEN:
                return "Q";
            case PIECE.WHITE_KING:
                return "K";
            case PIECE.WHITE_PAWN:
                return "P";
        }
        return "";
    }

    private static (int row, int col) FindPiece(BoardState state, PIECE pieceToLocate)
    {
        for(int row = 0; row <= 7; row++)
        {
            for(int col = 0; col <= 7; col++)
            {
                if(state.Board[row,col] == pieceToLocate)
                {
                    return (row, col);
                }
            }
        }

        return (-1, -1);
    }

    private static bool PawnAttackingKing(BoardState state, PLAYER player, int kingRow, int kingCol)
    {
        if(player == PLAYER.WHITE)
        {
            if(kingRow != 7 && kingCol != 0 && state.Board[kingRow+1, kingCol-1] == PIECE.BLACK_PAWN)
            {
                return true;
            }
            if(kingRow != 7 && kingCol != 7 && state.Board[kingRow+1, kingCol+1] == PIECE.BLACK_PAWN)
            {
                return true;
            }
        }
        else if(player == PLAYER.BLACK)
        {
            if(kingRow != 0 && kingCol != 0 && state.Board[kingRow-1, kingCol-1] == PIECE.WHITE_PAWN)
            {
                return true;
            }
            if(kingRow != 0 && kingCol != 7 && state.Board[kingRow-1, kingCol+1] == PIECE.WHITE_PAWN)
            {
                return true;
            }
        }
        return false;
    }

    private static bool KnightAttackingKing(BoardState state, PLAYER player, int kingRow, int kingCol)
    {
        PIECE otherColorKnight = (player == PLAYER.WHITE) ? PIECE.BLACK_KNIGHT : PIECE.WHITE_KNIGHT;

        List<(int rowDiff, int colDiff)> kightMoveDiffs = new(){(2,-1), (2,1), (1,2), (-1,2), (-2,1), (-2,-1), (-1,-2), (1,-2)};
        foreach(var m in kightMoveDiffs)
        {
            int row = kingRow + m.rowDiff;
            int col = kingCol + m.colDiff;

            if( row >= 0 && row <= 7 && col >= 0 && col <= 7 && state.Board[row, col] == otherColorKnight)
            {
                return true;
            }
        }
        return false;
    }

    private static bool DiagonalAttackinOnKing(BoardState state, PLAYER player, int kingRow, int kingCol)
    {
        PIECE otherColorBishop = (player == PLAYER.WHITE) ? PIECE.BLACK_BISHOP : PIECE.WHITE_BISHOP;
        PIECE otherColorQueen = (player == PLAYER.WHITE) ? PIECE.BLACK_QUEEN : PIECE.WHITE_QUEEN;

        List<(int rowDiff, int colDiff)> bishopDiffs = new(){ (1,-1), (1,1), (-1, 1), (-1,-1)};
        foreach(var diff in bishopDiffs)
        {
            for(int i = 1; i <= 8; i++)
            {
                int checkRow = kingRow + (i*diff.rowDiff);
                int checkCol = kingCol + (i*diff.colDiff);

                //stop at the edge of the board
                if(checkRow < 0 || checkRow > 7 || checkCol < 0 || checkCol > 7)
                {
                    break;
                }

                if(state.Board[checkRow, checkCol] == otherColorBishop || state.Board[checkRow, checkCol] == otherColorQueen)
                {
                    return true;
                }

                //If we hit another piece besides a rook or queen, break out of the loop.
                if(state.Board[checkRow, checkCol] != PIECE.NONE)
                {
                    break;
                }
            }
        }


        return false;
    }

    private static bool ColumnRowAttackOnKing(BoardState state, PLAYER player, int kingRow, int kingCol)
    {
        PIECE otherColorRook = (player == PLAYER.WHITE) ? PIECE.BLACK_ROOK : PIECE.WHITE_ROOK;
        PIECE otherColorQueen = (player == PLAYER.WHITE) ? PIECE.BLACK_QUEEN : PIECE.WHITE_QUEEN;

        List<(int rowDiff, int colDiff)> rookDiffs = new(){ (0,-1), (0,1), (-1, 0), (1,0)};
        foreach(var diff in rookDiffs)
        {
            for(int i = 1; i <= 8; i++)
            {
                int checkRow = kingRow + (i*diff.rowDiff);
                int checkCol = kingCol + (i*diff.colDiff);

                //stop at the edge of the board
                if(checkRow < 0 || checkRow > 7 || checkCol < 0 || checkCol > 7)
                {
                    break;
                }

                if(state.Board[checkRow, checkCol] == otherColorRook || state.Board[checkRow, checkCol] == otherColorQueen)
                {
                    return true;
                }

                //If we hit another piece besides a rook or queen, break out of the loop.
                if(state.Board[checkRow, checkCol] != PIECE.NONE)
                {
                    break;
                }
            }
        }
        return false;
    }

}