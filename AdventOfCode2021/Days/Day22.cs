using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day22
    {
        public static string _sampleInput = @"on x=-20..26,y=-36..17,z=-47..7
on x=-20..33,y=-21..23,z=-26..28
on x=-22..28,y=-29..23,z=-38..16
on x=-46..7,y=-6..46,z=-50..-1
on x=-49..1,y=-3..46,z=-24..28
on x=2..47,y=-22..22,z=-23..27
on x=-27..23,y=-28..26,z=-21..29
on x=-39..5,y=-6..47,z=-3..44
on x=-30..21,y=-8..43,z=-13..34
on x=-22..26,y=-27..20,z=-29..19
off x=-48..-32,y=26..41,z=-47..-37
on x=-12..35,y=6..50,z=-50..-2
off x=-48..-32,y=-32..-16,z=-15..-5
on x=-18..26,y=-33..15,z=-7..46
off x=-40..-22,y=-38..-28,z=23..41
on x=-16..35,y=-41..10,z=-47..6
off x=-32..-23,y=11..30,z=-14..3
on x=-49..-5,y=-3..45,z=-29..18
off x=18..30,y=-20..-8,z=-3..13
on x=-41..9,y=-7..43,z=-33..15
on x=-54112..-39298,y=-85059..-49293,z=-27449..7877
on x=967..23432,y=45373..81175,z=27513..53682";

        public static bool[,,] _cubes = new bool[101,101,101];

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
            foreach(var line in lines)
            {
                if (line.StartsWith("on"))
                {
                    ProcessLineOn(line);
                }
                else
                {
                    ProcessLineOff(line);
                }
            }

            var totalOn = GetOnCount();
            
            return totalOn.ToString();
        }

        internal static string RunPart2(string input)
        {
            return "";        
        }

        #region Private Methods
        private static int GetOnCount()
        {
            var count = 0;

            for(int i = 0; i < 101; i++)
            {
                for(int j = 0; j < 101; j++)
                {
                    for(int k = 0; k < 101; k++)
                    {
                        if (_cubes[i,j,k])
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
        private static void ProcessLineOn(string line)
        {
            var tokens = StringUtils.SplitInOrder(line, new string[] { "on x=", "..", ",y=", "..", ",z=", ".." });
            ProcessLine(tokens, true);
        }

        private static void ProcessLineOff(string line)
        {
            var tokens = StringUtils.SplitInOrder(line, new string[] { "off x=", "..", ",y=", "..", ",z=", ".." });
            ProcessLine(tokens, false);
        }

        private static (int, int, int, int, int, int) GetMaxAndMins(List<string> tokens)
        {
            return (Int32.Parse(tokens[0]), Int32.Parse(tokens[1]), Int32.Parse(tokens[2]), Int32.Parse(tokens[3]), Int32.Parse(tokens[4]),
                Int32.Parse(tokens[5]));
        
        }

        private static void ProcessLine(List<string> tokens, bool on)
        {
            var minMaxes = GetMaxAndMins(tokens);
            if (minMaxes.Item1 > 50 || minMaxes.Item2 < -50 || minMaxes.Item3 > 50 || minMaxes.Item4 < -50 || minMaxes.Item5 > 50 || minMaxes.Item6 < -50)
            {
                return;
            }

            var minX = minMaxes.Item1 < -50 ? -50 : minMaxes.Item1;
            var maxX = minMaxes.Item2 > 50 ? 50 : minMaxes.Item2;
            var minY = minMaxes.Item3 < -50 ? -50 : minMaxes.Item3;
            var maxY = minMaxes.Item4 > 50 ? 50 : minMaxes.Item4;
            var minZ = minMaxes.Item5 < -50 ? -50 : minMaxes.Item5;
            var maxZ = minMaxes.Item6 > 50 ? 50 : minMaxes.Item6;

            for(int x = minX; x <= maxX; x++)
            {
                for(int y = minY; y <= maxY; y++)
                {
                    for(int z = minZ; z <= maxZ; z++)
                    {
                        _cubes[x + 50, y + 50, z + 50] = on;
                    }
                }
            }
        }
        #endregion
    }
}
