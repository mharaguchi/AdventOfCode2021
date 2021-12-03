using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public static class Day1
    {
        public static string Run(string input)
        {
            //return RunPart1(input);
            return RunPart2(input);
        }

        internal static string RunPart1(string input)
        {
            var nums = FileInputUtils.SplitLinesIntoIntList(input);
            var prev = -1;
            var increasedCount = 0;

            foreach (var num in nums)
            {
                Console.Write(num.ToString());
                if (num > prev && prev >= 0)
                {
                    Console.WriteLine(" (increased)");
                    increasedCount++;
                }
                else
                {
                    Console.WriteLine(" (decreased)");
                }
                prev = num;
            }

            return increasedCount.ToString();
        }

        internal static string RunPart2(string input)
        {
            var nums = FileInputUtils.SplitLinesIntoIntList(input);
            var twoPrev = -1;
            var onePrev = -1;
            var prevSum = -1;
            var increasedCount = 0;

            foreach (var num in nums)
            {
                var thisSum = -1;
                if (onePrev >= 0 && twoPrev >= 0)
                {
                    thisSum = num + onePrev + twoPrev;
                    Console.Write(thisSum.ToString());
                    if (thisSum > prevSum && prevSum != -1)
                    {
                        Console.WriteLine(" (increased)");
                        increasedCount++;
                    }
                    else
                    {
                        if (prevSum == -1)
                        {
                            Console.WriteLine(" (N/A - no previous sum)");
                        }
                        else
                        {
                            Console.WriteLine(" (decreased)");
                        }
                    }
                }
                twoPrev = onePrev;
                onePrev = num;
                prevSum = thisSum;
            }

            return increasedCount.ToString();
        }

    }
}
