using System;
using System.Collections.Generic;


namespace TicTacToeGame
{
    public class TicTacToe
    {
        private int moveNumber = 0;
        public int ToMove 
        {
            get
            {
                return (moveNumber%2)+1;
            }
        }

        private List<int[]> winningPaths = new List<int[]>{
            new int[] {0,1,2},
            new int[] {3,4,5},
            new int[] {6,7,8},
            new int[] {0,3,6},
            new int[] {1,4,7},
            new int[] {2,5,8},
            new int[] {0,4,8},
            new int[] {2,4,6},
        };

        private int[] _board = new int[9];
        public int[] Board
        {
            get
            {
                var toReturn = new int[_board.Length];
                for(int i = 0; i < toReturn.Length; i++)
                    toReturn[i] = _board[i];
                return toReturn;
            }
        }


        public void Place(int location)
        {
            if(Winner() == 0 && location >= 0 && location <= 8 && _board[location] == 0)
            {
                _board[location] = (moveNumber%2)+1;
                moveNumber += 1;
            }
        }

        public int Winner()
        {
            foreach(var path in winningPaths)
            {
                if(_board[path[0]] != 0 && _board[path[0]] == _board[path[1]] && _board[path[0]] == _board[path[2]])
                    return _board[path[0]];
            }
            if(moveNumber == 9)
                return 3;
            return 0;
        }

    }
}
