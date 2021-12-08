using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day8
    {
        //public static string _sampleInput = @"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";

        public static string _sampleInput = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        /* Signal Patterns */
        /*
         * 0: abcefg
         * 1: cf
         * 2: acdeg
         * 3: acdfg
         * 4: bcdf
         * 5: abdfg
         * 6: abdefg
         * 7: acf
         * 8: abcdefg
         * 9: abcdfg
         * 
         */
        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var outputTokens = new List<string>();

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " | ", " ", " ", " " });
                outputTokens.AddRange(tokens.Skip(10).Take(4));
            }

            var matches = outputTokens.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).Count();

            return matches.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var sum = (long)0;

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " | ", " ", " ", " " });
                var inputTokens = new List<string>();
                var outputTokens = new List<string>();

                var frequencyDict = new Dictionary<char, int>();

                inputTokens.AddRange(tokens.Take(10));
                outputTokens.AddRange(tokens.Skip(10).Take(4));

                for (char c = 'a'; c <= 'g'; c++)
                {
                    var freq = inputTokens.Where(x => x.Contains(c)).Count();
                    frequencyDict.Add(c, freq);
                }

                sum += GetOutputValue(frequencyDict, inputTokens, outputTokens);
            }

            return sum.ToString();
        }

        #region Private Methods
        private static Dictionary<char, List<char>> InitializeValues()
        {
            var initialValues = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            var initial = new Dictionary<char, List<char>>();
            for (char c = 'a'; c <= 'g'; c++)
            {
                initial.Add(c, initialValues);
            }
            return initial;
        }

        private static string SortString(string input)
        {
            char[] characters = input.ToCharArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private static int GetOutputValue(Dictionary<char, int> frequencyMapping, List<string> inputTokens, List<string> outputTokens)
        {
            var wireMapping = GetWireMapping(frequencyMapping, inputTokens); //mapping wires to segments

            var outputStringBuilder = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                var thisDigit = DecodeOutputTokenValue(wireMapping, outputTokens[i]);
                outputStringBuilder.Append(thisDigit.ToString());
            }

            return Convert.ToInt32(outputStringBuilder.ToString());
        }

        private static Dictionary<char, char> GetWireMapping(Dictionary<char, int> frequencyMapping, List<string> inputTokens)
        {
            var wireMapping = new Dictionary<char, char>(); //mapping segments to wires

            var e = frequencyMapping.Where(x => x.Value == 4).Select(x => x.Key).FirstOrDefault();
            var b = frequencyMapping.Where(x => x.Value == 6).Select(x => x.Key).FirstOrDefault();
            var f = frequencyMapping.Where(x => x.Value == 9).Select(x => x.Key).FirstOrDefault();

            var strLen2 = inputTokens.Where(x => x.Length == 2).FirstOrDefault();
            var strLen3 = inputTokens.Where(x => x.Length == 3).FirstOrDefault();
            var a = strLen3.ToCharArray().Except(strLen2.ToCharArray()).FirstOrDefault();

            var c = frequencyMapping.Where(x => x.Value == 8 && x.Key != a).Select(x => x.Key).FirstOrDefault();

            var strLen4 = inputTokens.Where(x => x.Length == 4).FirstOrDefault();
            var d = frequencyMapping.Where(x => x.Value == 7 && strLen4.Contains(x.Key)).Select(x => x.Key).FirstOrDefault();

            var g = frequencyMapping.Keys.Where(x => x != a && x != b && x != c && x != d && x != e && x != f).FirstOrDefault();

            wireMapping.Add(a, 'a');
            wireMapping.Add(b, 'b');
            wireMapping.Add(c, 'c');
            wireMapping.Add(d, 'd');
            wireMapping.Add(e, 'e');
            wireMapping.Add(f, 'f');
            wireMapping.Add(g, 'g');

            return wireMapping;
        }

        private static int DecodeOutputTokenValue(Dictionary<char, char> wireMapping, string outputToken)
        {
            var translatedValue = new char[outputToken.Length];
            for (int i = 0; i < translatedValue.Length; i++)
            {
                translatedValue[i] = wireMapping[outputToken[i]];
            }
            var translatedString = new string(translatedValue);
            var sortedNumberValue = SortString(translatedString);

            return GetIntFromString(sortedNumberValue);
        }

        private static int GetIntFromString(string inputSegmentValue)
        {
            switch (inputSegmentValue)
            {
                case "abcefg":
                    return 0;
                case "cf":
                    return 1;
                case "acdeg":
                    return 2;
                case "acdfg":
                    return 3;
                case "bcdf":
                    return 4;
                case "abdfg":
                    return 5;
                case "abdefg":
                    return 6;
                case "acf": 
                    return 7;
                case "abcdefg":
                    return 8;
                case "abcdfg": 
                    return 9;
                default:
                    throw new Exception("Should not get here");
            }
        }
        #endregion

/*
 *          * 0: abcefg
 * 1: cf
 * 2: acdeg
 * 3: acdfg
 * 4: bcdf
 * 5: abdfg
 * 6: abdefg
 * 7: acf
 * 8: abcdefg
 * 9: abcdfg

a:8
b:6
c:8
d:7
e:4
f:9
g:7

Look for 4 freq = 'e'
Look for 6 freq = 'b'
Look for 9 freq = 'f'
Look for 2-digit, compare to 3-digit, extra digit is 'a'
Look for 8 freq not a = 'c'
Look for 4-digit, 7 freq = 'd'
Last one = 'g'
*/

}
}
