using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day11
    {
        public static string _sampleInput = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

        private const int BOARD_SIZE = 10;

        private static int _numFlashes = 0;

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var lineLength = lines[0].Length;

            var board = GetBoard(lines);

            for (int i = 0; i < 100; i++)
            {
                IncrementBoard(board);
                var newFlashes = board.SelectMany(x => x).Where(x => x > 9).Any();
                while (newFlashes)
                {
                    ProcessFlashes(board);
                    newFlashes = board.SelectMany(x => x).Where(x => x > 9).Any();
                }
                ResetFlashedLocations(board);
                PrintBoard(board, i + 1);
            }

            return _numFlashes.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var lineLength = lines[0].Length;

            var board = GetBoard(lines);

            var tracker = 0;

            while (true) {
                tracker++;
                IncrementBoard(board);
                var newFlashes = board.SelectMany(x => x).Where(x => x > 9).Any();
                while (newFlashes)
                {
                    ProcessFlashes(board);
                    newFlashes = board.SelectMany(x => x).Where(x => x > 9).Any();
                }
                ResetFlashedLocations(board);
                if (board.SelectMany(x => x).Where(x => x == 0).Count() == 100)
                {
                    break;
                }
                //PrintBoard(board, tracker);
            }

            return tracker.ToString();
        }

        #region Private Methods
        private static void ProcessFlash(Point pos, List<List<int>> board)
        {
            var x = pos.X;
            var y = pos.Y;

            board[y][x] = -1;
            _numFlashes++;

            //Increment above
            if (y != 0)
            {
                //above
                if (board[y - 1][x] != -1)
                {
                    board[y - 1][x] += 1;
                }

                //above left
                if (x != 0)
                {
                    if (board[y - 1][x-1] != -1)
                    {
                        board[y - 1][x-1] += 1;
                    }
                }

                //above right
                if (x != BOARD_SIZE - 1)
                {
                    if (board[y - 1][x + 1] != -1)
                    {
                        board[y - 1][x + 1] += 1;
                    }
                }
            }

            //check below
            if (y != BOARD_SIZE - 1)
            {

                //below
                if (board[y + 1][x] != -1)
                {
                    board[y + 1][x] += 1;
                }

                //below left
                if (x != 0)
                {
                    if (board[y + 1][x - 1] != -1)
                    {
                        board[y + 1][x - 1] += 1;
                    }
                }

                //below right
                if (x != BOARD_SIZE - 1)
                {
                    if (board[y + 1][x + 1] != -1)
                    {
                        board[y + 1][x + 1] += 1;
                    }
                }
            }

            //check left
            if (x != 0)
            {
                if (board[y][x - 1] != -1)
                {
                    board[y][x - 1] += 1;
                }
            }

            //check right
            if (x != BOARD_SIZE - 1)
            {
                if (board[y][x + 1] != -1)
                {
                    board[y][x + 1] += 1;
                }
            }
        }

        private static void ProcessFlashes(List<List<int>> board)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    if (board[y][x] > 9)
                    {
                        ProcessFlash(new Point { X = x, Y = y }, board);
                    }
                }
            }
        }

        private static void ResetFlashedLocations(List<List<int>> board)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    if (board[y][x] == -1)
                    {
                        board[y][x] = 0;
                    }
                }
            }
        }

        private static void IncrementBoard(List<List<int>> board)
        {
            for(int y = 0; y < BOARD_SIZE; y++)
            {
                for(int x = 0; x < BOARD_SIZE; x++)
                {
                    board[x][y] = board[x][y] + 1;
                }
            }
        }

        private static List<List<int>> GetBoard(string[] lines)
        {
            var board = new List<List<int>>();
            foreach(var line in lines)
            {
                var row = new List<int>();
                for(int i = 0; i < BOARD_SIZE; i++)
                {
                    var thisNum = Int32.Parse(line[i].ToString());
                    row.Add(thisNum);
                }
                board.Add(row);
            }

            return board;
        }

        private static void PrintBoard(List<List<int>> board, int stepNum)
        {
            Console.WriteLine("After step " + stepNum);

            foreach(var line in board)
            {
                foreach(var num in line)
                {
                    Console.Write(num.ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion

    }
}
