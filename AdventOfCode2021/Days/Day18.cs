using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day18
    {
        public static string _sampleInput = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";

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

            var currentSnailfishSum = lines[0];
            for (int i = 1; i < lines.Length; i++)
            {
                currentSnailfishSum = AddSnailfishNums(currentSnailfishSum, lines[i]);
                currentSnailfishSum = ReduceSnailfishNum(currentSnailfishSum).ToString();
            }

            return CalculateMagnitude(currentSnailfishSum).ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i+1; j < lines.Length; j++)
                {
                    var added = AddSnailfishNums(lines[i], lines[j]);
                    var reduced = ReduceSnailfishNum(added);
                    var result = CalculateMagnitude(reduced);
                    if (result > max)
                    {
                        max = result;
                    }

                    added = AddSnailfishNums(lines[j], lines[i]);
                    reduced = ReduceSnailfishNum(added);
                    result = CalculateMagnitude(reduced);
                    if (result > max)
                    {
                        max = result;
                    }
                }
            }

            return max.ToString();
        }

        #region Private Methods
        internal static string AddSnailfishNums(string input1, string input2)
        {
            var result = input1.Insert(0, "[");
            result = result.Insert(result.Length, "," + input2 + "]");

            return result;
        }

        internal static string ReduceSnailfishNum(string snailfishNum)
        {
            var changed = true;
            var changedString = snailfishNum;
            while (changed)
            {

                changed = false;
                var depth = 0;
                var numLength = changedString.Length;
                for (int i = 0; i < numLength; i++)
                {
                    if (changedString[i] == '[')
                    {
                        depth++;
                    }
                    if (changedString[i] == ']')
                    {
                        depth--;
                    }
                    if (depth == 5)
                    {
                        changedString = ExplodePair(changedString, i);
                        Console.WriteLine(changedString);
                        changed = true;
                        break;
                    }
                }
                if (!changed)
                {
                    var splitLoc = GetSplitLocation(changedString);
                    if (splitLoc > -1)
                    {
                        changedString = SplitNumber(changedString, splitLoc);
                        Console.WriteLine(changedString);
                        changed = true;
                    }
                }
            }

            return changedString;
        }

        internal static string SplitNumber(string snailfishNum, int loc)
        {
            var numLen = 0;
            var tracker = loc;

            while (Char.IsDigit(snailfishNum[tracker])){
                numLen++;
                tracker++;
            }

            var numStr = snailfishNum.Substring(loc, numLen);
            var num = Int32.Parse(numStr);
            var leftNum = num / 2;
            var rightNum = num / 2 + num % 2;

            var newPair = $"[{leftNum.ToString()},{rightNum.ToString()}]";

            var newSnailfishNum = snailfishNum.Remove(loc, numLen).Insert(loc, newPair);

            return newSnailfishNum;
        }

        internal static int GetSplitLocation(string snailfishNum)
        {
            var loc = -1;
            var depth = 0;
            for (int i = 0; i < snailfishNum.Length; i++)
            {
                if (Char.IsDigit(snailfishNum[i]))
                {
                    depth++;
                }
                else
                {
                    depth = depth > 1 ? depth-- : 0;
                }

                if (depth == 2)
                {
                    loc = i - 1;
                    break;                }
            }

            return loc;
        }

        internal static string ExplodePair(string snailfishNum, int loc)
        {
            var pairToExplode = StringUtils.GetExpression(snailfishNum[loc..], '[', ']');
            var result = snailfishNum.Remove(loc, pairToExplode.Length).Insert(loc, "0");

            var tokens = StringUtils.SplitInOrder(pairToExplode, new string[] { "[", ",", "]" });
            var leftExplode = Int32.Parse(tokens[0]);
            var rightExplode = Int32.Parse(tokens[1]);

            var prevLength = result.Length;
            result = ApplyLeftExplode(loc, result, leftExplode);
            var diff = result.Length - prevLength;
            loc += diff;
            result = ApplyRightExplode(loc, result, rightExplode);

            return result;
        }

        private static string ApplyLeftExplode(int loc, string snailfishNum, int leftExplode)
        {
            var tracker = loc;
            var found = false;
            var len = 0;
            while (tracker > 0 && !found)
            {
                tracker--;
                if (Char.IsDigit(snailfishNum[tracker]))
                {
                    while (Char.IsDigit(snailfishNum[tracker]))
                    {
                        tracker--;
                        len++;
                    }
                    found = true;
                }
            }

            if (found)
            {
                var leftLoc = tracker + 1;
                var leftLen = len;
                var left = Int32.Parse(snailfishNum.Substring(leftLoc, leftLen));
                left = left + leftExplode;
                snailfishNum = snailfishNum.Remove(leftLoc, leftLen).Insert(leftLoc, left.ToString());
            }

            return snailfishNum;
        }

        private static string ApplyRightExplode(int loc, string snailfishNum, int rightExplode)
        {
            var tracker = loc;
            var found = false;
            var len = 0;
            var start = 0;
            while (tracker < snailfishNum.Length - 1 && !found)
            {
                tracker++;
                if (Char.IsDigit(snailfishNum[tracker]))
                {
                    start = tracker;
                    while (Char.IsDigit(snailfishNum[tracker]))
                    {
                        tracker++;
                        len++;
                    }
                    found = true;
                }
            }

            if (found)
            {
                var rightLoc = start;
                var rightLen = len;
                var right = Int32.Parse(snailfishNum.Substring(rightLoc, rightLen));
                right = right + rightExplode;
                snailfishNum = snailfishNum.Remove(rightLoc, rightLen).Insert(rightLoc, right.ToString());
            }

            return snailfishNum;
        }

        /// <summary>
        /// Calculate magnitude of an already-reduced snailfish number (no double digit numbers allowed)
        /// </summary>
        /// <param name="snailfishNum"></param>
        /// <returns></returns>
        internal static int CalculateMagnitude(string snailfishNum)
        {
            var tracker = 1;
            int leftOperand = 0;
            int rightOperand = 0;

            if (snailfishNum[tracker] == '[')
            {
                string fullLeftSnailfishNum = StringUtils.GetExpression(snailfishNum[tracker..], '[', ']');
                leftOperand = CalculateMagnitude(fullLeftSnailfishNum);
                tracker += fullLeftSnailfishNum.Length + 1; //advance tracker past snailfishnum and comma
            }
            else
            {
                leftOperand = Int32.Parse(snailfishNum[tracker].ToString());
                tracker += 2; //advance tracker past comma
            }

            if (snailfishNum[tracker] == '[')
            {
                string fullRightSnailfishNum = StringUtils.GetExpression(snailfishNum[tracker..], '[', ']');
                rightOperand = CalculateMagnitude(fullRightSnailfishNum);
                tracker += fullRightSnailfishNum.Length + 1; //advance tracker past snailfishnum and comma
            }
            else
            {
                rightOperand = Int32.Parse(snailfishNum[tracker].ToString());
                tracker += 2; //advance tracker past comma
            }

            return leftOperand * 3 + rightOperand * 2;
        }
        #endregion
    }
}
