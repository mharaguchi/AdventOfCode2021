using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day10
    {
        public static string _sampleInput = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

        public static Dictionary<char, char> _closeToOpenMapping = new Dictionary<char, char>()
        {
            {')','(' },
            {'}','{' },
            {']','[' },
            {'>','<' },
        };

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

            var total = (long)0;

            foreach (var line in lines)
            {
                var lineLength = line.Length;

                var stack = new Stack<char>();
                for (int i = 0; i < lineLength; i++)
                {
                    var thisChar = line[i];
                    if (IsOpenCharacter(thisChar))
                    {
                        stack.Push(thisChar);
                    }
                    else
                    {
                        var prevChar = stack.Pop();
                        if (prevChar != _closeToOpenMapping[thisChar])
                        {
                            total += GetIllegalScore(thisChar);
                        }
                    }
                }
            }

            return total.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var scores = new List<long>();

            foreach (var line in lines)
            {
                var lineLength = line.Length;
                var illegal = false;
                var thisLineScore = (long)0;

                var stack = new Stack<char>();
                for (int i = 0; i < lineLength; i++)
                {
                    var thisChar = line[i];
                    if (IsOpenCharacter(thisChar))
                    {
                        stack.Push(thisChar);
                    }
                    else
                    {
                        var prevChar = stack.Pop();
                        if (prevChar != _closeToOpenMapping[thisChar])
                        {
                            illegal = true;
                        }
                    }
                }
                if (!illegal)
                {
                    thisLineScore = GetIncompleteLineScore(stack);
                    scores.Add(thisLineScore);
                }
            }

            scores.Sort();
            var middlePositionNum = scores.Count / 2;
            var middleScore = scores[middlePositionNum];

            return middleScore.ToString();
        }

        #region Private Methods
        private static bool IsOpenCharacter(char c)
        {
            if (c == '(' || c == '{' || c == '<' || c == '[')
            {
                return true;
            }
            return false;
        }

        private static int GetIllegalScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
            }
            throw new Exception("Found illegal character");
        }

        private static long GetIncompleteLineScore(Stack<char> incompleteStack)
        {
            var score = (long)0;
            while(incompleteStack.Count > 0)
            {
                var thisOpenChar = incompleteStack.Pop();
                var thisCharScore = GetIncompleteCharScore(thisOpenChar);
                score *= 5;
                score += thisCharScore;
            }

            return score;
        }

        private static long GetIncompleteCharScore(char c)
        {
            switch (c)
            {
                case '(':
                    return 1;
                case '[':
                    return 2;
                case '{':
                    return 3;
                case '<':
                    return 4;
            }
            throw new Exception("Found illegal character");
        }
        #endregion

    }
}
