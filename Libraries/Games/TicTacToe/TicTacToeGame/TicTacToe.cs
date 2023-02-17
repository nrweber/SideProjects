using System;
using System.Collections.Generic;


namespace TicTacToeGame
{
    public class TicTacToe
    {
        private Stack<int> _moves = new();
        private int moveNumber 
        {
            get
            {
                return _moves.Count;
            }
        }
        
        public int ToMove
        {
            get
            {
                return (moveNumber%2)+1;
            }
        }

        private int _winner = 0;
        public int Winner { get { return _winner; } }

        private int[] _winningPath = new int[0];
        public int[] WinningPath
        {
            get
            {
                var toReturn = new int[_winningPath.Length];
                for(int i = 0; i < toReturn.Length; i++)
                    toReturn[i] = _winningPath[i];
                return toReturn;
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
            if(Winner == 0 && location >= 0 && location <= 8 && _board[location] == 0)
            {
                _board[location] = (moveNumber%2)+1;
                _moves.Push(location);

                CheckWinner();
            }
        }

        public void Undo()
        {
            if(moveNumber > 0 && _winner == 0)
            {
                int lastMove = _moves.Pop();
                _board[lastMove] = 0;
            }
        }

        public void CheckWinner()
        {
            foreach(var path in winningPaths)
            {
                if(_board[path[0]] != 0 && _board[path[0]] == _board[path[1]] && _board[path[0]] == _board[path[2]])
                {
                    _winner = _board[path[0]];
                    _winningPath = path;
                    return;
                }
            }

            if(moveNumber == 9)
            {
                _winner = 3;
                return;
            }

            //Game is still going
            _winner = 0;
        }

    }
}
