using System;
using Xunit;

namespace FifteenPuzzleGame.Test
{
    public class FifteenPuzzleTest
    {
        [Fact]
        public void BoardSizeIs16()
        {
            FifteenPuzzleService p = new();

            Assert.Equal(16, p.CurrentBoard.Length);
        }

        [Fact]
        public void NumbersOfZeroToFiftenAreOnTheBoard()
        {
            FifteenPuzzleService p = new();

            var board = p.CurrentBoard;

            for(int n = 0; n <= 15; n++)
            {
                int count = 0;
                for(int i = 0; i < board.Length; i++)
                {
                    if(board[i] == n)
                        count++;
                }
                Assert.Equal(1, count);
            }
        }

        [Fact]
        public void ZeroIsInTheLastPositionToStart()
        {
            FifteenPuzzleService p = new();

            var board = p.CurrentBoard;

            Assert.Equal(0, board[15]);
        }

        [Fact]
        public void PastInAreaBecomesTheCurrentBoard()
        {
            int[] toPassIn = new int[] {1,6,2,7,3,8,4,9,5,10,11,15,12,14,13, 0};

            FifteenPuzzleService p = new(toPassIn);

            Assert.Equal(toPassIn, p.CurrentBoard);
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(2, null)]
        [InlineData(3, null)]
        [InlineData(4, null)]
        [InlineData(5, new int[] {1,2,3,4,
                                  5,0,7,8,
                                  9,6,10,11,
                                  12,13,14,15})]
        [InlineData(6, null)]
        [InlineData(7, null)]
        [InlineData(8, new int[] {1,2,3,4,
                                  5,6,7,8,
                                  0,9,10,11,
                                  12,13,14,15})]
        [InlineData(9, null)]
        [InlineData(10, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,10,0,11,
                                   12,13,14,15})]
        [InlineData(11, null)]
        [InlineData(12, null)]
        [InlineData(13, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,13,10,11,
                                   12,0,14,15})]
        [InlineData(14, null)]
        [InlineData(15, null)]
        public void ZeroInMiddleMoves(int moveLoc, int[] expectedOutput)
        {
            int[] toPassIn = new int[] {1,2,3,4,
                                        5,6,7,8,
                                        9,0,10,11,
                                        12,13,14,15};

            FifteenPuzzleService p = new(toPassIn);
            p.Move(moveLoc);

            if(expectedOutput == null)
            {
                // No Change
                Assert.Equal(toPassIn, p.CurrentBoard);
            }
            else
            {
                //Assert.Equal(expectedOutput, p.CurrentBoard);
                for(int i = 0; i < expectedOutput.Length; i++)
                {
                    Assert.Equal(expectedOutput[i], p.CurrentBoard[i]);
                }
            }
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(2, null)]
        [InlineData(3, null)]
        [InlineData(4, null)]
        [InlineData(5, null)]
        [InlineData(6, null)]
        [InlineData(7, null)]
        [InlineData(8, null)]
        [InlineData(9, null)]
        [InlineData(10, null)]
        [InlineData(11, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,10,11,0,
                                   13,14,15,12})]
        [InlineData(12, null)]
        [InlineData(13, null)]
        [InlineData(14, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,10,11,12,
                                   13,14,0,15})]
        [InlineData(15, null)]
        public void ZeroInLowerRight(int moveLoc, int[] expectedOutput)
        {
            int[] toPassIn = new int[] {1,2,3,4,
                                        5,6,7,8,
                                        9,10,11,12,
                                        13,14,15,0};

            FifteenPuzzleService p = new(toPassIn);
            p.Move(moveLoc);

            if(expectedOutput == null)
            {
                // No Change
                Assert.Equal(toPassIn, p.CurrentBoard);
            }
            else
            {
                //Assert.Equal(expectedOutput, p.CurrentBoard);
                for(int i = 0; i < expectedOutput.Length; i++)
                {
                    Assert.Equal(expectedOutput[i], p.CurrentBoard[i]);
                }
            }
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(2, null)]
        [InlineData(3, null)]
        [InlineData(4, null)]
        [InlineData(5, null)]
        [InlineData(6, null)]
        [InlineData(7, null)]
        [InlineData(8, null)]
        [InlineData(9, null)]
        [InlineData(10, null)]
        [InlineData(11, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,10,11,0,
                                   13,14,15,12})]
        [InlineData(12, null)]
        [InlineData(13, null)]
        [InlineData(14, new int[] {1,2,3,4,
                                   5,6,7,8,
                                   9,10,11,12,
                                   13,14,0,15})]
        [InlineData(15, null)]
        public void ZeroInLowerLeft(int moveLoc, int[] expectedOutput)
        {
            int[] toPassIn = new int[] {1,2,3,4,
                                        5,6,7,8,
                                        9,10,11,12,
                                        13,14,15,0};

            FifteenPuzzleService p = new(toPassIn);
            p.Move(moveLoc);

            if(expectedOutput == null)
            {
                // No Change
                Assert.Equal(toPassIn, p.CurrentBoard);
            }
            else
            {
                //Assert.Equal(expectedOutput, p.CurrentBoard);
                for(int i = 0; i < expectedOutput.Length; i++)
                {
                    Assert.Equal(expectedOutput[i], p.CurrentBoard[i]);
                }
            }
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, new int[] {1,0,2,3,
                                  4,5,6,7,
                                  8,9,10,11,
                                  12,13,14,15})]
        [InlineData(2, null)]
        [InlineData(3, null)]
        [InlineData(4, new int[] {4,1,2,3,
                                  0,5,6,7,
                                  8,9,10,11,
                                  12,13,14,15})]
        [InlineData(5, null)]
        [InlineData(6, null)]
        [InlineData(7, null)]
        [InlineData(8, null)]
        [InlineData(9, null)]
        [InlineData(10, null)]
        [InlineData(11, null)]
        [InlineData(12, null)]
        [InlineData(13, null)]
        [InlineData(14, null)]
        [InlineData(15, null)]
        public void ZeroInUpperLeft(int moveLoc, int[] expectedOutput)
        {
            int[] toPassIn = new int[] {0,1,2,3,
                                        4,5,6,7,
                                        8,9,10,11,
                                        12,13,14,15};

            FifteenPuzzleService p = new(toPassIn);
            p.Move(moveLoc);

            if(expectedOutput == null)
            {
                // No Change
                Assert.Equal(toPassIn, p.CurrentBoard);
            }
            else
            {
                //Assert.Equal(expectedOutput, p.CurrentBoard);
                for(int i = 0; i < expectedOutput.Length; i++)
                {
                    Assert.Equal(expectedOutput[i], p.CurrentBoard[i]);
                }
            }
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, null)]
        [InlineData(2, new int[] {1,2,0,3,
                                  4,5,6,7,
                                  8,9,10,11,
                                  12,13,14,15})]
        [InlineData(3, null)]
        [InlineData(4, null)]
        [InlineData(5, null)]
        [InlineData(6, null)]
        [InlineData(7, new int[] {1,2,3,7,
                                  4,5,6,0,
                                  8,9,10,11,
                                  12,13,14,15})]
        [InlineData(8, null)]
        [InlineData(9, null)]
        [InlineData(10, null)]
        [InlineData(11, null)]
        [InlineData(12, null)]
        [InlineData(13, null)]
        [InlineData(14, null)]
        [InlineData(15, null)]
        public void ZeroInUpperRight(int moveLoc, int[] expectedOutput)
        {
            int[] toPassIn = new int[] {1,2,3,0,
                                        4,5,6,7,
                                        8,9,10,11,
                                        12,13,14,15};

            FifteenPuzzleService p = new(toPassIn);
            p.Move(moveLoc);

            if(expectedOutput == null)
            {
                // No Change
                Assert.Equal(toPassIn, p.CurrentBoard);
            }
            else
            {
                //Assert.Equal(expectedOutput, p.CurrentBoard);
                for(int i = 0; i < expectedOutput.Length; i++)
                {
                    Assert.Equal(expectedOutput[i], p.CurrentBoard[i]);
                }
            }
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-3)]
        [InlineData(-5)]
        [InlineData(-1)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(500)]
        public void MovesAbove15AndBelow0DoNothing(int moveLoc)
        {
            int[] toPassIn = new int[] {1,2,3,0,
                                        4,5,6,7,
                                        8,9,10,11,
                                        12,13,14,15};
            FifteenPuzzleService p = new(toPassIn);

            p.Move(moveLoc);

            Assert.Equal(toPassIn, p.CurrentBoard);
        }

        [Fact]
        public void ValuesOfCurrentBoardCannotBeChangedExternally()
        {
            FifteenPuzzleService p = new();

            p.CurrentBoard[8] = 1000;
            Assert.NotEqual(1000, p.CurrentBoard[8]);
        }


    }
}
