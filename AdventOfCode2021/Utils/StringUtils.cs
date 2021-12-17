using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Takes in a string and parses out tokens in order, split by the strings. For instance, 1:3,5 could have splits = [":",","] and return {"1","3","5"}
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splits"></param>
        /// <returns></returns>
        public static List<string> SplitInOrder(string input, string[] splits)
        {
            var tokens = new List<string>();
            var inputTracker = 0;
            var splitsTracker = 0;
            var thisToken = new StringBuilder();
            while (inputTracker < input.Length)
            {
                var remainingString = input.Substring(inputTracker, input.Length - inputTracker);
                if (splitsTracker == splits.Length)
                {
                    tokens.Add(remainingString);
                    PrintTokens(tokens);
                    return tokens;
                }
                if (remainingString.StartsWith(splits[splitsTracker]))
                {
                    inputTracker += splits[splitsTracker].Length;
                    splitsTracker++;
                    if (thisToken.ToString().Length > 0)
                    {
                        tokens.Add(thisToken.ToString());
                    }
                    thisToken = new StringBuilder();
                }
                else
                {
                    thisToken.Append(input[inputTracker]);
                    inputTracker++;
                }
            }
            PrintTokens(tokens);
            return tokens;
        }

        static void PrintTokens(List<string> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine("Token " + i.ToString() + ": " + tokens[i]);
            }
            Console.WriteLine();
        }

        public static List<int> StringArrayToIntList(string[] lines)
        {
            var intList = new List<int>();
            foreach (var line in lines)
            {
                intList.Add(int.Parse(line));
            }
            return intList;
        }

        /// <summary>
        /// Convert a hex string into 4-bit binary, using 0=0000, 1=0001, A=1010, etc
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string HexTo4BitBinaryString(string hexString)
        {
            string binarystring = String.Join(String.Empty,
              hexString.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );

            return binarystring;
        }
    }
}
