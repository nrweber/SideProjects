using System.Text;


namespace ChessLibrary;

public static class ChessHelper
{
    public static List<Move> PossibleMovesForLocation(BoardState state, Location loc)
    {
        //Generate all possible moves by the normal rules of the pieces
        // Remove any that result in the current color being in check.
        // Mate and Still Mate will have 0 moves returned.

        List<Move> moves = new();

        // Add all moves possible by basic move rules. Moves that put
        // the current players king into check will be removed later.
        if(state.CurrentTurn == PLAYER.WHITE)
        {
            switch(state.Board[loc.Row, loc.Column])
            {
                case PIECE.WHITE_PAWN:
                    AddWhitePawnMoves(loc, state, moves);
                    break;
                case PIECE.WHITE_ROOK:
                    AddColumnRowMoves(loc, state, moves, LOCATION_COLOR.WHITE);
                    break;
                case PIECE.WHITE_KNIGHT:
                    //Knight Moves
                    break;
                case PIECE.WHITE_BISHOP:
                    //Diagonal Moves
                    break;
                case PIECE.WHITE_QUEEN:
                    //ColumnRow Moves
                    //Diagonal Moves
                    break;
                case PIECE.WHITE_KING:
                    //King Moves
                    break;
            }
        }
        else
        {
            switch(state.Board[loc.Row, loc.Column])
            {
                case PIECE.BLACK_PAWN:
                    AddBlackPawnMoves(loc, state, moves);
                    break;
                case PIECE.BLACK_ROOK:
                    AddColumnRowMoves(loc, state, moves, LOCATION_COLOR.BLACK);
                    break;
                case PIECE.BLACK_KNIGHT:
                    //Knight Moves
                    break;
                case PIECE.BLACK_BISHOP:
                    //Diagonal Moves
                    break;
                case PIECE.BLACK_QUEEN:
                    //ColumnRow Moves
                    //Diagonal Moves
                    break;
                case PIECE.BLACK_KING:
                    //King Moves
                    break;
            }
        }

        return moves;
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

    private static LOCATION_COLOR LocationColor(PIECE p)
    {
        switch(p)
        {
            case PIECE.NONE:
                return LOCATION_COLOR.NO_PIECE;
            case PIECE.WHITE_PAWN:
            case PIECE.WHITE_ROOK:
            case PIECE.WHITE_KNIGHT:
            case PIECE.WHITE_BISHOP:
            case PIECE.WHITE_QUEEN:
            case PIECE.WHITE_KING:
                return LOCATION_COLOR.WHITE;
            default:
                return LOCATION_COLOR.BLACK;
        }
    }

    private static void AddWhitePawnMoves(Location loc, BoardState state, List<Move> moves)
    {
        if(state.Board[loc.Row+1, loc.Column] == PIECE.NONE)
        {
            //move foward 1 for row 6 which would be a promotion
            if(loc.Row == 6)
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column), PROMOTION_PIECE.ROOK));
            }
            //move foward 1 for rows 1-5
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column)));
            }
        }

        //move foward 2
        if(loc.Row == 1 && state.Board[loc.Row+1, loc.Column] == PIECE.NONE && state.Board[loc.Row+2, loc.Column] == PIECE.NONE)
        {
            moves.Add(new Move(loc, new Location(loc.Row+2, loc.Column)));
        }

        //attack up and left
        if(loc.Column != 0 && LOCATION_COLOR.BLACK == LocationColor(state.Board[loc.Row+1, loc.Column-1]))
        {
            //attack and promote if on row 6)
            if(loc.Row == 6)
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1), PROMOTION_PIECE.ROOK));
            }
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1)));
            }
        }

        //attack up and right
        if(loc.Column != 7 && LOCATION_COLOR.BLACK == LocationColor(state.Board[loc.Row+1, loc.Column+1]))
        {
            //attach and promote if on row 6)
            if(loc.Row == 6)
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1), PROMOTION_PIECE.ROOK));
            }
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1)));
            }
        }


        //EnPassante Left
        if(state.EnPassantSquare?.Row == loc.Row+1 && state.EnPassantSquare?.Column == loc.Column-1)
        {
            moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1)));
        }

        //EnPassante Right
        if(state.EnPassantSquare?.Row == loc.Row+1 && state.EnPassantSquare?.Column == loc.Column+1)
        {
            moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1)));
        }

    }


    private static void AddBlackPawnMoves(Location loc, BoardState state, List<Move> moves)
    {
        if(state.Board[loc.Row-1, loc.Column] == PIECE.NONE)
        {
            //move foward 1 for row 1 which would be a promotion
            if(loc.Row == 1)
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column), PROMOTION_PIECE.ROOK));
            }
            //move foward 1 for rows 1-5
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column)));
            }
        }

        if(loc.Row == 6 && state.Board[loc.Row-1, loc.Column] == PIECE.NONE && state.Board[loc.Row-2, loc.Column] == PIECE.NONE)
        {
            moves.Add(new Move(loc, new Location(loc.Row-2, loc.Column)));
        }

        //attack down and left
        if(loc.Column != 0 && LOCATION_COLOR.WHITE == LocationColor(state.Board[loc.Row-1, loc.Column-1]))
        {
            //attack and promote if on row 1
            if(loc.Row == 1)
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1), PROMOTION_PIECE.ROOK));
            }
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1)));
            }
        }

        //attack down and right
        if(loc.Column != 7 && LOCATION_COLOR.WHITE == LocationColor(state.Board[loc.Row-1, loc.Column+1]))
        {
            //attach and promote if on row 1
            if(loc.Row == 1)
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1), PROMOTION_PIECE.QUEEN));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1), PROMOTION_PIECE.BISHOP));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1), PROMOTION_PIECE.KNIGHT));
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1), PROMOTION_PIECE.ROOK));
            }
            else
            {
                moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1)));
            }
        }

        //EnPassante Left
        if(state.EnPassantSquare?.Row == loc.Row-1 && state.EnPassantSquare?.Column == loc.Column-1)
        {
            moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1)));
        }

        //EnPassante Right
        if(state.EnPassantSquare?.Row == loc.Row-1 && state.EnPassantSquare?.Column == loc.Column+1)
        {
            moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1)));
        }
    }

    private static void AddColumnRowMoves(Location loc, BoardState state, List<Move> moves, LOCATION_COLOR playerColor)
    {

        //up
        for(int r = loc.Row+1; r <= 7; r++)
        {
            int c = loc.Column;
            var locColor = LocationColor(state.Board[r,c]);
            if(locColor != playerColor)
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }

            if(locColor != LOCATION_COLOR.NO_PIECE)
                break;
        }

        //down
        for(int r = loc.Row-1; r >= 0; r--)
        {
            int c = loc.Column;
            var locColor = LocationColor(state.Board[r,c]);
            if(locColor != playerColor)
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }

            if(locColor != LOCATION_COLOR.NO_PIECE)
                break;
        }

        //left
        for(int c = loc.Column-1; c >= 0; c--)
        {
            int r = loc.Row;
            var locColor = LocationColor(state.Board[r,c]);
            if(locColor != playerColor)
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }

            if(locColor != LOCATION_COLOR.NO_PIECE)
                break;
        }

        //Right
        for(int c = loc.Column+1; c <= 7; c++)
        {
            int r = loc.Row;
            var locColor = LocationColor(state.Board[r,c]);
            if(locColor != playerColor)
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }

            if(locColor != LOCATION_COLOR.NO_PIECE)
                break;
        }

    }
}
