using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Days
{
    public static class Day16
    {
        public static string _sampleInput = @"A0016C880162017C3686B18A3D4780";

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
            return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
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
            return "";
        }

        #region Private Methods
        internal static int GetVersions(string binaryString)
        {
            return 0;
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
        #endregion
    }
}
