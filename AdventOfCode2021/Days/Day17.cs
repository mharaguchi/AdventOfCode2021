using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day17
    {
        public static string _sampleInput = @"target area: x=20..30, y=-10..-5";

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
            var tokens = StringUtils.SplitInOrder(lines[0], new string[] { "target area: x=", "..", ", y=", ".." });

            var targetMinX = Int32.Parse(tokens[0]);
            var targetMaxX = Int32.Parse(tokens[1]);
            var targetMinY = Int32.Parse(tokens[2]);
            var targetMaxY = Int32.Parse(tokens[3]);

            var minXVelocity = GetMinXVelocity(targetMinX);
            var maxYVelocity = GetMaxYVelocity(minXVelocity, targetMinY, targetMaxY);
            var maxYHeight = GetMaxYHeight(minXVelocity, maxYVelocity);

            return maxYHeight.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var tokens = StringUtils.SplitInOrder(lines[0], new string[] { "target area: x=", "..", ", y=", ".." });

            var targetMinX = Int32.Parse(tokens[0]);
            var targetMaxX = Int32.Parse(tokens[1]);
            var targetMinY = Int32.Parse(tokens[2]);
            var targetMaxY = Int32.Parse(tokens[3]);

            var minXVelocity = GetMinXVelocity(targetMinX);
            var maxYVelocity = GetMaxYVelocity(minXVelocity, targetMinY, targetMaxY);

            var count = 0;

            for(int x = minXVelocity; x <= targetMaxX; x++)
            {
                for(int y = targetMinY; y <= maxYVelocity; y++)
                {
                    if (WillEventuallyHitTarget(x, y, targetMinX, targetMaxX, targetMinY, targetMaxY))
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }

        #region Private Methods
        internal static bool WillEventuallyHitTarget(int xVelocity, int yVelocity, int targetMinX, int targetMaxX, int targetMinY, int targetMaxY)
        {
            var xLoc = 0;
            var yLoc = 0;

            var xVel = xVelocity;
            var yVel = yVelocity;

            while(xLoc < targetMaxX && yLoc > targetMinY)
            {
                xLoc += xVel;
                yLoc += yVel;
                if (xVel > 0) {
                    xVel--;
                }
                yVel--;
                if (xLoc >= targetMinX && xLoc <= targetMaxX && yLoc >= targetMinY && yLoc <= targetMaxY)
                {
                    //Console.WriteLine(xVelocity.ToString() + "," + yVelocity.ToString());
                    return true;
                }
            }
            return false;
        }

        internal static int GetMaxYHeight(int xVelocity, int yVelocity)
        {
            var yLoc = 0;
            while (yVelocity > 0)
            {
                yLoc += yVelocity;
                yVelocity--;
            }

            return yLoc;
        }

        internal static int GetMaxYVelocity(int minXVelocity, int targetMinY, int targetMaxY)
        {

            var maxYVelocity = 0;
            for (int i = 0; i < 20000; i++)
            {
                /* Get first y coord when x velocity is 0 */
                var yLoc = 0;
                var yVelocity = i;
                for (int j = minXVelocity; j > 0; j--)
                {
                    yLoc = yLoc + yVelocity;
                    yVelocity--;
                }

                if (WillEventuallyHitTargetVertically(yLoc, yVelocity, targetMinY, targetMaxY))
                {
                    maxYVelocity = i;
                }
            }

            return maxYVelocity;
        }

        internal static bool WillEventuallyHitTargetVertically(int startLoc, int startVelocity, int targetMinY, int targetMaxY)
        {
            var loc = startLoc;
            var velocity = startVelocity;
            while(loc > targetMinY)
            {
                loc += velocity;
                if (loc >= targetMinY && loc <= targetMaxY)
                {
                    return true;
                }
                velocity--;
            }
            return false;
        }

        internal static int GetMinXVelocity(int targetMinX)
        {
            var found = false;
            var tracker = 0;
            while (!found)
            {
                tracker++;
                var velocity = tracker;
                var total = 0;
                for(int i = tracker; i > 0; i--)
                {
                    total += i;
                }
                if (total >= targetMinX)
                {
                    found = true;
                }
            }
            return tracker;
        }
        #endregion
    }
}
