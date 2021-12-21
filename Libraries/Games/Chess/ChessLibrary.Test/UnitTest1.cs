using Xunit;

namespace ChessLibrary.Test;

public class LibraryScopeTests
{
    [Fact]
    public void BoardStateIsPublic()
    {
        BoardState b = new BoardState();
    }

    [Fact]
    public void PlayerEnumIsPublic()
    {
        PLAYER b = PLAYER.WHITE;
    }

    [Fact]
    public void PieceEnumIsPublic()
    {
        PIECE b = PIECE.WHITE_PAWN;
    }

    [Fact]
    public void LocationIsPublic()
    {
        Location l = new Location();
    }
}
