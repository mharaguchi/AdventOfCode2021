using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;
using System.Drawing;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day12
    {
        public static string _sampleInput = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

        public static string _sampleInput2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

        public static string _sampleInput3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(_sampleInput2);
            //return RunPart1(_sampleInput3);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            //return RunPart2(_sampleInput2);
            //return RunPart2(_sampleInput3);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var paths = GetPaths(lines);
            var traversed = new List<string>();
            var numPaths = CountPaths("start", paths, traversed);

            return numPaths.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var paths = GetPaths(lines);
            var traversed = new List<string>();
            var numPaths = CountPathsPart2("start", paths, traversed, false);

            return numPaths.ToString();
        }

        #region Private Methods
        private static int CountPaths(string sourceCave, Dictionary<string, List<string>> paths, List<string> traversed)
        {
            var numPaths = 0;

            if (sourceCave == "end")
            {
                foreach(var loc in traversed)
                {
                    Console.Write(loc + ",");
                }
                Console.Write("end");
                Console.WriteLine();
                return 1;
            }

            traversed.Add(sourceCave);

            var connections = paths[sourceCave];
            var validNextLocations = new List<string>(connections);
            foreach(var location in connections)
            {
                if (traversed.Contains(location) && char.IsLower(location[0]))
                {
                    validNextLocations.Remove(location);
                }
            }

            if (validNextLocations == null || validNextLocations.Count == 0)
            {
                return 0;
            }

            foreach (var location in validNextLocations)
            {
                if ((char.IsLower(location[0]) && !traversed.Contains(location)) || char.IsUpper(location[0]))
                {
                    numPaths += CountPaths(location, paths, new List<string>(traversed));
                }
            }

            return numPaths;
        }

        private static int CountPathsPart2(string sourceCave, Dictionary<string, List<string>> paths, List<string> traversed, bool usedSecondVisit)
        {
            var numPaths = 0;

            if (sourceCave == "end")
            {
                //foreach (var loc in traversed)
                //{
                //    Console.Write(loc + ",");
                //}
                //Console.Write("end");
                //Console.WriteLine();
                return 1;
            }

            if (traversed.Contains(sourceCave) && char.IsLower(sourceCave[0]))
            {
                usedSecondVisit = true;
            }

            traversed.Add(sourceCave);

            var connections = paths[sourceCave];
            var validNextLocations = new List<string>(connections);
            foreach (var location in connections)
            {
                if ((traversed.Contains(location) && char.IsLower(location[0]) && usedSecondVisit) || location == "start")
                {
                    validNextLocations.Remove(location);
                }
            }

            if (validNextLocations == null || validNextLocations.Count == 0)
            {
                return 0;
            }

            foreach (var location in validNextLocations)
            {
                numPaths += CountPathsPart2(location, paths, new List<string>(traversed), usedSecondVisit);
            }

            return numPaths;
        }

        private static Dictionary<string, List<string>> GetPaths(string[] lines)
        {
            var paths = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var tokens = line.Split(new char[] { '-' });
                var source = tokens[0];
                var dest = tokens[1];

                if (!paths.ContainsKey(source))
                {
                    paths.Add(source, new List<string> { dest });
                }
                else
                {
                    paths[source].Add(dest);
                }

                if (!paths.ContainsKey(dest))
                {
                    paths.Add(dest, new List<string> { source });
                }
                else
                {
                    paths[dest].Add(source);
                }
            }

            return paths;
        }
        #endregion

    }
}
