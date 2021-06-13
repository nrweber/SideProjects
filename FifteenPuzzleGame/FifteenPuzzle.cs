using System;
using System.Linq;

namespace FifteenPuzzleGame
{
    public class FifteenPuzzle
    {
        private int[] _solution = new int[16] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
        private int[] _currentBoard = new int[16];

        private int _zeroPosition = 16;

        public int[] CurrentBoard { get { return _currentBoard.ToArray(); } }

        public FifteenPuzzle()
        {
            SetupRandomBoard();
        }

        public FifteenPuzzle(int[] startingBoard)
        {
            for(int i = 0; i < _currentBoard.Length; i++)
            {
                _currentBoard[i] = startingBoard[i];
                if(_currentBoard[i] == 0)
                {
                    _zeroPosition = i;
                }
            }
        }

        private void SetupRandomBoard()
        {
            Random rnd =new Random();

            _currentBoard = _solution.OrderBy(x => rnd.Next()).ToArray();

            //Move 0 to the end
            for(int i = 0; i < _currentBoard.Length; i++)
            {
                if(_currentBoard[i] == 0)
                {
                    _currentBoard[i] = _currentBoard[15];
                    _currentBoard[15] = 0;
                    break;
                }
            }
        }

        public void Move(int position)
        {
            if(
                    ( (_zeroPosition % 4 != 3) && (position == _zeroPosition+1)) ||   // Move Right
                    ( (_zeroPosition > 3 ) && (position == _zeroPosition-4)) ||  // Move Up
                    ( (_zeroPosition < 12) && (position == _zeroPosition+4)) ||  // Move Down
                    ( (_zeroPosition % 4 != 0) && (position == _zeroPosition-1))      // Move Left
            )
            {
                _currentBoard[_zeroPosition] = _currentBoard[position];
                _currentBoard[position] = 0;

                _zeroPosition = position;
            }
        }
    }
}
