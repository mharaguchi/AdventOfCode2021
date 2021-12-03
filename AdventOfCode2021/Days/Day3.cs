using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public static class Day3
    {
        public static string _input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

        public static string Run(string input)
        {
            //return RunPart1(input);
            return RunPart2(input);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var lineCount = lines.Length;
            var lineLength = lines[0].Length;

            var gammaBinaryStrBuilder = new StringBuilder();
            var epsilonBinaryStrBuilder = new StringBuilder();

            for (int i = 0; i < lineLength; i++)
            {
                gammaBinaryStrBuilder.Append(lines.Where(l => l[i] == '0').Count() > lineCount / 2 ? "0" : "1");
                epsilonBinaryStrBuilder.Append(gammaBinaryStrBuilder[i] == '0' ? "1" : "0");
            }

            var gammaBinaryStr = gammaBinaryStrBuilder.ToString();
            var epsilonBinaryStr = epsilonBinaryStrBuilder.ToString();

            var gamma = Convert.ToInt64(gammaBinaryStr, 2);
            var epsilon = Convert.ToInt64(epsilonBinaryStr, 2);

            var product = gamma * epsilon;

            return product.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var product = GetProduct(lines);

            return product.ToString();
        }

        internal static long GetProduct(string[] lines)
        {
            var lineLength = lines[0].Length;

            var possibleOxygenRatings = new List<string>(lines);
            var possibleCo2Ratings = new List<string>(lines);

            for (int i = 0; i < lineLength; i++)
            {
                var thisOxygenRatingValue = possibleOxygenRatings.Where(l => l[i] == '0').Count() > possibleOxygenRatings.Count / 2 ? "0" : "1";
                var thisCo2RatingValue = possibleCo2Ratings.Where(l => l[i] == '1').Count() < (possibleCo2Ratings.Count + possibleCo2Ratings.Count % 2)/ 2 ? "1" : "0";

                if (possibleOxygenRatings.Count > 1) {
                    possibleOxygenRatings = possibleOxygenRatings.Where(l => l[i] == thisOxygenRatingValue[0]).ToList();
                }
                if (possibleCo2Ratings.Count > 1)
                {
                    possibleCo2Ratings = possibleCo2Ratings.Where(l => l[i] == thisCo2RatingValue[0]).ToList();
                }
            }

            var oxygenRating = Convert.ToInt64(possibleOxygenRatings[0], 2);
            var co2Rating = Convert.ToInt64(possibleCo2Ratings[0], 2);

            var product = oxygenRating * co2Rating;

            return product;
        }
    }
}
