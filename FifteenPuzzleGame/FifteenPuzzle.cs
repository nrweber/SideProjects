using System;
using System.Linq;

namespace FifteenPuzzleGame
{
    public class FifteenPuzzle
    {
        private int[] _solution = new int[16] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
        private int[] _currentBoard = new int[16]{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};

        private int _zeroPosition = 15;

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

            //You cannot just shuffle the array. If you do the puzzle may
            // not be solvable. So, this loop just make a bunch of random moves
            // to shuffle up the board.
            for(int i = 0; i < 10000; i++)
            {
                int loc = rnd.Next(0, 16);
                Move(loc);
            }

            //Move the zero to the bottom right corner
            while(_zeroPosition %4 != 3)
                Move(_zeroPosition+1);

            while(_zeroPosition != 15)
                Move(_zeroPosition+4);

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
