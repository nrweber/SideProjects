namespace ChessLibrary;

public class BasicTwoPersonChessService
{
    public Action DataUpdated { get; set; } = delegate { };

    BoardState _state = new();
    public BoardState State
    {
        get
        {
            return _state;
        }
    }

    public bool MakeMove(Move m)
    {
        (var moveHappend, _state) = ChessHelper.MakeMove(_state, m);
        DataUpdated();
        return moveHappend;
    }

    public void NewGame()
    {
        _state = new();
        DataUpdated();
    }
}
