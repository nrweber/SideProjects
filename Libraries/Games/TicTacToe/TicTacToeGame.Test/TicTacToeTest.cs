using System;
using Xunit;

using TicTacToeGame;

namespace TicTacToeGame.Test
{
    public class TicTacToeTest
    {
        [Fact]
        public void BoradIsBlankToStartWith()
        {
            var g = new TicTacToe();

            var b = g.Board;

            for(int i = 0; i < 9; i++)
            {
                Assert.Equal(0, b[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 0, 2, 1, 3, 5, 4})]
        [InlineData(new int[] { 4, 5, 3, 1, 2, 0})]
        public void AlerternatesXThenO(int[] locations)
        {
            var g = new TicTacToe();

            for(int i = 0; i < locations.Length; i++)
            {
                var l = locations[i];
                Assert.Equal(0, g.Board[l]);

                g.Place(l);

                if(i % 2 == 0)
                    Assert.Equal(1, g.Board[l]);
                else
                    Assert.Equal(2, g.Board[l]);
            }
        }

        [Theory]
        [InlineData(new int[] { 0, 3, 1, 4, 2})] // 0, 1, 2
        [InlineData(new int[] { 5, 1, 4, 8, 3})] // 3, 4, 5
        [InlineData(new int[] { 8, 1, 6, 0, 7})] // 6, 7, 8
        [InlineData(new int[] {0, 1, 3, 7, 6})] // 0, 3, 6
        [InlineData(new int[] {1, 3, 7, 5, 4})] // 1, 4, 7
        [InlineData(new int[] {8, 4, 5, 0, 2})] // 2, 5, 8
        [InlineData(new int[] {8, 2, 0, 3, 4})] // 0, 4, 8
        [InlineData(new int[] {2, 1, 6, 5, 4})] // 2, 4, 6
        public void CheckForXWin(int[] locations)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length-1; i++)
            {
                g.Place(locations[i]);
                Assert.Equal(0, g.Winner);
            }

            var winningMove = locations[locations.Length-1];
            g.Place(winningMove);
            Assert.Equal(1, g.Winner);
        }

        [Theory]
        [InlineData(new int[] {8, 0, 3, 1, 4, 2})] // 0, 1, 2
        [InlineData(new int[] {0, 5, 1, 4, 8, 3})] // 3, 4, 5
        [InlineData(new int[] {3, 8, 1, 6, 0, 7})] // 6, 7, 8
        [InlineData(new int[] {2, 0, 1, 3, 7, 6})] // 0, 3, 6
        [InlineData(new int[] {8, 1, 3, 7, 5, 4})] // 1, 4, 7
        [InlineData(new int[] {1, 8, 4, 5, 0, 2})] // 2, 5, 8
        [InlineData(new int[] {7, 8, 2, 0, 3, 4})] // 0, 4, 8
        [InlineData(new int[] {0, 2, 1, 6, 5, 4})] // 2, 4, 6
        public void CheckForOWin(int[] locations)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length-1; i++)
            {
                g.Place(locations[i]);
                Assert.Equal(0, g.Winner);
            }

            var winningMove = locations[locations.Length-1];
            g.Place(winningMove);
            Assert.Equal(2, g.Winner);
        }

        [Theory]
        [InlineData(new int[] {0,1,5,2,6,3,4,8,7})]
        [InlineData(new int[] {0,1,2,4,3,5,7,6,8})]
        [InlineData(new int[] {0,1,2,3,4,6,5,8,7})]
        public void CheckForCatGame(int[] locations)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length-1; i++)
            {
                g.Place(locations[i]);
                Assert.Equal(0, g.Winner);
            }

            var lastMove = locations[locations.Length-1];
            g.Place(lastMove);
            Assert.Equal(3, g.Winner);
        }

        [Theory]
        [InlineData(new int[] { 5, 1, 4, 8, 3, 0})]
        [InlineData(new int[] {1, 3, 7, 5, 4, 2})]
        [InlineData(new int[] {8, 4, 5, 0, 2, 6})]
        [InlineData(new int[] {3, 8, 1, 6, 0, 7, 2})]
        [InlineData(new int[] {8, 1, 3, 7, 5, 4, 2})]
        [InlineData(new int[] {1, 8, 4, 5, 0, 2, 7})]
        public void MovesAreIgnoredAfterWin(int[] locations)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length-2; i++)
            {
                g.Place(locations[i]);
                Assert.Equal(0, g.Winner);
            }

            var winningMove = locations[locations.Length-2];
            g.Place(winningMove);
            Assert.NotEqual(0, g.Winner);

            var extraMove = locations[locations.Length-1];
            var boardBefore = g.Board;
            g.Place(extraMove);
            var boardAfter = g.Board;

            Assert.Equal(boardBefore, boardAfter);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void PlayingInALocationAgainIsIgnored(int l)
        {
            var g = new TicTacToe();

            g.Place(l);

            var boardBefore = g.Board;
            g.Place(l);
            var boardAfter = g.Board;

            Assert.Equal(boardBefore, boardAfter);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(15)]
        [InlineData(-2)]
        [InlineData(347993)]
        public void PlayingOutsideBoardIsIgnored(int l)
        {
            var g = new TicTacToe();

            var boardBefore = g.Board;
            g.Place(l);
            var boardAfter = g.Board;

            Assert.Equal(boardBefore, boardAfter);
        }

        [Fact]
        public void ToMoveReturnsWhosTurnItIs()
        {
            var g = new TicTacToe();

            Assert.Equal(1, g.ToMove);
            g.Place(0);

            Assert.Equal(2, g.ToMove);
            g.Place(1);

            Assert.Equal(1, g.ToMove);
            g.Place(3);

            Assert.Equal(2, g.ToMove);
            g.Place(4);
        }

        [Theory]
        [InlineData(new int[] { 0, 3, 1, 4, 2}, new int[] {0, 1, 2})]
        [InlineData(new int[] { 5, 1, 4, 8, 3}, new int[] {3, 4, 5})]
        [InlineData(new int[] { 8, 1, 6, 0, 7}, new int[] {6, 7, 8})]
        [InlineData(new int[] {0, 1, 3, 7, 6}, new int[] {0, 3, 6})]
        [InlineData(new int[] {1, 3, 7, 5, 4}, new int[] {1, 4, 7})]
        [InlineData(new int[] {8, 4, 5, 0, 2}, new int[] {2, 5, 8})]
        [InlineData(new int[] {8, 2, 0, 3, 4}, new int[] {0, 4, 8})]
        [InlineData(new int[] {2, 1, 6, 5, 4}, new int[] {2, 4, 6})]
        [InlineData(new int[] {8, 0, 3, 1, 4, 2}, new int[] {0, 1, 2})]
        [InlineData(new int[] {0, 5, 1, 4, 8, 3}, new int[] {3, 4, 5})]
        [InlineData(new int[] {3, 8, 1, 6, 0, 7}, new int[] {6, 7, 8})]
        [InlineData(new int[] {2, 0, 1, 3, 7, 6}, new int[] {0, 3, 6})]
        [InlineData(new int[] {8, 1, 3, 7, 5, 4}, new int[] {1, 4, 7})]
        [InlineData(new int[] {1, 8, 4, 5, 0, 2}, new int[] {2, 5, 8})]
        [InlineData(new int[] {7, 8, 2, 0, 3, 4}, new int[] {0, 4, 8})]
        [InlineData(new int[] {0, 2, 1, 6, 5, 4}, new int[] {2, 4, 6})]
        public void WinningPathGivesTheLocationsOfTheWinningPlays(int[] locations, int[] winningPath)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length; i++)
                g.Place(locations[i]);

            Assert.Equal(winningPath, g.WinningPath);
        }

        [Theory]
        [InlineData(new int[] {5, 1, 4}, 8)]
        [InlineData(new int[] {1, 3}, 7)]
        [InlineData(new int[] {8, 4, 5}, 0)]
        [InlineData(new int[] {3, 8, 1}, 6)]
        [InlineData(new int[] {8, 1}, 3)]
        [InlineData(new int[] {1, 8, 4}, 5)]
        public void UndoMoves(int[] startingMoves, int moveToDoAndUndo)
        {
            var g = new TicTacToe();
            for(int i = 0; i < startingMoves.Length; i++)
            {
                g.Place(startingMoves[i]);
                Assert.Equal(0, g.Winner);
            }

            var boardBefore = g.Board;
            g.Place(moveToDoAndUndo);
            var boardMiddle = g.Board;
            g.Undo();
            var boardAfter = g.Board;

            Assert.NotEqual(boardMiddle, boardBefore);
            Assert.Equal(boardBefore, boardAfter);
        }

        [Theory]
        [InlineData(new int[] {0, 8, 1, 7, 2})]
        public void UndoDoesNothingIfGameIsOver(int[] locations)
        {
            var g = new TicTacToe();
            for(int i = 0; i < locations.Length-1; i++)
            {
                g.Place(locations[i]);
                Assert.Equal(0, g.Winner);
            }
            g.Place(locations[locations.Length-1]);
            Assert.Equal(1, g.Winner);


            var boardBefore = g.Board;
            g.Undo();
            var boardAfter = g.Board;

            Assert.Equal(boardBefore, boardAfter);
        }

    }
}
