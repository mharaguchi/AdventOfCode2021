using AdventOfCode2021.Utils;
using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode2021.Days
{
    public static class Day15Try2
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
        private static Dictionary<Point, int> _distancesFromStart = new Dictionary<Point, int>();
        private static List<Point> _unvisitedPoints = new List<Point>();

        private static Point _endPoint;

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            _board = ProcessInput(input);
            _endPoint = new Point(_board.Count - 1, _board.Count - 1);

            var traversed = new List<Point>();
            var startingPoint = new Point(0, 0);

            Initialize(_board.Count);

            while (_unvisitedPoints.Count > 0)
            {
                Console.WriteLine("Remaining nodes: " + _unvisitedPoints.Count);

                var minPoint = _unvisitedPoints.Aggregate((point1, point2) => GetCurrentPointScore(point1) < GetCurrentPointScore(point2) ? point1 : point2);
                VisitPoint(minPoint);
            }

            return _distancesFromStart[_endPoint].ToString();
        }

        internal static string RunPart2(string input)
        {
            var boardTile = new List<List<int>>();
            boardTile = ProcessInput(input);
            _board = GenerateBoard(boardTile);
            //PrintBoard(_board);

            _endPoint = new Point(_board.Count - 1, _board.Count - 1);

            var traversed = new List<Point>();
            var startingPoint = new Point(0, 0);

            Initialize(_board.Count);

            while (_unvisitedPoints.Count > 0)
            {
                if (_unvisitedPoints.Count % 1000 == 0)
                {
                    Console.WriteLine("Remaining nodes: " + _unvisitedPoints.Count);
                }
                var minPoint = _unvisitedPoints.Aggregate((point1, point2) => GetCurrentPointScore(point1) < GetCurrentPointScore(point2) ? point1 : point2);
                VisitPoint(minPoint);
            }

            return _distancesFromStart[_endPoint].ToString();
        }

        #region Private Methods
        private static List<List<int>> GenerateBoard(List<List<int>> boardTile)
        {
            var board = new List<List<int>>(boardTile);

            //Copy board tile across horizontally 4 times
            for (int dupeNum = 1; dupeNum <= 4; dupeNum++)
            {
                for (int i = 0; i < boardTile.Count; i++)
                {
                    for (int j = 0; j < boardTile.Count; j++)
                    {
                        var nextNum = boardTile[i][j] + dupeNum;
                        nextNum = nextNum <= 9 ? nextNum : nextNum % 9;
                        board[i].Add(nextNum);
                    }
                }
            }

            //Duplicate rows downward
            var fullBoardLength = board[0].Count;
            for (int dupeNum = 1; dupeNum <= 4; dupeNum++)
            {
                for (int i = 0; i < boardTile.Count; i++)
                {
                    var row = new List<int>();
                    for (int j = 0; j < fullBoardLength; j++)
                    {
                        var nextNum = board[i][j] + dupeNum;
                        nextNum = nextNum <= 9 ? nextNum : nextNum % 9;
                        row.Add(nextNum);
                    }
                    board.Add(row);
                }
            }

            return board;
        }

        private static void Initialize(int sideLength)
        {
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    var thisPoint = new Point(i, j);
                    _unvisitedPoints.Add(thisPoint);

                    if (i == 0 && j == 0)
                    {
                        _distancesFromStart.Add(thisPoint, 0); //initialize starting point to be 0 dist
                    }
                    else
                    {
                        _distancesFromStart.Add(thisPoint, int.MaxValue); //initialize starting point to be 0 dist
                    }
                }
            }
        }

        private static void VisitPoint(Point thisPoint)
        {
            _unvisitedPoints.Remove(thisPoint);
            var x = thisPoint.X;
            var y = thisPoint.Y;

            var right = new Point(x + 1, y);
            var down = new Point(x, y + 1);
            var up = new Point(x, y - 1);
            var left = new Point(x - 1, y);

            var adjacentPoints = new List<Point>() { up, left, right, down };
            adjacentPoints = adjacentPoints.Where(x => !IsOffBoard(x) && _unvisitedPoints.Contains(x)).ToList();

            var dist = GetCurrentPointScore(thisPoint);
            foreach (var adjacentPoint in adjacentPoints)
            {
                var testDist = dist + GetPointValue(adjacentPoint);
                if (testDist < GetCurrentPointScore(adjacentPoint))
                {
                    _distancesFromStart[adjacentPoint] = testDist;
                }
            }
        }

        private static int GetCurrentPointScore(Point thisPoint)
        {
            return _distancesFromStart[thisPoint];
        }

        private static int GetPointValue(Point thisPoint)
        {
            return _board[thisPoint.Y][thisPoint.X];
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

            foreach (var line in lines)
            {
                var thisRow = new List<int>();
                foreach (var thisChar in line)
                {
                    thisRow.Add(Int32.Parse(thisChar.ToString()));
                }
                board.Add(thisRow);
            }

            return board;
        }

        private static void PrintBoard(List<List<int>> board)
        {
            foreach (var boardLine in board)
            {
                foreach (int point in boardLine)
                {
                    Console.Write(point);
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
