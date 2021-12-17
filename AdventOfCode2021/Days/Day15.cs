using AdventOfCode2021.Utils;
using System.Diagnostics;
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

        private static Dictionary<Point, int> _smallestPaths = new Dictionary<Point, int>();

        private static int _currentWorkingMin; //lowest value found so far for the current FindSmallestPathToStart node
        private static int _tracker; //the current level of box 

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
            _lowestRisk = 900;

            _smallestPaths.Add(new Point(0, 1), _board[1][0]);
            _smallestPaths.Add(new Point(1, 0), _board[0][1]);
            _smallestPaths.Add(new Point(1, 1), Math.Min(_board[1][0], _board[1][0]) + _board[1][1]);

            _tracker = 2;
            while(_tracker < _board.Count)
            {
                for (int i = 0; i < _tracker; i++) //starting at the next row, go across
                {
                    var thisPoint = new Point(_tracker, i);
                    var val = FindSmallestPathToStart(thisPoint);
                    //Console.WriteLine("Got lowest: " + val.ToString());
                    _smallestPaths.Add(thisPoint, val);
                }
                for (int i = 0; i < _tracker; i++) //Go from top of next col to node before diagonal
                {
                    var thisPoint = new Point(i, _tracker);
                    var val = FindSmallestPathToStart(thisPoint);
                    //Console.WriteLine("Got lowest: " + val.ToString());
                    _smallestPaths.Add(thisPoint, val);
                }

                var diagonalPoint = new Point(_tracker, _tracker);
                var diagonalVal = FindSmallestPathToStart(diagonalPoint);
                //Console.WriteLine("Got lowest: " + diagonalVal.ToString());
                _smallestPaths.Add(diagonalPoint, diagonalVal);

                _tracker++;
            }

            //for(int i = 2; i < _board.Count; i++)
            //{
            //    var thisPoint = new Point(i, i);
            //    var val = FindSmallestPathToStart(thisPoint);
            //    _smallestPaths.Add(thisPoint, val);
            //}
            //for (int i = 0; i < _board.Count; i++)
            //{
            //    for(int j = 0; j < _board.Count; j++)
            //    {
            //        if (i <= 1 && j <= 1) {
            //            continue;
            //        }
            //        var thisPoint = new Point(i, j);
            //        var val = FindSmallestPathToStart(thisPoint);
            //        _smallestPaths.Add(thisPoint, val);
            //    }
            //}
            //TraversePoint(startingPoint, currentSum, traversed, true);

            return _smallestPaths[_endPoint].ToString();
        }

        internal static string RunPart2(string input)
        {
            return "";
        }

        #region Private Methods
        private static int FindSmallestPathToStart(Point thisPoint)
        {
            //Console.WriteLine();
            Console.WriteLine($"Finding smallest path for ({thisPoint.X},{thisPoint.Y})");
            var stopwatch = new Stopwatch(); 
            stopwatch.Start();
            var baseline = GetBaselinePath(thisPoint);

            _currentWorkingMin = baseline;

            VisitPoint(thisPoint, 0, new List<Point>(), thisPoint);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());
            return _currentWorkingMin;
        }

        private static void VisitPoint(Point thisPoint, int currentSum, List<Point> traversed, Point targetPoint)
        {
            if (thisPoint.X == 0 && thisPoint.Y == 0)
            {
                if (currentSum < _currentWorkingMin)
                {
                    _currentWorkingMin = currentSum;
                    //Console.WriteLine("Found current lowest: " + currentSum);
                }
                return;
            }

            if (_smallestPaths.ContainsKey(thisPoint))
            {
                currentSum += _smallestPaths[thisPoint];
                if (currentSum < _currentWorkingMin)
                {
                    _currentWorkingMin = currentSum;
                    //Console.WriteLine("Found current lowest: " + currentSum);
                }
                //Console.WriteLine($"Search node ({targetPoint.X},{targetPoint.Y}), Visiting point ({thisPoint.X},{thisPoint.Y}), current low={_currentWorkingMin}");
                //Console.WriteLine("Found solution from _smallestPaths, but not lowest: " + currentSum.ToString());
                return;
            }

            currentSum += GetPointValue(thisPoint);
            var minimumPathValue = currentSum + GetMinFromPreviousCompletedLevel(targetPoint);
            //Console.WriteLine($"Search node ({targetPoint.X},{targetPoint.Y}), Visiting point ({thisPoint.X},{thisPoint.Y}), current minimum={minimumPathValue}, current low={_currentWorkingMin}");
            if (minimumPathValue >= _currentWorkingMin)
            {
                //Console.WriteLine("Dead end, returning");
                return;
            }
            
            traversed.Add(thisPoint);

            var x = thisPoint.X;
            var y = thisPoint.Y;

            var right = new Point(x + 1, y);
            var down = new Point(x, y + 1);
            var up = new Point(x, y - 1);
            var left = new Point(x - 1, y);

            var adjacentPoints = new List<Point>() { up, left, right, down };
            adjacentPoints = adjacentPoints.Where(x => !IsOffBoard(x) && !traversed.Contains(x) && GetPointValue(x) + minimumPathValue < _currentWorkingMin).ToList();

            foreach (var adjacentPoint in adjacentPoints)
            {
                //Console.WriteLine($"Trying new path from ({thisPoint.X},{thisPoint.Y}) to ({adjacentPoint.X},{adjacentPoint.Y})");
                VisitPoint(adjacentPoint, currentSum, new List<Point>(traversed), targetPoint);
            }
            //Console.WriteLine($"Done trying paths from ({thisPoint.X},{thisPoint.Y})");
        }

        private static int GetMinFromPreviousCompletedLevel(Point thisPoint)
        {
            var previousCompletedTracker = _tracker - 1;
            
            var minRow = int.MaxValue;
            var minCol = int.MaxValue;

            //Currently assumes working from start outward and diagonal is last completed node
            if (thisPoint.X == thisPoint.Y)
            {
                minRow = _smallestPaths.Where(x => x.Key.Y == thisPoint.Y).Select(x => x.Value).Min();
                minCol = _smallestPaths.Where(x => x.Key.X == thisPoint.X).Select(x => x.Value).Min();
                return Math.Min(minRow, minCol);
            }

            minRow = _smallestPaths.Where(x => x.Key.Y == previousCompletedTracker).Select(x => x.Value).Min();
            minCol = _smallestPaths.Where(x => x.Key.X == previousCompletedTracker).Select(x => x.Value).Min();

            return Math.Min(minRow, minCol);
        }

        private static int GetBaselinePath(Point thisPoint)
        {
            var x = thisPoint.X;
            var y = thisPoint.Y;

            var up = new Point(x, y - 1);
            var left = new Point(x - 1, y);

            if (_smallestPaths.ContainsKey(up))
            {
                return GetPointValue(thisPoint) + _smallestPaths[up];
            }
            if (_smallestPaths.ContainsKey(left))
            {
                return GetPointValue(thisPoint) + _smallestPaths[left];
            }
            //Get a first path value by going straight left and up

            //for (int i = thisPoint.X-1; i >= 0; i--)
            //{
            //    currentSum += GetPointValue(new Point(i, thisPoint.Y));
            //}
            //for (int i = thisPoint.Y-1; i > 0; i--)
            //{
            //    currentSum += GetPointValue(new Point(0, i));
            //}

            //return currentSum;
            throw new Exception("shouldn't get here");
        }

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
