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

        public static string Run(string puzzleInput)
        {
            return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var template = lines[0];

            var pairs = GetPairs(lines);

            var opStack = new Stack<(string, int)>();

            //add first 3 triples to stack
            var startOp1 = (RunInsertion(template.Substring(0, 2), pairs), ITERATIONS - 1);
            var startOp2 = (RunInsertion(template.Substring(1, 2), pairs), ITERATIONS - 1);
            var startOp3 = (RunInsertion(template.Substring(2, 2), pairs), ITERATIONS - 1);

            opStack.Push(startOp3);
            opStack.Push(startOp2);
            opStack.Push(startOp1);

            var frequencyDictionary = RunOperations(opStack, pairs);

            var max = frequencyDictionary.Values.Max();
            var min = frequencyDictionary.Values.Min();

            //var max = polymer.GroupBy(c => c).Select(c => c.Count()).Max();
            //var min = polymer.GroupBy(c => c).Select(c => c.Count()).Min();

            var diff = max - min;

            return diff.ToString();
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

        private static Dictionary<char, long> RunOperations(Stack<(string, int)> ops, Dictionary<string, string> pairs)
        {
            var charCounts = new Dictionary<char, long>();
            while (ops.Count > 0)
            {
                RunOperation(ops, pairs, charCounts, ops.Count == 1);
                Console.WriteLine("ops count: " + ops.Count());
            }
            return charCounts;
        }

        private static void RunOperation(Stack<(string, int)> ops, Dictionary<string, string> pairs, Dictionary<char, long> charCounts, bool isLastOp)
        {
            var op = ops.Pop();
            if (isLastOp && op.Item2 == 0) //last op, add all 3 letters
            {
                AddLetterCount(op.Item1[0], charCounts);
                AddLetterCount(op.Item1[1], charCounts);
                AddLetterCount(op.Item1[2], charCounts);
            }
            else if (op.Item2 == 0) //not last op, should add first 2 b/c 3rd is a dupe
            {
                AddLetterCount(op.Item1[0], charCounts);
                AddLetterCount(op.Item1[1], charCounts);
            }
            else
            {
                var newIterations = op.Item2 - 1;
                var newOp1 = (RunInsertion(op.Item1.Substring(0, 2), pairs), newIterations); //Add 2 new triples to stack
                var newOp2 = (RunInsertion(op.Item1.Substring(1, 2), pairs), newIterations);

                ops.Push(newOp2);
                ops.Push(newOp1);
            }
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
