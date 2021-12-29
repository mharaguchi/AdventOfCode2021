using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day25
    {
        public static string _sampleInput = @"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>";

        //private static char[,] _board;

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var board = new char[lines[0].Length, lines.Length];

            var steps = 0;

            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[0].Length; x++)
                {
                    board[x,y] = lines[y][x];
                }
            }

            var changed = true;

            while (changed)
            {
                PrintBoard(board, steps);
                steps++;
                var result = Move(board);
                changed = result.Item2;
                board = result.Item1;
            }

            return steps.ToString();
        }

        internal static string RunPart2(string input)
        {
            return "";        
        }

        #region Private Methods
        private static (char[,], bool) Move(char[,] board)
        {
            //var moved = new List<(int, int)>();
            var moved = false;

            var maxX = board.GetLength(0);
            var maxY = board.GetLength(1);

            var newBoard = board;

            //for(int y = 0; y < maxY; y++)
            //{
            //    for(int x = 0; x < maxX; x++)
            //    {
            //        var thisChar = board[x, y];
            //        if (thisChar == '.')
            //        {
            //            newBoard[x, y] = '.';
            //        }
            //        else if (thisChar == '>')
            //        {
            //            var newX = x + 1;
            //            if (newX == maxX)
            //            {
            //                newX = 0;
            //            }
            //            if (board[newX, y] == '.')
            //            {
            //                moved = true;
            //                newBoard[x, y] = '.';
            //                newBoard[newX, y] = '>';
            //            }
            //            else
            //            {
            //                newBoard[x, y] = '>';
            //            }
            //        }
            //        else if (thisChar == 'v')
            //        {
            //            newBoard[x, y] = 'v';
            //        }
            //    }
            //}

            var canMove = new List<(int, int)>();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var thisChar = newBoard[x, y];
                    if (thisChar == '>')
                    {
                        var newX = x + 1;
                        if (newX == maxX)
                        {
                            newX = 0;
                        }
                        if (newBoard[newX, y] == '.')
                        {
                            canMove.Add((x, y));
                        }
                    }
                }
            }

            foreach(var move in canMove)
            {
                var newX = move.Item1 + 1;
                if (newX == maxX)
                {
                    newX = 0;
                }

                newBoard[move.Item1, move.Item2] = '.';
                newBoard[newX, move.Item2] = '>';

                moved = true;
            }

            canMove.Clear();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var thisChar = newBoard[x, y];
                    if (thisChar == 'v')
                    {
                        var newY = y + 1;
                        if (newY == maxY)
                        {
                            newY = 0;
                        }
                        if (newBoard[x, newY] == '.')
                        {
                            canMove.Add((x, y));
                        }
                    }
                }
            }

            foreach (var move in canMove)
            {
                var newY = move.Item2 + 1;
                if (newY == maxY)
                {
                    newY = 0;
                }

                newBoard[move.Item1, move.Item2] = '.';
                newBoard[move.Item1, newY] = 'v';

                moved = true;
            }

            return (newBoard, moved);
        }

        private static void PrintBoard(char[,] board, int stepNum)
        {
            Console.WriteLine("After step " + stepNum);

            var maxX = board.GetLength(0);
            var maxY = board.GetLength(1);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Console.Write(board[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion
    }
}
