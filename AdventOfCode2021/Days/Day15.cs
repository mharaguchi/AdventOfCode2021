using AdventOfCode2021.Utils;
using System.Drawing;

namespace AdventOfCode2021.Days
{
    public static class Day15
    {
        public static string _sampleInput = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

        private static List<List<int>> _board = new List<List<int>>();
        private static Dictionary<Point, long> _lowestRisks = new Dictionary<Point, long>();
        private static long _lowestRisk = long.MaxValue;
        private static Point _endPoint;

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            _board = ProcessInput(input);
            _endPoint = new Point(_board.Count - 1, _board.Count - 1);

            var traversed = new List<Point>();
            var startingPoint = new Point(0, 0);

            var currentSum = (long)0;

            //_lowestRisk = GetInitialPath(startingPoint);
            _lowestRisk = 2000;

            TraversePoint(startingPoint, currentSum, traversed, true);

            return _lowestRisks[_endPoint].ToString();
        }

        internal static string RunPart2(string input)
        {
            return "";
        }

        #region Private Methods
        private static int GetInitialPath(Point startingPoint)
        {
            var currentSum = 0;
            for(int i = 1; i < _board.Count; i++)
            {
                currentSum += _board[0][i];
            }
            for (int i = 1; i < _board.Count; i++)
            {
                currentSum += _board[i][_board.Count - 1];
            }

            return currentSum;
        }

        private static int GetPointValue(Point thisPoint)
        {
            return _board[thisPoint.Y][thisPoint.X];
        }

        private static void TraversePoint(Point thisPoint, long currentSum, List<Point> traversed, bool isStartingPoint)
        {
            Console.WriteLine($"Traversing point ({thisPoint.X}, {thisPoint.Y}), currentSum={currentSum}");
            traversed.Add(thisPoint);

            if (!isStartingPoint)
            {
                currentSum += GetPointValue(thisPoint);
                if (currentSum > _lowestRisk)
                {
                    return;
                }
            }

            if (_lowestRisks.ContainsKey(thisPoint))
            {
                if (currentSum > _lowestRisks[thisPoint])
                {
                    return;
                }
                if (currentSum < _lowestRisks[thisPoint])
                {
                    _lowestRisks[thisPoint] = currentSum;
                }
            }
            else
            {
                _lowestRisks.Add(thisPoint, currentSum);
            }

            if (thisPoint == _endPoint)
            {
                if (currentSum < _lowestRisk)
                {
                    _lowestRisk = currentSum;
                    Console.WriteLine(_lowestRisk);
                }
                return;
            }

            TraverseAdjacent(thisPoint, currentSum, traversed);
        }

        private static void TraverseAdjacent(Point thisPoint, long currentSum, List<Point> traversed)
        {
            var x = thisPoint.X;
            var y = thisPoint.Y;

            var right = new Point(x + 1, y);
            var down = new Point(x, y + 1);
            var up = new Point(x, y - 1);
            var left = new Point(x - 1, y);

            var adjacentPoints = new List<Point>() { right, down, up, left };
            adjacentPoints = adjacentPoints.Where(x => !IsOffBoard(x) && !traversed.Contains(x)).ToList();

            //var adjacentPointsWithValues = new List<(Point, int)> { (right, GetPointValue(right)), (down, GetPointValue(down)), (up, GetPointValue(up)), (left, GetPointValue(up)) };
            //adjacentPoints = adjacentPoints.OrderBy(x => x.Item2).ToList();

            //var adjacentPointsWithValues = adjacentPoints.Select(x => (x, GetPointValue(x))).OrderBy(x => x.Item2).ToList();

            //foreach(var adjacentPoint in adjacentPointsWithValues)
            //{
            //    TraversePoint(adjacentPoint.Item1, currentSum, new List<Point>(traversed), false);
            //}
            
            foreach (var adjacentPoint in adjacentPoints)
            {
                TraversePoint(adjacentPoint, currentSum, new List<Point>(traversed), false);
            }

        }

        private static bool IsOffBoard(Point checkPoint)
        {
            if (checkPoint.X < 0 || checkPoint.Y < 0 || checkPoint.X >= _board.Count || checkPoint.Y >= _board.Count)
            {
                return true;
            };
            return false;
        }

        private static List<List<int>> ProcessInput(string input)
        {
            var board = new List<List<int>>();
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            foreach(var line in lines)
            {
                var thisRow = new List<int>();
                foreach(var thisChar in line){
                    thisRow.Add(Int32.Parse(thisChar.ToString()));
                }
                board.Add(thisRow);
            }

            return board;
        }
        #endregion
    }
}
