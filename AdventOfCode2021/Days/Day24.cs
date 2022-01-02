using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day24
    {
        //        public static string _sampleInput = @"inp w
        //add z w
        //mod z 2
        //div w 2
        //add y w
        //mod y 2
        //div w 2
        //add x w
        //mod x 2
        //div w 2
        //mod w 2";
        public static string _sampleInput = @"inp x
mul x -1";

        public static int w, x, y, z;
        public static int _inputTracker = 0;
        public static List<int> _inputs = new List<int>();

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
            for (var w1 = 9; w1 > 0; w1--)
            {
                long z1 = w1 + 16;
                for (var w2 = 9; w2 > 0; w2--)
                {
                    long z2 = w2 + 11 + z1 * 26;
                    for (var w3 = 9; w3 > 0; w3--)
                    {
                        long z3 = w3 + 12 + z2 * 26;
                        for (var w4 = 9; w4 > 0; w4--)
                        {
                            long z4 = w4 == z3 % 26 - 5 ? z3 / 26 : z3 + w4 + 12;
                            for (var w5 = 9; w5 > 0; w5--)
                            {
                                long z5 = w5 == z4 % 26 - 3 ? z4 / 26 : z4 + w5 + 12;
                                for (var w6 = 9; w6 > 0; w6--)
                                {
                                    long z6 = w6 + 2 + z5 * 26;
                                    for (var w7 = 9; w7 > 0; w7--)
                                    {
                                        long z7 = w7 + 11 + z6 * 26;
                                        for (var w8 = 9; w8 > 0; w8--)
                                        {
                                            long z8 = w8 == z7 % 26 - 16 ? z7 / 26 : z7 + w8 + 4;
                                            for (var w9 = 9; w9 > 0; w9--)
                                            {
                                                long z9 = w9 + 12 + z8 * 26;
                                                for (var w10 = 9; w10 > 0; w10--)
                                                {
                                                    long z10 = w10 + 9 + z9 * 26;
                                                    if (z10 > 26 * 21 * 26 * 26)
                                                    {
                                                        continue;
                                                    }
                                                    for (var w11 = 9; w11 > 0; w11--)
                                                    {
                                                        long z11 = w11 == z10 % 26 - 7 ? z10 / 26 : z10 + w11 + 10;
                                                        if (z11 >= 26 * 21 * 26)
                                                        {
                                                            continue;
                                                        }
                                                        for (var w12 = 9; w12 > 0; w12--)
                                                        {
                                                            long z12 = w12 == z11 % 26 - 11 ? z11 / 26 : z11 + w12 + 11;
                                                            if (z12 >= 26 * 21)
                                                            {
                                                                continue;
                                                            }
                                                            for (var w13 = 9; w13 > 0; w13--)
                                                            {
                                                                long z13 = w13 == z12 % 26 - 6 ? z12 / 26 : z12 + w13 + 6;
                                                                if (z13 < 12 || z13 > 20)
                                                                {
                                                                    continue;
                                                                }
                                                                for (var w14 = 9; w14 > 0; w14--)
                                                                {
                                                                    long z14 = w14 == z13 % 26 - 11 ? z13 / 26 : z13 + w14 + 15;
                                                                    var modelNumber = w1.ToString() + w2.ToString() + w3.ToString() + w4.ToString() + w5.ToString() +
                                                                            w6.ToString() + w7.ToString() + w8.ToString() + w9.ToString() + w10.ToString() +
                                                                            w11.ToString() + w12.ToString() + w13.ToString() + w14.ToString();
                                                                    Console.WriteLine(modelNumber);
                                                                    if (z14 == 0)
                                                                    {
                                                                        return "Solution: " + modelNumber;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            //for(var i = 99999999999999; i > 1111111111111; i--)
            //{
            //w = x = y = z = 0;
            //var inputStr = "99999999999919";
            //var inputStr = "16";
            //if (inputStr.Contains('0'))
            //{
            //    continue;
            //}
            //var w1 = Int32.Parse(inputStr[0].ToString());
            //var w2 = Int32.Parse(inputStr[1].ToString());
            //var w3 = Int32.Parse(inputStr[2].ToString());
            //var w4 = Int32.Parse(inputStr[3].ToString());
            //var w5 = Int32.Parse(inputStr[4].ToString());
            //var w6 = Int32.Parse(inputStr[5].ToString());
            //var w7 = Int32.Parse(inputStr[6].ToString());
            //var w8 = Int32.Parse(inputStr[7].ToString());
            //var w9 = Int32.Parse(inputStr[8].ToString());
            //var w10 = Int32.Parse(inputStr[9].ToString());
            //var w11 = Int32.Parse(inputStr[10].ToString());
            //var w12 = Int32.Parse(inputStr[11].ToString());
            //var w13 = Int32.Parse(inputStr[12].ToString());
            //var w14 = Int32.Parse(inputStr[13].ToString());


            //_inputTracker = 0;
            //_inputs = new List<int>();
            //Console.WriteLine($"Testing model number {@inputStr}");
            //for (var j = 0; j < 14; j++)
            //{
            //    _inputs.Add(Int32.Parse(inputStr[j].ToString()));
            //}
            //foreach (var line in lines)
            //{
            //    ProcessLine(line);
            //}
            //if (z == 0)
            //{
            //    return inputStr;
            //}
        //}

            return "broke";
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            for (var w1 = 1; w1 < 10; w1++)
            {
                long z1 = w1 + 16;
                for (var w2 = 1; w2 < 10; w2++)
                {
                    long z2 = w2 + 11 + z1 * 26;
                    for (var w3 = 1; w3 < 10; w3++)
                    {
                        long z3 = w3 + 12 + z2 * 26;
                        for (var w4 = 1; w4 < 10; w4++)
                        {
                            long z4 = w4 == z3 % 26 - 5 ? z3 / 26 : z3 + w4 + 12;
                            for (var w5 = 1; w5 < 10 ; w5++)
                            {
                                long z5 = w5 == z4 % 26 - 3 ? z4 / 26 : z4 + w5 + 12;
                                for (var w6 = 1; w6 < 10; w6++)
                                {
                                    long z6 = w6 + 2 + z5 * 26;
                                    for (var w7 = 1; w7 < 10; w7++)
                                    {
                                        long z7 = w7 + 11 + z6 * 26;
                                        for (var w8 = 1; w8 < 10; w8++)
                                        {
                                            long z8 = w8 == z7 % 26 - 16 ? z7 / 26 : z7 + w8 + 4;
                                            for (var w9 = 1; w9 < 10; w9++)
                                            {
                                                long z9 = w9 + 12 + z8 * 26;
                                                for (var w10 = 1; w10 < 10; w10++)
                                                {
                                                    long z10 = w10 + 9 + z9 * 26;
                                                    if (z10 > 26 * 21 * 26 * 26)
                                                    {
                                                        continue;
                                                    }
                                                    for (var w11 = 1; w11 < 10; w11++)
                                                    {
                                                        long z11 = w11 == z10 % 26 - 7 ? z10 / 26 : z10 + w11 + 10;
                                                        if (z11 >= 26 * 21 * 26)
                                                        {
                                                            continue;
                                                        }
                                                        for (var w12 = 1; w12 < 10; w12++)
                                                        {
                                                            long z12 = w12 == z11 % 26 - 11 ? z11 / 26 : z11 + w12 + 11;
                                                            if (z12 >= 26 * 21)
                                                            {
                                                                continue;
                                                            }
                                                            for (var w13 = 1; w13 < 10; w13++)
                                                            {
                                                                long z13 = w13 == z12 % 26 - 6 ? z12 / 26 : z12 + w13 + 6;
                                                                if (z13 < 12 || z13 > 20)
                                                                {
                                                                    continue;
                                                                }
                                                                for (var w14 = 1; w14 < 10; w14++)
                                                                {
                                                                    long z14 = w14 == z13 % 26 - 11 ? z13 / 26 : z13 + w14 + 15;
                                                                    var modelNumber = w1.ToString() + w2.ToString() + w3.ToString() + w4.ToString() + w5.ToString() +
                                                                            w6.ToString() + w7.ToString() + w8.ToString() + w9.ToString() + w10.ToString() +
                                                                            w11.ToString() + w12.ToString() + w13.ToString() + w14.ToString();
                                                                    Console.WriteLine(modelNumber);
                                                                    if (z14 == 0)
                                                                    {
                                                                        return "Solution: " + modelNumber;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "broke";
        }

        #region Private Methods
        private static void ProcessLine(string line)
        {
            var tokens = line.Split(' ');

            //if (line == "inp w") {
            //    w = 15;
            //    return;
            //}
            if (line == "inp z")
            {
                z = 5;
                return;
            }
            if (line == "inp x")
            {
                x = 15;
                return;
            }
            //if (line == "inp w")
            //{
            //    w = _inputs[_inputTracker];
            //    _inputTracker++;
            //    return;
            //}
            //if (line == "mul x 0")
            //{
            //    x = 0;
            //    return;
            //}
            //if (line == "add x z")
            //{
            //    x = z;
            //    return;
            //}
            //if (line == "mod x 26")
            //{
            //    x = x % 26;
            //    return;
            //}
            //if (line == "div z 26")
            //{
            //    z = z / 26;
            //    return;
            //}
            //if (line == "div z 1")
            //{
            //    return;
            //}
            //if (line == "eql x w")
            //{
            //    x = x == w ? 1 : 0;
            //    return;
            //}
            //if (line == "mul y 0")
            //{
            //    y = 0;
            //    return;
            //}
            //if (line == "add y 0")
            //{
            //    y = 0;
            //    return;
            //}
            //if (line == "mul y x")
            //{
            //    y = x * y;
            //    return;
            //}
            //if (line == "add y 1")
            //{
            //    y++;
            //    return;
            //}
            //if (line == "mul z y")
            //{
            //    z *= y;
            //    return;
            //}
            //if (line == "add y w")
            //{
            //    y += w;
            //    return;
            //}
            //if (line == "mul x y")
            //{
            //    x *= y;
            //    return;
            //}
            //if (line == "add z y")
            //{
            //    z += y;
            //    return;
            //}
            var op1 = GetVarValue(tokens[1]);
            var op2 = tokens.Length > 2 ? GetVarValue(tokens[2]) : -1;

            int result;

            switch (tokens[0])
            {
                case "inp":
                    SetVarValue(tokens[1], _inputs[_inputTracker]);
                    _inputTracker++;
                    break;
                case "add":
                    result = op1 + op2;
                    SetVarValue(tokens[1], result);
                    break;
                case "mul":
                    result = op1 * op2;
                    SetVarValue(tokens[1], result);
                    break;
                case "div":
                    result = op1 / op2;
                    SetVarValue(tokens[1], result);
                    break;
                case "mod":
                    result = op1 % op2;
                    SetVarValue(tokens[1], result);
                    break;
                case "eql":
                    result = op1 == op2 ? 1 : 0;
                    SetVarValue(tokens[1], result);
                    break;
            }
        }

        private static int GetVarValue(string var)
        {
            return var switch
            {
                "w" => w,
                "x" => x,
                "y" => y,
                "z" => z,
                _ => Int32.Parse(var),
            };
        }
        
        private static void SetVarValue(string varName, int varValue)
        {
            switch (varName)
            {
                case "w":
                    w = varValue;
                    break;
                case "x":
                    x = varValue;
                    break;
                case "y":
                    y = varValue;
                    break;
                case "z":
                    z = varValue;
                    break;
                default:
                    throw new Exception("Invalid var");
            }
        }
        #endregion
    }
}
