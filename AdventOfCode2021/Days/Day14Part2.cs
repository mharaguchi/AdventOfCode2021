using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day14Part2
    {
        public static string _sampleInput = @"NNCB

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

        const int ITERATIONS = 40;

        //private static Dictionary<char, long> _charCounts = new Dictionary<char, long>();
        private static Dictionary<(string, int), Dictionary<char, long>> dynamicCharCounts = new Dictionary<(string, int), Dictionary<char, long>>();

        public static string Run(string puzzleInput)
        {
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var template = lines[0];

            var pairs = GetPairs(lines);

            //var opsList = new List<(string, int)>();

            //add first triples to list
            //var startOp1 = (RunInsertion(template.Substring(0, 2), pairs), ITERATIONS - 1);
            //var startOp2 = (RunInsertion(template.Substring(1, 2), pairs), ITERATIONS - 1);
            //var startOp3 = (RunInsertion(template.Substring(2, 2), pairs), ITERATIONS - 1);

            //opsList.Add(startOp1);
            //opsList.Add(startOp2);
            //opsList.Add(startOp3);

            var opsList = GetOpsList(template, pairs);

            var frequencyDictionary = RunOperations(opsList, pairs);

            var max = frequencyDictionary.Values.Max();
            var min = frequencyDictionary.Values.Min();

            var diff = max - min;

            return diff.ToString();
        }

        #region Private Methods
        private static List<(string, int)> GetOpsList(string template, Dictionary<string, string> pairs)
        {
            var opsList = new List<(string, int)>();

            for(int i = 0; i < template.Length - 1; i++)
            {
                var triple = RunInsertion(template.Substring(i, 2), pairs);
                opsList.Add((triple, ITERATIONS - 1));
            }

            return opsList;
        }

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

        private static Dictionary<char, long> RunOperations(List<(string, int)> ops, Dictionary<string, string> pairs)
        {
            var totalCharCounts = new Dictionary<char, long>();
            foreach(var op in ops)
            {
                var currentCharCounts = new Dictionary<char, long>();
                currentCharCounts = RunOperation(op, currentCharCounts, pairs);
                MergeDictionaries(currentCharCounts, totalCharCounts);
            }
            AddLetterCount(ops[ops.Count - 1].Item1.Last(), totalCharCounts);

            return totalCharCounts;
        }

        private static void MergeDictionaries(Dictionary<char, long> source, Dictionary<char, long> target)
        {
            foreach(var pair in source)
            {
                if (target.ContainsKey(pair.Key))
                {
                    target[pair.Key] = target[pair.Key] + pair.Value;
                }
                else
                {
                    target.Add(pair.Key, pair.Value);
                }
            }
        }

        private static Dictionary<char, long> RunOperation((string, int) op, Dictionary<char, long> currentCharCounts, Dictionary<string, string> pairs)
        {
            Dictionary<char, long> currentCharCountsCopy = new Dictionary<char, long>(currentCharCounts);

            Console.WriteLine("dynamic dictionary contents: " + dynamicCharCounts.Count);

            if (dynamicCharCounts.ContainsKey(op))
            {
                return dynamicCharCounts[op];
            }

            if (op.Item2 == 0) //add first 2 b/c 3rd is a dupe except for the last token
            {
                AddLetterCount(op.Item1[0], currentCharCountsCopy);
                AddLetterCount(op.Item1[1], currentCharCountsCopy);
            }
            else
            {
                var newIterations = op.Item2 - 1;
                var newOp1 = (RunInsertion(op.Item1.Substring(0, 2), pairs), newIterations); 
                var newOp2 = (RunInsertion(op.Item1.Substring(1, 2), pairs), newIterations);

                var dict1 = new Dictionary<char, long>(RunOperation(newOp1, new Dictionary<char, long>(currentCharCountsCopy), pairs));
                var dict2 = new Dictionary<char, long>(RunOperation(newOp2, new Dictionary<char, long>(currentCharCountsCopy), pairs));

                MergeDictionaries(dict1, dict2);
                currentCharCountsCopy = new Dictionary<char, long>(dict2);
            }

            dynamicCharCounts.Add(op, new Dictionary<char, long>(currentCharCountsCopy));

            return currentCharCountsCopy;
        }

        private static void AddLetterCount(char thisChar, Dictionary<char, long> charCounts)
        {
            if (charCounts.ContainsKey(thisChar))
            {
                charCounts[thisChar] = charCounts[thisChar] + 1;
            }
            else
            {
                charCounts.Add(thisChar, 1);
            }
        }

        //private static string RunInsertions(string template, Dictionary<string, string> pairs, int steps)
        //{
        //    var currentString = template;
        //    for(int i = 0; i < steps; i++)
        //    {
        //        currentString = RunInsertion(currentString, pairs);
        //        Console.WriteLine("step " + (i + 1));
        //        //Console.WriteLine("After step " + i + 1 + ": " + currentString);
        //    }
            
        //    return currentString;
        //}

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
