using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    public static class Day2
    {
        public static string _input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

        public static string Run(string input)
        {
            //return RunPart1(input);
            return RunPart2(input);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var horizPos = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " " });
                var command = tokens[0];
                var dist = Int32.Parse(tokens[1]);
                switch (command)
                {
                    case "forward":
                        horizPos += dist; break;
                    case "up":
                        depth -= dist; break;
                    case "down":
                        depth += dist; break;
                }
            }

            var product = horizPos * depth;

            return product.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var aim = 0;
            var horizPos = 0;
            var depth = 0;

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " " });
                var command = tokens[0];
                var dist = Int32.Parse(tokens[1]);
                switch (command)
                {
                    case "forward":
                        horizPos += dist;
                        depth += aim * dist;
                        break;
                    case "up":
                        aim -= dist;
                        break;
                    case "down":
                        aim += dist; 
                        break;
                }
            }

            var product = horizPos * depth;

            return product.ToString();
        }
    }
}
