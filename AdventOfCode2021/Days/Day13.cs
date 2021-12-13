using AdventOfCode2021.Models;
using AdventOfCode2021.Models.Day13;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day13
    {
        public static string _sampleInput = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

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
            var dots = new List<Point>();
            var foldLines = new List<FoldLine>();

            ProcessInput(lines, dots, foldLines);

            //PrintBoard(dots);
            dots = FoldLine(foldLines[0], dots);

            //PrintBoard(dots);

            return dots.Count().ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var dots = new List<Point>();
            var foldLines = new List<FoldLine>();

            ProcessInput(lines, dots, foldLines);

            //PrintBoard(dots);
            foreach (var line in foldLines)
            {
                dots = FoldLine(line, dots);
            }

            PrintBoard(dots);

            return dots.Count().ToString();
        }

        #region Private Methods
        internal static List<Point> FoldLine(FoldLine thisLine, List<Point> dots)
        {
            var foldedDots = new List<Point>();

            if (thisLine.XorY == "x")
            {
                foreach(var dot in dots)
                {
                    Point newDot;
                    if (dot.X < thisLine.RowOrColNum)
                    {
                        newDot = dot;
                    }
                    else
                    {
                        var newX = thisLine.RowOrColNum - (dot.X - thisLine.RowOrColNum);
                        newDot = new Point(newX, dot.Y);
                    }
                    if (!foldedDots.Contains(newDot))
                    {
                        foldedDots.Add(newDot);
                    }
                }
            }
            else
            {
                foreach (var dot in dots)
                {
                    Point newDot;
                    if (dot.Y < thisLine.RowOrColNum)
                    {
                        newDot = dot;
                    }
                    else
                    {
                        var newY = thisLine.RowOrColNum - (dot.Y - thisLine.RowOrColNum);
                        newDot = new Point(dot.X, newY);
                    }
                    if (!foldedDots.Contains(newDot))
                    {
                        foldedDots.Add(newDot);
                    }
                }
            }
            return foldedDots;
        }

        internal static void ProcessInput(string[] lines, List<Point> dots, List<FoldLine> foldLines)
        {
            foreach(var line in lines)
            {
                if (line.StartsWith("fold"))
                {
                    var tokens = StringUtils.SplitInOrder(line, new string[] { "fold along ", "=" });
                    var foldLine = new FoldLine(tokens[0], Int32.Parse(tokens[1]));
                    foldLines.Add(foldLine);
                }
                else
                {
                    var tokens = line.Split(new char[] { ',' });
                    dots.Add(new Point(Int32.Parse(tokens[0]), Int32.Parse(tokens[1])));
                }
            }
        }

        internal static void PrintBoard(List<Point> dots)
        {
            var maxDotX = dots.Max(x => x.X);
            var maxDotY = dots.Max(y => y.Y);

            for (int y = 0; y <= maxDotY; y++)
            {
                for (int x = 0; x <= maxDotX; x++)
                {
                    var thisPoint = new Point(x, y);
                    if (dots.Contains(thisPoint))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion

    }
}
