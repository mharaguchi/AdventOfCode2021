using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;

namespace AdventOfCode2021.Days
{
    public static class Day5
    {
        public static string _sampleInput = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

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
            var board = new Dictionary<Point, int>();

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { ",", " -> ", "," });
                var coordinates = tokens.Select(x => Convert.ToInt32(x)).ToList();

                var x1 = coordinates[0];
                var y1 = coordinates[1];
                var x2 = coordinates[2];
                var y2 = coordinates[3];

                if (x1 != x2 && y1 != y2) //not horizontal or vertical line
                {
                    continue;
                }
                if (x1 == x2) // x values are the same
                {
                    /* This section of code is for situations where the line as noted is right to left or bottom to top */
                    var lowY = Math.Min(y1, y2);
                    var highY = Math.Max(y1, y2);

                    for (int j = lowY; j <= highY; j++)
                    {
                        var point = new Point(x1, j);
                        if (board.ContainsKey(point))
                        {
                            board[point]++;
                        }
                        else
                        {
                            board.Add(point, 1);
                        }
                    }
                }
                else // y values should be the same
                {
                    /* This section of code is for situations where the line as noted is right to left or bottom to top */
                    var lowX = Math.Min(x1, x2);
                    var highX = Math.Max(x1, x2);

                    for (int i = lowX; i <= highX; i++)
                    {
                        var point = new Point(i, y1);
                        if (board.ContainsKey(point))
                        {
                            board[point]++;
                        }
                        else
                        {
                            board.Add(point, 1);
                        }
                    }
                }
            }

            var valueOver2 = board.Where(x => x.Value > 1).Count();

            return valueOver2.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var board = new Dictionary<Point, int>();

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { ",", " -> ", "," });
                var coordinates = tokens.Select(x => Convert.ToInt32(x)).ToList();

                var x1 = coordinates[0];
                var y1 = coordinates[1];
                var x2 = coordinates[2];
                var y2 = coordinates[3];

                if (x1 == x2) // x values are the same
                {
                    /* This section of code is for situations where the line as noted is right to left or bottom to top */
                    var lowY = Math.Min(y1, y2);
                    var highY = Math.Max(y1, y2);

                    for (int j = lowY; j <= highY; j++)
                    {
                        var point = new Point(x1, j);
                        if (board.ContainsKey(point))
                        {
                            board[point]++;
                        }
                        else
                        {
                            board.Add(point, 1);
                        }
                    }
                }
                else if (y1 == y2) // y values are the same
                {
                    /* This section of code is for situations where the line as noted is right to left or bottom to top */
                    var lowX = Math.Min(x1, x2);
                    var highX = Math.Max(x1, x2);

                    for (int i = lowX; i <= highX; i++)
                    {
                        var point = new Point(i, y1);
                        if (board.ContainsKey(point))
                        {
                            board[point]++;
                        }
                        else
                        {
                            board.Add(point, 1);
                        }
                    }
                }
                else //diagonal, hopefully?
                {
                    var lowX = Math.Min(x1, x2);
                    var highX = Math.Max(x1, x2);
                    var lowY = Math.Min(y1, y2);
                    var highY = Math.Max(y1, y2);

                    for (int i = 0; i <= Math.Abs(highX - lowX); i++)
                    {
                        var x = x1;
                        var y = y1;
                        if (x2 < x1)
                        {
                            x = x - i;
                        }
                        else
                        {
                            x = x + i;
                        }

                        if (y2 < y1)
                        {
                            y = y - i;
                        }
                        else
                        {
                            y = y + i;
                        }

                        var point = new Point(x, y);
                        if (board.ContainsKey(point))
                        {
                            board[point]++;
                        }
                        else
                        {
                            board.Add(point, 1);
                        }
                    }
                }
            }

            var valueOver2 = board.Where(x => x.Value > 1).Count();

            return valueOver2.ToString();
        }
    }
}
