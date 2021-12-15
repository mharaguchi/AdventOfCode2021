using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day14
    {
        public static string _sampleInput = @"NN

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var template = lines[0];

            var pairs = GetPairs(lines);

            var polymer = RunInsertions(template, pairs, 10);

            var max = polymer.GroupBy(c => c).Select(c => c.Count()).Max();
            var min = polymer.GroupBy(c => c).Select(c => c.Count()).Min();

            var diff = max - min;

            return diff.ToString();
        }

        internal static string RunPart2(string input)
        {
            return "";
        }

        #region Private Methods
        private static Dictionary<string, string> GetPairs(string[] lines)
        {
            var pairs = new Dictionary<string, string>();
            for (int i = 1; i < lines.Length; i++)
            {
                var tokens = StringUtils.SplitInOrder(lines[i], new string[] { " -> " });
                if (!pairs.ContainsKey(tokens[0]))
                {
                    pairs.Add(tokens[0], tokens[1]);
                }
            }

            return pairs;
        }

        private static string RunInsertions(string template, Dictionary<string, string> pairs, int steps)
        {
            var currentString = template;
            for(int i = 0; i < steps; i++)
            {
                currentString = RunInsertion(currentString, pairs);
                Console.WriteLine("step " + (i + 1));
                //Console.WriteLine("After step " + i + 1 + ": " + currentString);
            }
            
            return currentString;
        }

        private static string RunInsertion(string start, Dictionary<string, string> pairs)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < start.Length - 1; i++)
            {
                sb.Append(start[i]);
                var pair = start[i].ToString() + start[i + 1].ToString();
                sb.Append(pairs[pair]);
            }
            sb.Append(start[^1]);

            return sb.ToString();
        }
        #endregion
    }
}
