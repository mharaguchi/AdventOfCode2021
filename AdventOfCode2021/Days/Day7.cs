using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;

namespace AdventOfCode2021.Days
{
    public static class Day7
    {
        public static string _sampleInput = @"16,1,2,0,4,2,7,1,2,14";

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var initialPositions = FileInputUtils.SplitLineIntoIntList(input, ",");
            var min = initialPositions.Min();
            var max = initialPositions.Max();
            var currentMinFuel = int.MaxValue;

            for(int i = min; i <= max; i++)
            {
                var thisFuel = initialPositions.Select(x => GetFuelCost(x, i)).Sum();
                if (thisFuel < currentMinFuel)
                {
                    currentMinFuel = thisFuel;
                }
            }

            return currentMinFuel.ToString();
        }

        internal static string RunPart2(string input)
        {
            return RunPart1(input);
        }

        #region Private Methods
        private static int GetFuelCost(int pos, int target)
        {
            var diff = Math.Abs(pos - target);
            var total = 0;
            for(int i = 1; i <= diff; i++)
            {
                total += i;
            }
            return total;
        }
        #endregion
    }
}
