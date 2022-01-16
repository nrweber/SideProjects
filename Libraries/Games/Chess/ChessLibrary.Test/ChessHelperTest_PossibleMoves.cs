using Xunit;

namespace ChessLibrary.Test;

public class ChessHeplerTest_PosibleMoves
{
    // Did not go all out on these test. The "ForLocation" test cover all the moves well.
    // May come back and add a few more later.

    [Fact]
    public void StartingBoard_WhitesTurn()
    {
        BoardState state = new();

        var moves = ChessHelper.PossibleMoves(state);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //Knights
                item => Assert.True(item.Equals(new Move(new Location(0,1), new Location(2,0)))),
                item => Assert.True(item.Equals(new Move(new Location(0,1), new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(new Location(0,6), new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(new Location(0,6), new Location(2,7)))),
                //Pawns
                item => Assert.True(item.Equals(new Move(new Location(1,0), new Location(2,0)))),
                item => Assert.True(item.Equals(new Move(new Location(1,0), new Location(3,0)))),
                item => Assert.True(item.Equals(new Move(new Location(1,1), new Location(2,1)))),
                item => Assert.True(item.Equals(new Move(new Location(1,1), new Location(3,1)))),
                item => Assert.True(item.Equals(new Move(new Location(1,2), new Location(2,2)))),
                item => Assert.True(item.Equals(new Move(new Location(1,2), new Location(3,2)))),
                item => Assert.True(item.Equals(new Move(new Location(1,3), new Location(2,3)))),
                item => Assert.True(item.Equals(new Move(new Location(1,3), new Location(3,3)))),
                item => Assert.True(item.Equals(new Move(new Location(1,4), new Location(2,4)))),
                item => Assert.True(item.Equals(new Move(new Location(1,4), new Location(3,4)))),
                item => Assert.True(item.Equals(new Move(new Location(1,5), new Location(2,5)))),
                item => Assert.True(item.Equals(new Move(new Location(1,5), new Location(3,5)))),
                item => Assert.True(item.Equals(new Move(new Location(1,6), new Location(2,6)))),
                item => Assert.True(item.Equals(new Move(new Location(1,6), new Location(3,6)))),
                item => Assert.True(item.Equals(new Move(new Location(1,7), new Location(2,7)))),
                item => Assert.True(item.Equals(new Move(new Location(1,7), new Location(3,7))))
                );
    }

    [Fact]
    public void Aftere2e4_BlacksTurn()
    {
        //Get Classic Board
        var Board = new BoardState().Board;
        Board[(3*8)+4] = Board[(1*8)+4];
        Board[(1*8)+4] = PIECE.NONE;
        BoardState state = new(Board, CurrentTurn: PLAYER.BLACK);

        var moves = ChessHelper.PossibleMoves(state);

        Assert.NotEmpty(moves);
        Assert.Collection(moves,
                //Pawns
                item => Assert.True(item.Equals(new Move(new Location(6,0), new Location(5,0)))),
                item => Assert.True(item.Equals(new Move(new Location(6,0), new Location(4,0)))),
                item => Assert.True(item.Equals(new Move(new Location(6,1), new Location(5,1)))),
                item => Assert.True(item.Equals(new Move(new Location(6,1), new Location(4,1)))),
                item => Assert.True(item.Equals(new Move(new Location(6,2), new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(new Location(6,2), new Location(4,2)))),
                item => Assert.True(item.Equals(new Move(new Location(6,3), new Location(5,3)))),
                item => Assert.True(item.Equals(new Move(new Location(6,3), new Location(4,3)))),
                item => Assert.True(item.Equals(new Move(new Location(6,4), new Location(5,4)))),
                item => Assert.True(item.Equals(new Move(new Location(6,4), new Location(4,4)))),
                item => Assert.True(item.Equals(new Move(new Location(6,5), new Location(5,5)))),
                item => Assert.True(item.Equals(new Move(new Location(6,5), new Location(4,5)))),
                item => Assert.True(item.Equals(new Move(new Location(6,6), new Location(5,6)))),
                item => Assert.True(item.Equals(new Move(new Location(6,6), new Location(4,6)))),
                item => Assert.True(item.Equals(new Move(new Location(6,7), new Location(5,7)))),
                item => Assert.True(item.Equals(new Move(new Location(6,7), new Location(4,7)))),
                //Knights
                item => Assert.True(item.Equals(new Move(new Location(7,1), new Location(5,2)))),
                item => Assert.True(item.Equals(new Move(new Location(7,1), new Location(5,0)))),
                item => Assert.True(item.Equals(new Move(new Location(7,6), new Location(5,7)))),
                item => Assert.True(item.Equals(new Move(new Location(7,6), new Location(5,5))))
                );
    }

}
