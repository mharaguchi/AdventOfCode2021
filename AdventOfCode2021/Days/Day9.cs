using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day9
    {
        public static string _sampleInput = @"2199943210
3987894921
9856789892
8767896789
9899965678";

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

            var numLines = lines.Length;
            var lineLength = lines[0].Length;

            var lowPoints = new List<int>();

            for (int y = 0; y < numLines; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    if (IsLowPoint(x, y, lines))
                    {
                        lowPoints.Add(int.Parse(lines[y][x].ToString()));
                    }
                }
            }

            var total = lowPoints.Sum() + lowPoints.Count();

            return total.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var lowPoints = GetLowPoints(lines);
            var basins = new Dictionary<Point, List<Point>>();
            //var pointsInMultipleBasins = new List<Point>();

            foreach(var lowPoint in lowPoints)
            {
                var pointsInBasin = new List<Point>();
                TraverseBasin(lowPoint, lines, pointsInBasin);
                basins.Add(lowPoint, pointsInBasin);
            }

            var top3 = basins.OrderByDescending(x => x.Value.Count).Take(3).Select(x => x.Value.Count);

            var product = top3.Aggregate(1, (x, y) => x * y);

            return product.ToString();        
        }

        #region Private Methods
        internal static void TraverseBasin(Point curPos, string[] lines, List<Point> pointsInBasin)
        {
            //Add current point if not already added
            if (!pointsInBasin.Contains(curPos))
            {
                pointsInBasin.Add(curPos);
            }

            //Traverse adjacent

            var maxY = lines.Length - 1;
            var maxX = lines[0].Length - 1;

            //Check above
            if (curPos.Y != 0)
            {
                var nextPos = new Point(curPos.X, curPos.Y - 1);
                if (lines[nextPos.Y][nextPos.X] != '9' && !pointsInBasin.Contains(nextPos))
                {
                    TraverseBasin(nextPos, lines, pointsInBasin);
                }
            }

            //check below
            if (curPos.Y != maxY)
            {
                var nextPos = new Point(curPos.X, curPos.Y + 1);
                if (lines[nextPos.Y][nextPos.X] != '9' && !pointsInBasin.Contains(nextPos))
                {
                    TraverseBasin(nextPos, lines, pointsInBasin);
                }
            }

            //check left
            if (curPos.X != 0)
            {
                var nextPos = new Point(curPos.X - 1, curPos.Y);
                if (lines[nextPos.Y][nextPos.X] != '9' && !pointsInBasin.Contains(nextPos))
                {
                    TraverseBasin(nextPos, lines, pointsInBasin);
                }
            }

            //check right
            if (curPos.X != maxX)
            {
                var nextPos = new Point(curPos.X + 1, curPos.Y);
                if (lines[nextPos.Y][nextPos.X] != '9' && !pointsInBasin.Contains(nextPos))
                {
                    TraverseBasin(nextPos, lines, pointsInBasin);
                }
            }
        }

        internal static List<Point> GetLowPoints(string[] lines)
        {
            var numLines = lines.Length;
            var lineLength = lines[0].Length;

            var lowPoints = new List<Point>();

            for (int y = 0; y < numLines; y++)
            {
                for (int x = 0; x < lineLength; x++)
                {
                    if (IsLowPoint(x, y, lines))
                    {
                        lowPoints.Add(new Point(x, y));
                    }
                }
            }

            return lowPoints;
        }

        internal static bool IsLowPoint(int x, int y, string[] lines)
        {
            var curVal = lines[y][x];

            //Check above
            if (y != 0)
            {
                if (curVal >= lines[y - 1][x])
                {
                    return false;
                }
            }

            //check below
            if (y != lines.Length - 1)
            {
                if (curVal >= lines[y + 1][x])
                {
                    return false;
                }
            }

            //check left
            if (x != 0)
            {
                if (curVal >= lines[y][x-1])
                {
                    return false;
                }
            }

            //check right
            if (x != lines[0].Length - 1)
            {
                if (curVal >= lines[y][x + 1])
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

    }
}
