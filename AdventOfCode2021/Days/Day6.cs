using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;

namespace AdventOfCode2021.Days
{
    public static class Day6
    {
        public static string _sampleInput = @"3,4,3,1,2";
        private const int NUM_DAYS = 256;
        private const int NEW_FISHY_VALUE = 8;
        private const int RESET_FISHY_VALUE = 6;

        private static Dictionary<int, long> _fishies = new Dictionary<int, long>();

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var initialFishies = FileInputUtils.SplitLineIntoIntList(input, ",");

            for(int i = 0; i <= NEW_FISHY_VALUE; i++)
            {
                _fishies[i] = initialFishies.Where(x => x == i).Count();
            }

            for (int i = 0; i < NUM_DAYS; i++)
            {
                ProcessDay(i);
            }

            return _fishies.Values.Sum().ToString();
        }

        internal static string RunPart2(string input)
        {
            return RunPart1(input);
        }

        #region Private Methods
        internal static void ProcessDay(int day)
        {
            var numNewFishies = _fishies[0];
            for(int i = 0; i <= NEW_FISHY_VALUE - 1; i++)
            {
                _fishies[i] = _fishies[i + 1];
            }
            _fishies[NEW_FISHY_VALUE] = numNewFishies;
            _fishies[RESET_FISHY_VALUE] += numNewFishies;
            PrintValues(day);
        }

        internal static void PrintValues(int day)
        {
            Console.Write("After " + day + " days:");
            for (int i = 0; i <= NEW_FISHY_VALUE; i++)
            {
                Console.Write(i + "(" + _fishies[i] + ")" + " ");
            }
            Console.WriteLine();
        }
        #endregion
    }
}
