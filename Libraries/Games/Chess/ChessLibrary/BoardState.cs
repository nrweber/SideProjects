using System.Text;

namespace ChessLibrary;

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
    static readonly string[] PossibleCastleStrings = new  string[]{"KQkq", "KQk", "KQq", "KQ", "Kkq", "Kq", "Kk", "K", "Qkq", "Qq", "Qk", "Q", "kq", "k", "q", "-"};
    static readonly char[] ValidColumnLetters = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'};

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


    public BoardState() :  this((PIECE[])BoardState.ClassicBoardSetup.Clone())
    {

    }


    public PIECE PieceAt(int row, int col)
    {
        if(row < 0 || row > 7 || col < 0 || col > 7)
            throw new ArgumentException($"Index out of bounds ({row},{col})");

        int location = (row*8)+col;
        return Board[location];
    }



    //This is the starting position for a clasic chess game.
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



    public  string ToFENNotation()
    {
        StringBuilder output = new StringBuilder();
        output.Append(FENBoardString());
        output.Append(" ");
        output.Append((CurrentTurn == PLAYER.WHITE) ? "w" : "b");
        output.Append(" ");
        if(WhiteCanKingCastle)
            output.Append("K");
        if(WhiteCanQueenCastle)
            output.Append("Q");
        if(BlackCanKingCastle)
            output.Append("k");
        if(BlackCanQueenCastle)
            output.Append("q");
        if(WhiteCanKingCastle == false && WhiteCanQueenCastle == false && BlackCanKingCastle == false && BlackCanQueenCastle == false)
            output.Append("-");
        output.Append(" ");
        if(EnPassanteSquare is null)
            output.Append("-");
        else
            output.Append(EnPassanteSquare?.Algebraic);
        output.Append(" ");
        output.Append(HalfMovesSinceLastCaptureOrPawnMove);
        output.Append(" ");
        output.Append(MoveNumber);
        return output.ToString();
    }

    private string FENBoardString()
    {
        StringBuilder output = new StringBuilder();

        for(int i = 7; i >= 0; i--)
        {
            int blankCount = 0;
            for(int j = 0; j < 8; j++)
            {
                var piece = PieceAt(i,j);
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

    public  List<Move> PossibleMoves()
    {
        List<Move> moves = new();
        for(int r = 0; r <= 7; r++)
        {
            for(int c = 0; c <= 7; c++)
            {
                var loc = new Location(r,c);
                moves.AddRange(PossibleMovesForLocation(loc));
            }
        }
        return moves;
    }

    public List<Move> PossibleMovesForLocation(Location loc)
    {
        //Return a blank list if the location is invalid
        if(loc.Row < 0 || loc.Row > 7 || loc.Column < 0 || loc.Column > 7 )
            return new List<Move>();


        List<Move> moves = new();
        // Add all moves possible by basic move rules. Moves that put
        // the current players king into check will be removed later.
        if(CurrentTurn == PLAYER.WHITE)
        {
            switch(PieceAt(loc.Row, loc.Column))
            {
                case PIECE.WHITE_PAWN:
                    AddWhitePawnMoves(loc, moves);
                    break;
                case PIECE.WHITE_ROOK:
                    AddColumnRowMoves(loc, moves, LOCATION_COLOR.WHITE);
                    break;
                case PIECE.WHITE_KNIGHT:
                    AddKnightMoves(loc, moves, LOCATION_COLOR.WHITE);
                    break;
                case PIECE.WHITE_BISHOP:
                    AddDiagonalMoves(loc, moves, LOCATION_COLOR.WHITE);
                    break;
                case PIECE.WHITE_QUEEN:
                    AddColumnRowMoves(loc, moves, LOCATION_COLOR.WHITE);
                    AddDiagonalMoves(loc, moves, LOCATION_COLOR.WHITE);
                    break;
                case PIECE.WHITE_KING:
                    AddKingMoves(loc, moves, LOCATION_COLOR.WHITE);
                    break;
            }
        }
        else
        {
            switch(PieceAt(loc.Row, loc.Column))
            {
                case PIECE.BLACK_PAWN:
                    AddBlackPawnMoves(loc, moves);
                    break;
                case PIECE.BLACK_ROOK:
                    AddColumnRowMoves(loc, moves, LOCATION_COLOR.BLACK);
                    break;
                case PIECE.BLACK_KNIGHT:
                    AddKnightMoves(loc, moves, LOCATION_COLOR.BLACK);
                    break;
                case PIECE.BLACK_BISHOP:
                    AddDiagonalMoves(loc, moves, LOCATION_COLOR.BLACK);
                    break;
                case PIECE.BLACK_QUEEN:
                    AddColumnRowMoves(loc, moves, LOCATION_COLOR.BLACK);
                    AddDiagonalMoves(loc, moves, LOCATION_COLOR.BLACK);
                    break;
                case PIECE.BLACK_KING:
                    AddKingMoves(loc, moves, LOCATION_COLOR.BLACK);
                    break;
            }
        }

        List<Move> goodMoves = new();
        foreach(var move in moves)
        {
            PIECE[] newBoard = Board;
            newBoard[(move.End.Row*8)+move.End.Column] = PieceAt(move.Start.Row, move.Start.Column);
            newBoard[(move.Start.Row*8)+move.Start.Column] = PIECE.NONE;
            BoardState stateCopy = this with {Board = newBoard};

            if(CurrentTurn == PLAYER.WHITE && false == stateCopy.WhiteIsInCheck())
            {
                goodMoves.Add(move);
            }
            if(CurrentTurn == PLAYER.BLACK && false == stateCopy.BlackIsInCheck())
            {
                goodMoves.Add(move);
            }
        }

        return goodMoves;
    }

    private static int BoardArrayLocation(int row, int col)
    {
        return (row*8)+col;
    }

    public (bool moveMade, BoardState newState) MakeMove(Move newMove)
    {
        ////////////////////////////////////////////////////
        // Copy the state
        PLAYER playerMakingMove = CurrentTurn;

        ////////////////////////////////////////////////////
        // Check if it is a valid move
        var validMoves = PossibleMoves();
        if(false == validMoves.Contains(newMove))
        {
            return (false, this);
        }

        ////////////////////////////////////////////////////
        // Useful info from move
        Location startLoc = newMove.Start;
        PIECE pieceAtStartLocation = PieceAt(startLoc.Row, startLoc.Column);
        Location endLoc = newMove.End;
        PIECE pieceAtEndLocation = PieceAt(endLoc.Row, endLoc.Column);

        ////////////////////////////////////////////////////
        // Update Board
        var newBoard = Board;

        newBoard[BoardArrayLocation(newMove.End.Row, newMove.End.Column)] = newBoard[BoardArrayLocation(newMove.Start.Row, newMove.Start.Column)];
        newBoard[BoardArrayLocation(newMove.Start.Row, newMove.Start.Column)] = PIECE.NONE;

        //Promotion
        if(newMove.promotion != PROMOTION_PIECE.NONE)
        {
            newBoard[BoardArrayLocation(newMove.End.Row, newMove.End.Column)] = PromotionToPiece(newMove.promotion, playerMakingMove);
        }

        //Castle - Move Rook
        if(pieceAtStartLocation == PIECE.WHITE_KING && startLoc == new Location(0,4) && endLoc == new Location(0,6))
        {
            newBoard[BoardArrayLocation(0,5)] = PIECE.WHITE_ROOK;
            newBoard[BoardArrayLocation(0,7)] = PIECE.NONE;
        }
        if(pieceAtStartLocation == PIECE.WHITE_KING && startLoc == new Location(0,4) && endLoc == new Location(0,2))
        {
            newBoard[BoardArrayLocation(0,3)] = PIECE.WHITE_ROOK;
            newBoard[BoardArrayLocation(0,0)] = PIECE.NONE;
        }
        if(pieceAtStartLocation == PIECE.BLACK_KING && startLoc == new Location(7,4) && endLoc == new Location(7,6))
        {
            newBoard[BoardArrayLocation(7,5)] = PIECE.BLACK_ROOK;
            newBoard[BoardArrayLocation(7,7)] = PIECE.NONE;
        }
        if(pieceAtStartLocation == PIECE.BLACK_KING && startLoc == new Location(7,4) && endLoc == new Location(7,2))
        {
            newBoard[BoardArrayLocation(7,3)] = PIECE.BLACK_ROOK;
            newBoard[BoardArrayLocation(7,0)] = PIECE.NONE;
        }


        //En Pasante: remove pawn
        if(pieceAtStartLocation == PIECE.WHITE_PAWN && endLoc == EnPassanteSquare && EnPassanteSquare != null)
        {
            Location a = (Location)EnPassanteSquare; // TODO: this gets around a strange error that Row and Column are not accessable. Need to look into this more.
            newBoard[BoardArrayLocation(a.Row-1, a.Column)]= PIECE.NONE;
        }
        if(pieceAtStartLocation == PIECE.BLACK_PAWN && endLoc == EnPassanteSquare && EnPassanteSquare != null)
        {
            Location a = (Location)EnPassanteSquare; // TODO: this gets around a strange error that Row and Column are not accessable. Need to look into this more.
            newBoard[BoardArrayLocation(a.Row+1, a.Column)]= PIECE.NONE;
        }


        ////////////////////////////////////////////////////
        // Flip turn
        var newCurrentTurn = (CurrentTurn == PLAYER.WHITE) ? PLAYER.BLACK : PLAYER.WHITE;


        ////////////////////////////////////////////////////
        // Check casteling possibilities
        bool newWhiteCanKingCastle = WhiteCanKingCastle;
        bool newWhiteCanQueenCastle = WhiteCanQueenCastle;
        bool newBlackCanKingCastle = BlackCanKingCastle;
        bool newBlackCanQueenCastle = BlackCanQueenCastle;

        if(pieceAtStartLocation == PIECE.WHITE_KING)
        {
            newWhiteCanKingCastle = false;
            newWhiteCanQueenCastle = false;
        }

        if(pieceAtStartLocation == PIECE.BLACK_KING)
        {
            newBlackCanKingCastle = false;
            newBlackCanQueenCastle = false;
        }

        //White Rook Moves
        if(pieceAtStartLocation == PIECE.WHITE_ROOK && startLoc.Row == 0 && startLoc.Column == 7)
            newWhiteCanKingCastle = false;
        if(pieceAtStartLocation == PIECE.WHITE_ROOK && startLoc.Row == 0 && startLoc.Column == 0)
            newWhiteCanQueenCastle = false;

        if(pieceAtStartLocation == PIECE.BLACK_ROOK && startLoc.Row == 7 && startLoc.Column == 7)
            newBlackCanKingCastle = false;
        if(pieceAtStartLocation == PIECE.BLACK_ROOK && startLoc.Row == 7 && startLoc.Column == 0)
            newBlackCanQueenCastle = false;


        ////////////////////////////////////////////////////
        // check en passante
        Location? newEnPassanteSquare = null;
        if(pieceAtStartLocation == PIECE.WHITE_PAWN && startLoc.Row == 1 && endLoc.Row == 3)
        {
            newEnPassanteSquare = new Location(2, startLoc.Column);
        }
        if(pieceAtStartLocation == PIECE.BLACK_PAWN && startLoc.Row == 6 && endLoc.Row == 4)
        {
            newEnPassanteSquare = new Location(5, startLoc.Column);
        }

        ////////////////////////////////////////////////////
        // update half move counter
        int newHalfMovesSinceLastCaptureOrPawnMove = HalfMovesSinceLastCaptureOrPawnMove;
        if( PIECE.WHITE_PAWN == pieceAtStartLocation || PIECE.BLACK_PAWN == pieceAtStartLocation || PIECE.NONE != pieceAtEndLocation)
            newHalfMovesSinceLastCaptureOrPawnMove = 0;
        else
            newHalfMovesSinceLastCaptureOrPawnMove += 1;

        ////////////////////////////////////////////////////
        // up trun if was black's turn
        int newMoveNumber = MoveNumber;
        if(CurrentTurn == PLAYER.BLACK)
            newMoveNumber += 1;

        var newState = new BoardState
        (
            newBoard,
            newCurrentTurn,
            newWhiteCanKingCastle,
            newWhiteCanQueenCastle,
            newBlackCanKingCastle,
            newBlackCanQueenCastle,
            newEnPassanteSquare,
            newHalfMovesSinceLastCaptureOrPawnMove,
            newMoveNumber
        );


        return (true, newState);
    }

    public bool WhiteIsInCheck()
    {
        //Find the White King
        var loc = FindPiece(PIECE.WHITE_KING);

        // White is not in check if ther is no king.
        // These allows the BoardState to be used for other things then a game
        // such as setting up a situation to explain tactics or something like that
        if(loc.row == -1)
            return false;

        if(
                PawnAttackingKing(PLAYER.WHITE, loc.row, loc.col) ||
                KnightAttackingKing(PLAYER.WHITE, loc.row, loc.col) ||
                DiagonalAttackinOnKing(PLAYER.WHITE, loc.row, loc.col) ||
                ColumnRowAttackOnKing(PLAYER.WHITE, loc.row, loc.col) ||
                KingAttackOnKing(PLAYER.WHITE, loc.row, loc.col)
          )
        {
            return true;
        }

        return false;
    }

    public bool BlackIsInCheck()
    {
        //Find the Black King
        var loc = FindPiece(PIECE.BLACK_KING);

        // Black is not in check if ther is no king.
        // These allows the BoardState to be used for other things then a game
        // such as setting up a situation to explain tactics or something like that
        if(loc.row == -1)
            return false;

        if(
                PawnAttackingKing(PLAYER.BLACK, loc.row, loc.col) ||
                KnightAttackingKing(PLAYER.BLACK, loc.row, loc.col) ||
                DiagonalAttackinOnKing(PLAYER.BLACK, loc.row, loc.col) ||
                ColumnRowAttackOnKing(PLAYER.BLACK, loc.row, loc.col) ||
                KingAttackOnKing(PLAYER.BLACK, loc.row, loc.col)
          )
        {
            return true;
        }

        return false;
    }


    public static (bool, BoardState) ToBoardState(String Fen)
    {
        var parts = Fen.Split(" ");

        if(parts.Length != 6)
        {
            return (false, new BoardState());
        }

        PIECE[] Board = new PIECE[64];
        //Board in FEN and in the array are different order
        // This number will need to be translated to the correct
        // row and column
        int RelativeBoardIndex = 0;
        int StringIndex = 0;
        int RowCheck = 0;
        while(RelativeBoardIndex < 64 && StringIndex < parts[0].Length)
        {
            if(parts[0][StringIndex] == '/')
            {
                if(RelativeBoardIndex%8 == 0)
                {
                    StringIndex += 1;
                    RowCheck += 1;
                    continue;
                }
                else
                {
                    return (false, new BoardState());
                }

            }

            if(RelativeBoardIndex%8 == 0 && RelativeBoardIndex/8 != RowCheck)
            {
                return (false, new BoardState());
            }


            PIECE p = FENCharToPiece(parts[0][StringIndex]);
            if(p != PIECE.NONE)
            {
                int row = 7-(RelativeBoardIndex/8);
                int col = RelativeBoardIndex%8;
                int BoardIndex = (row*8)+col;
                Board[BoardIndex] = p;

                StringIndex += 1;
                RelativeBoardIndex += 1;
                continue;
            }

            int empties = 0;
            if(Int32.TryParse(parts[0][StringIndex].ToString(), out empties))
            {
                if(RelativeBoardIndex + empties > 64)
                    return (false, new BoardState());

                for(int i = 0; i < empties; i++)
                {
                    int row = 7-(RelativeBoardIndex/8);
                    int col = RelativeBoardIndex%8;
                    int BoardIndex = (row*8)+col;
                    Board[BoardIndex] = PIECE.NONE;
                    RelativeBoardIndex += 1;
                }

                StringIndex += 1;
                continue;
            }


            //If we got here the character is invalid
            return (false, new BoardState());
        }

        if(StringIndex < parts[0].Length || RelativeBoardIndex < 64)
            return (false, new BoardState());



        // Current Turn
        if(parts[1] != "w" && parts[1] != "b" )
            return (false, new BoardState());

        PLAYER CurrentTurn = (parts[1] == "w") ? PLAYER.WHITE : PLAYER.BLACK;

        // Castle possiblities
        if(PossibleCastleStrings.Contains(parts[2]) == false)
            return (false, new BoardState());
        var castleSplit = parts[2].ToList();
        bool WhiteCanKingCastle = castleSplit.Contains('K');
        bool WhiteCanQueenCastle = castleSplit.Contains('Q');
        bool BlackCanKingCastle = castleSplit.Contains('k');
        bool BlackCanQueenCastle = castleSplit.Contains('q');

        //EnPassange Square
        // Dash is ok to leave as default
        Location? EnPassanteSquare = null;
        if(parts[3] != "-") 
        {
            // Expect two characters
            // first must be a-z and second must be 3 or 6
            if( parts[3].Length != 2
                || (parts[3][1] != '3' && parts[3][1] != '6') 
                || false == ValidColumnLetters.Contains(parts[3][0]))
            {
                return (false, new BoardState());
            }
            else 
            {
                int col = (parts[3][0]-'a');
                int row = Int32.Parse(parts[3][1].ToString())-1;

                EnPassanteSquare = new Location(row, col);
            }
        }

        //Half moves since pawn move or capture
        int halfMoves = 0;
        // must be positive number
        bool halfMovesValid = Int32.TryParse(parts[4], out halfMoves);
        if(halfMovesValid == false || halfMoves < 0)
            return (false, new BoardState());
        int HalfMovesSinceLastCaptureOrPawnMove = halfMoves;

        //Move Number
        int moveNumber = 0;
        bool moveNumberValid = Int32.TryParse(parts[5], out moveNumber);
        //must be postive and greater than zero
        if(moveNumberValid == false || moveNumber <= 0)
            return (false, new BoardState());
        int MoveNumber = moveNumber;

        var newState = new BoardState
        (
            Board,
            CurrentTurn,
            WhiteCanKingCastle,
            WhiteCanQueenCastle,
            BlackCanKingCastle,
            BlackCanQueenCastle,
            EnPassanteSquare,
            HalfMovesSinceLastCaptureOrPawnMove,
            MoveNumber
        );
        return (true, newState);
    }


    private static PIECE PromotionToPiece(PROMOTION_PIECE promotionPiece, PLAYER player)
    {
        switch(promotionPiece)
        {
            case PROMOTION_PIECE.QUEEN:
                return (player == PLAYER.WHITE) ? PIECE.WHITE_QUEEN : PIECE.BLACK_QUEEN;
            case PROMOTION_PIECE.KNIGHT:
                return (player == PLAYER.WHITE) ? PIECE.WHITE_KNIGHT : PIECE.BLACK_KNIGHT;
            case PROMOTION_PIECE.ROOK:
                return (player == PLAYER.WHITE) ? PIECE.WHITE_ROOK : PIECE.BLACK_ROOK;
            case PROMOTION_PIECE.BISHOP:
                return (player == PLAYER.WHITE) ? PIECE.WHITE_BISHOP : PIECE.BLACK_BISHOP;
        }
        return PIECE.NONE;
    }

    private static PIECE FENCharToPiece(Char c)
    {
        switch(c)
        {
            case 'r':
                return PIECE.BLACK_ROOK;
            case 'n':
                return PIECE.BLACK_KNIGHT;
            case 'b':
                return PIECE.BLACK_BISHOP;
            case 'q':
                return PIECE.BLACK_QUEEN;
            case 'k':
                return PIECE.BLACK_KING;
            case 'p':
                return PIECE.BLACK_PAWN;
            case 'R':
                return PIECE.WHITE_ROOK;
            case 'N':
                return PIECE.WHITE_KNIGHT;
            case 'B':
                return PIECE.WHITE_BISHOP;
            case 'Q':
                return PIECE.WHITE_QUEEN;
            case 'K':
                return PIECE.WHITE_KING;
            case 'P':
                return PIECE.WHITE_PAWN;
        }
        return PIECE.NONE;
    }

    private (int row, int col) FindPiece(PIECE pieceToLocate)
    {
        for(int row = 0; row <= 7; row++)
        {
            for(int col = 0; col <= 7; col++)
            {
                if(PieceAt(row,col) == pieceToLocate)
                {
                    return (row, col);
                }
            }
        }

        return (-1, -1);
    }

    private bool PawnAttackingKing(PLAYER player, int kingRow, int kingCol)
    {
        if(player == PLAYER.WHITE)
        {
            if(kingRow != 7 && kingCol != 0 && PieceAt(kingRow+1, kingCol-1) == PIECE.BLACK_PAWN)
            {
                return true;
            }
            if(kingRow != 7 && kingCol != 7 && PieceAt(kingRow+1, kingCol+1) == PIECE.BLACK_PAWN)
            {
                return true;
            }
        }
        else if(player == PLAYER.BLACK)
        {
            if(kingRow != 0 && kingCol != 0 && PieceAt(kingRow-1, kingCol-1) == PIECE.WHITE_PAWN)
            {
                return true;
            }
            if(kingRow != 0 && kingCol != 7 && PieceAt(kingRow-1, kingCol+1) == PIECE.WHITE_PAWN)
            {
                return true;
            }
        }
        return false;
    }

    private bool KnightAttackingKing(PLAYER player, int kingRow, int kingCol)
    {
        PIECE otherColorKnight = (player == PLAYER.WHITE) ? PIECE.BLACK_KNIGHT : PIECE.WHITE_KNIGHT;

        List<(int rowDiff, int colDiff)> kightMoveDiffs = new(){(2,-1), (2,1), (1,2), (-1,2), (-2,1), (-2,-1), (-1,-2), (1,-2)};
        foreach(var m in kightMoveDiffs)
        {
            int row = kingRow + m.rowDiff;
            int col = kingCol + m.colDiff;

            if( row >= 0 && row <= 7 && col >= 0 && col <= 7 && PieceAt(row, col) == otherColorKnight)
            {
                return true;
            }
        }
        return false;
    }

    private bool DiagonalAttackinOnKing(PLAYER player, int kingRow, int kingCol)
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

                if(PieceAt(checkRow, checkCol) == otherColorBishop || PieceAt(checkRow, checkCol) == otherColorQueen)
                {
                    return true;
                }

                //If we hit another piece besides a rook or queen, break out of the loop.
                if(PieceAt(checkRow, checkCol) != PIECE.NONE)
                {
                    break;
                }
            }
        }


        return false;
    }

    private bool ColumnRowAttackOnKing(PLAYER player, int kingRow, int kingCol)
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

                if(PieceAt(checkRow, checkCol) == otherColorRook || PieceAt(checkRow, checkCol) == otherColorQueen)
                {
                    return true;
                }

                //If we hit another piece besides a rook or queen, break out of the loop.
                if(PieceAt(checkRow, checkCol) != PIECE.NONE)
                {
                    break;
                }
            }
        }
        return false;
    }

    private bool KingAttackOnKing(PLAYER player, int kingRow, int kingCol)
    {
        // This function may seem strange since it is illegal for a king to move to be in position to attack a king.
        // but that is exactly why we need this function. This is useful when checking if a move is legal.
        PIECE otherColorKing = (player == PLAYER.WHITE) ? PIECE.BLACK_KING : PIECE.WHITE_KING;
        List<(int rowDiff, int colDiff)> diffs = new(){(1, -1), (1,0), (1,1), (0, -1), (0, 1), (-1, -1), (-1, 0), (-1, 1)};

        foreach(var diff in diffs)
        {
            int r = kingRow + diff.rowDiff;
            int c = kingCol + diff.colDiff;
            if(  r >= 0 && r <= 7 && c >= 0 && c <= 7)
            {
                if(PieceAt(r,c) == otherColorKing)
                {
                    return true;
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

    private void AddWhitePawnMoves(Location loc, List<Move> moves)
    {
        if(PieceAt(loc.Row+1, loc.Column) == PIECE.NONE)
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
        if(loc.Row == 1 && PieceAt(loc.Row+1, loc.Column) == PIECE.NONE && PieceAt(loc.Row+2, loc.Column) == PIECE.NONE)
        {
            moves.Add(new Move(loc, new Location(loc.Row+2, loc.Column)));
        }

        //attack up and left
        if(loc.Column != 0 && LOCATION_COLOR.BLACK == LocationColor(PieceAt(loc.Row+1, loc.Column-1)))
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
        if(loc.Column != 7 && LOCATION_COLOR.BLACK == LocationColor(PieceAt(loc.Row+1, loc.Column+1)))
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
        if(EnPassanteSquare?.Row == loc.Row+1 && EnPassanteSquare?.Column == loc.Column-1)
        {
            moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column-1)));
        }

        //EnPassante Right
        if(EnPassanteSquare?.Row == loc.Row+1 && EnPassanteSquare?.Column == loc.Column+1)
        {
            moves.Add(new Move(loc, new Location(loc.Row+1, loc.Column+1)));
        }

    }


    private void AddBlackPawnMoves(Location loc, List<Move> moves)
    {
        if(PieceAt(loc.Row-1, loc.Column) == PIECE.NONE)
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

        if(loc.Row == 6 && PieceAt(loc.Row-1, loc.Column) == PIECE.NONE && PieceAt(loc.Row-2, loc.Column) == PIECE.NONE)
        {
            moves.Add(new Move(loc, new Location(loc.Row-2, loc.Column)));
        }

        //attack down and left
        if(loc.Column != 0 && LOCATION_COLOR.WHITE == LocationColor(PieceAt(loc.Row-1, loc.Column-1)))
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
        if(loc.Column != 7 && LOCATION_COLOR.WHITE == LocationColor(PieceAt(loc.Row-1, loc.Column+1)))
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
        if(EnPassanteSquare?.Row == loc.Row-1 && EnPassanteSquare?.Column == loc.Column-1)
        {
            moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column-1)));
        }

        //EnPassante Right
        if(EnPassanteSquare?.Row == loc.Row-1 && EnPassanteSquare?.Column == loc.Column+1)
        {
            moves.Add(new Move(loc, new Location(loc.Row-1, loc.Column+1)));
        }
    }

    private void AddColumnRowMoves(Location loc, List<Move> moves, LOCATION_COLOR playerColor)
    {
        List<(int rowDiff, int colDiff)> diffs = new(){ (1,0), (-1,0), (0, -1), (0,1)};

        foreach(var p in diffs)
        {
            int r = loc.Row+p.rowDiff;
            int c = loc.Column+p.colDiff;
            while(r >= 0 && r <= 7 && c >= 0 && c <= 7)
            {
                var locColor = LocationColor(PieceAt(r,c));
                if(locColor != playerColor)
                {
                    moves.Add(new Move(loc, new Location(r, c)));
                }

                if(locColor != LOCATION_COLOR.NO_PIECE)
                {
                    break;
                }

                r += p.rowDiff;
                c += p.colDiff;
            }
        }
    }

    private void AddDiagonalMoves(Location loc, List<Move> moves, LOCATION_COLOR playerColor)
    {
        List<(int rowDiff, int colDiff)> diffs = new(){ (1,-1), (1,1), (-1, 1), (-1,-1)};
        foreach(var p in diffs)
        {
            int r = loc.Row+p.rowDiff;
            int c = loc.Column+p.colDiff;
            while(r >= 0 && r <= 7 && c >= 0 && c <= 7)
            {
                var locColor = LocationColor(PieceAt(r,c));
                if(locColor != playerColor)
                {
                    moves.Add(new Move(loc, new Location(r, c)));
                }

                if(locColor != LOCATION_COLOR.NO_PIECE)
                {
                    break;
                }

                r += p.rowDiff;
                c += p.colDiff;
            }
        }
    }


    private void AddKnightMoves(Location loc, List<Move> moves, LOCATION_COLOR playerColor)
    {
        List<(int rowDiff, int colDiff)> diffs = new(){(2,-1), (2,1), (1,2), (-1,2), (-2,1), (-2,-1), (-1,-2), (1,-2)};

        foreach(var diff in diffs)
        {
            int r = loc.Row + diff.rowDiff;
            int c = loc.Column + diff.colDiff;

            if( c >= 0 && c <= 7 && r >= 0 && r <= 7 && playerColor != LocationColor(PieceAt(r,c)))
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }
        }
    }

    private void AddKingMoves(Location loc, List<Move> moves, LOCATION_COLOR playerColor)
    {
        List<(int rowDiff, int colDiff)> diffs = new(){(1,-1), (1,0), (1,1), (0,-1), (0,1), (-1,-1), (-1,0), (-1,1)};

        foreach(var diff in diffs)
        {
            int r = loc.Row + diff.rowDiff;
            int c = loc.Column + diff.colDiff;

            if( c >= 0 && c <= 7 && r >= 0 && r <= 7 && playerColor != LocationColor(PieceAt(r,c)))
            {
                moves.Add(new Move(loc, new Location(r, c)));
            }
        }

        if( playerColor == LOCATION_COLOR.WHITE && loc.Row == 0 && loc.Column == 4 && WhiteIsInCheck() == false)
        {
            if(WhiteCanKingCastle == true && PieceAt(0,5) == PIECE.NONE && PieceAt(0,6) == PIECE.NONE && PieceAt(0,7) == PIECE.WHITE_ROOK && WhiteKingWouldMoveThroughCheckKingSide() == false)
            {
                moves.Add(new Move(loc, new Location(0, 6)));
            }

            if(WhiteCanQueenCastle == true && PieceAt(0,3) == PIECE.NONE && PieceAt(0,2) == PIECE.NONE && PieceAt(0,1) == PIECE.NONE && PieceAt(0,0) == PIECE.WHITE_ROOK && WhiteKingWouldMoveThroughCheckQueenSide() == false)
            {
                moves.Add(new Move(loc, new Location(0, 2)));
            }
        }

        if( playerColor == LOCATION_COLOR.BLACK && loc.Row == 7 && loc.Column == 4 && BlackIsInCheck() == false)
        {
            if(BlackCanKingCastle == true && PieceAt(7,5) == PIECE.NONE && PieceAt(7,6) == PIECE.NONE && PieceAt(7,7) == PIECE.BLACK_ROOK && BlackKingWouldMoveThroughCheckKingSide() == false)
            {
                moves.Add(new Move(loc, new Location(7, 6)));
            }

            if( BlackCanQueenCastle == true && PieceAt(7,3) == PIECE.NONE && PieceAt(7,2) == PIECE.NONE && PieceAt(7,1) == PIECE.NONE && PieceAt(7,0) == PIECE.BLACK_ROOK && BlackKingWouldMoveThroughCheckQueenSide() == false)
            {
                moves.Add(new Move(loc, new Location(7, 2)));
            }
        }
    }

    private bool WhiteKingWouldMoveThroughCheckKingSide()
    {
        var BoardCopy = this.Board;
        BoardCopy[BoardArrayLocation(0,4)] = PIECE.NONE;
        BoardCopy[BoardArrayLocation(0,5)] = PIECE.WHITE_KING;

        var stateCopy = this with {Board = BoardCopy};
        return stateCopy.WhiteIsInCheck();
    }

    private bool WhiteKingWouldMoveThroughCheckQueenSide()
    {
        var BoardCopy = this.Board;
        BoardCopy[BoardArrayLocation(0,4)] = PIECE.NONE;
        BoardCopy[BoardArrayLocation(0,3)] = PIECE.WHITE_KING;


        var stateCopy = this with {Board = BoardCopy};
        return stateCopy.WhiteIsInCheck();
    }

    private bool BlackKingWouldMoveThroughCheckKingSide()
    {
        var BoardCopy = this.Board;
        BoardCopy[BoardArrayLocation(7,4)] = PIECE.NONE;
        BoardCopy[BoardArrayLocation(7,5)] = PIECE.BLACK_KING;

        var stateCopy = this with {Board = BoardCopy};
        return stateCopy.BlackIsInCheck();
    }

    private bool BlackKingWouldMoveThroughCheckQueenSide()
    {
        var BoardCopy = this.Board;
        BoardCopy[BoardArrayLocation(7,4)] = PIECE.NONE;
        BoardCopy[BoardArrayLocation(7,3)] = PIECE.BLACK_KING;

        var stateCopy = this with {Board = BoardCopy};
        return stateCopy.BlackIsInCheck();
    }


}
