using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day16
    {
        public static string _sampleInput = @"9C0141080250320F1802104A08";

        /* Sample Inputs
         * 8A004A801A8002F478 //16 version total
         * 620080001611562C8802118E34 //12 version total
         * C0015000016115A2E0802F182340 //23
         * A0016C880162017C3686B18A3D4780 //31
         */


        private static List<int> _versionNumbers = new List<int>();

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
            var binaryString = StringUtils.HexTo4BitBinaryString(lines[0]);
            Console.WriteLine(binaryString);

            ParsePacket(binaryString, 0);

            return _versionNumbers.Sum().ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var binaryString = StringUtils.HexTo4BitBinaryString(lines[0]);
            Console.WriteLine(binaryString);

            var result = ParsePacketPart2(binaryString, 0);

            return result.Item2.ToString();
        }

        #region Private Methods
        internal static (int, long) ParsePacketPart2(string binaryString, int startLoc)
        {
            var tracker = startLoc;

            var version = Convert.ToInt32(binaryString.Substring(tracker, 3), 2);
            tracker += 3;
            _versionNumbers.Add(version);

            var type = Convert.ToInt32(binaryString.Substring(tracker, 3), 2);
            tracker += 3;
            if (type == 4)
            {
                var literal = GetLiteral(binaryString, tracker);
                tracker += literal.Item1;
                return (tracker, literal.Item2); //(end location, literal value)
            }

            var lengthTypeId = binaryString[tracker];
            tracker++;
            if (lengthTypeId == '0')
            {
                var subpacketLength = Convert.ToInt32(binaryString.Substring(tracker, 15), 2);
                tracker += 15;
                var complete = tracker + subpacketLength;
                var subValues = new List<long>();
                while (tracker != complete)
                {
                    var result = ParsePacketPart2(binaryString, tracker);
                    tracker = result.Item1; //end location
                    subValues.Add(result.Item2);
                }
                var value = PerformOperation(subValues, type);
                return (tracker, value);
            }
            else
            {
                var subpacketCount = Convert.ToInt32(binaryString.Substring(tracker, 11), 2);
                tracker += 11;
                var subValues = new List<long>();
                for (int i = 0; i < subpacketCount; i++)
                {
                    var result = ParsePacketPart2(binaryString, tracker);
                    tracker = result.Item1; //end location
                    subValues.Add(result.Item2);
                }
                var value = PerformOperation(subValues, type);
                return (tracker, value);
            }

            //return tracker;
        }

        internal static int ParsePacket(string binaryString, int startLoc)
        {
            var tracker = startLoc;
            
            var version = Convert.ToInt32(binaryString.Substring(tracker, 3), 2);
            tracker += 3;
            _versionNumbers.Add(version);
            
            var type = Convert.ToInt32(binaryString.Substring(tracker, 3), 2);
            tracker += 3;
            if (type == 4)
            {
                tracker += GetLiteralLength(binaryString, tracker);
                return tracker;
            }

            var lengthTypeId = binaryString[tracker];
            tracker++;
            if (lengthTypeId == '0')
            {
                var subpacketLength = Convert.ToInt32(binaryString.Substring(tracker, 15), 2);
                tracker += 15;
                var complete = tracker + subpacketLength;
                while (tracker != complete)
                {
                    tracker = ParsePacket(binaryString, tracker);
                }
            }
            else
            {
                var subpacketCount = Convert.ToInt32(binaryString.Substring(tracker, 11), 2);
                tracker += 11;
                for (int i = 0; i < subpacketCount; i++)
                {
                    tracker = ParsePacket(binaryString, tracker);
                }
            }

            return tracker;
        }

        internal static int GetLiteralLength(string binaryString, int startLoc)
        {
            var tracker = startLoc;

            var opCode = binaryString[tracker];
            while (opCode != '0')
            {
                tracker += 5;
                opCode = binaryString[tracker];
            }
            tracker += 5; // Cover when opCode == 0, last group

            return tracker - startLoc;
        }

        /// <summary>
        /// Returns length & value of literal
        /// </summary>
        /// <param name="binaryString"></param>
        /// <param name="startLoc"></param>
        /// <returns></returns>
        internal static (int, long) GetLiteral(string binaryString, int startLoc)
        {
            var thisLiteralStringBuilder = new StringBuilder();
            var tracker = startLoc;

            var opCode = binaryString[tracker];
            while (opCode != '0')
            {
                thisLiteralStringBuilder.Append(binaryString.Substring(tracker+1, 4));
                tracker += 5;
                opCode = binaryString[tracker];
            }
            thisLiteralStringBuilder.Append(binaryString.Substring(tracker + 1, 4));
            tracker += 5; // Cover when opCode == 0, last group

            var value = Convert.ToInt64(thisLiteralStringBuilder.ToString(), 2);

            return (tracker - startLoc, value);
        }

        internal static long PerformOperation(List<long> operands, int type)
        {
            switch (type)
            {
                case 0:
                    return operands.Sum();
                case 1:
                    return operands.Aggregate((acc, val) => acc * val);
                case 2:
                    return operands.Min();
                case 3:
                    return operands.Max();
                case 5:
                    return operands[0] > operands[1] ? 1 : 0;
                case 6:
                    return operands[0] < operands[1] ? 1 : 0;
                case 7:
                    return operands[0] == operands[1] ? 1 : 0;
            }
            throw new Exception("invalid type");
        }
        #endregion
    }
}
