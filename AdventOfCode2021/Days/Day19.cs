using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day19
    {
        public static string _sampleInput = @"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401

--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390

--- scanner 2 ---
649,640,665
682,-795,504
-784,533,-524
-644,584,-595
-588,-843,648
-30,6,44
-674,560,763
500,723,-460
609,671,-379
-555,-800,653
-675,-892,-343
697,-426,-610
578,704,681
493,664,-388
-671,-858,530
-667,343,800
571,-461,-707
-138,-166,112
-889,563,-600
646,-828,498
640,759,510
-630,509,768
-681,-892,-333
673,-379,-804
-742,-814,-386
577,-820,562

--- scanner 3 ---
-589,542,597
605,-692,669
-500,565,-823
-660,373,557
-458,-679,-417
-488,449,543
-626,468,-788
338,-750,-386
528,-832,-391
562,-778,733
-938,-730,414
543,643,-506
-524,371,-870
407,773,750
-104,29,83
378,-903,-323
-778,-728,485
426,699,580
-438,-605,-362
-469,-447,-387
509,732,623
647,635,-688
-868,-804,481
614,-800,639
595,780,-596

--- scanner 4 ---
727,592,562
-293,-554,779
441,611,-461
-714,465,-776
-743,427,-804
-660,-479,-426
832,-632,460
927,-485,-438
408,393,-506
466,436,-512
110,16,151
-258,-428,682
-393,719,612
-211,-452,876
808,-476,-593
-575,615,604
-485,667,467
-680,325,-822
-627,-443,-432
872,-547,-609
833,512,582
807,604,487
839,-516,451
891,-625,532
-652,-548,-490
30,-46,-14";

        const int REQUIRED_OVERLAP = 3;

        public static string Run(string puzzleInput)
        {
            return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput);
            //return RunPart2(_sampleInput);
            //return RunPart2(puzzleInput);
        }

        internal static Dictionary<int, (int, int, int)> _scannerLocations = new Dictionary<int, (int, int, int)>();
        internal static Dictionary<int, List<int>> _beaconDistances = new Dictionary<int, List<int>>();

        /// <summary>
        /// First int is scanner num, 2nd int is beacon number, List of int is the relative distances to the other beacons in this scanner
        /// </summary>
        internal static Dictionary<int, Dictionary<int, List<int>>> _beaconRelativeDistances = new Dictionary<int, Dictionary<int, List<int>>>();

        internal static List<(int, int, int)> _normalizedBeaconLocations = new List<(int, int, int)>();

        /// <summary>
        /// Each scanner is a list of beacon locations relative to that scanner
        /// </summary>
        internal static List<List<(int, int, int)>> _scanners = new List<List<(int, int, int)>>();

        internal static string RunPart1(string input)
        {

            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            
            ProcessInput(_scanners, lines);

            int dupes = SetBeaconDistances(_beaconDistances, _scanners);
            if (dupes > 0)
            {
                Console.WriteLine("Dupes found: " + dupes.ToString());
            }

            _scannerLocations.Add(0, (0, 0, 0));

            while (_scannerLocations.Count < _scanners.Count)
            {
                var currentKnownLocations = _scannerLocations.Select(x => x.Key).ToList();
                foreach (var knownLocation in currentKnownLocations)
                {
                    for (int i = 0; i < _scanners.Count; i++)
                    {
                        if (currentKnownLocations.Contains(i))
                        {
                            continue;
                        }
                        if (!_scannerLocations.ContainsKey(i) && Have12CommonBeacons(knownLocation, i))
                        {
                            //var location = GetAbsoluteScannerLocation(knownLocation, i);
                            //foreach(var beaconLocation in _scanners[i])
                            //{
                            //    var normalizedBeaconLocation = (beaconLocation.Item1 + location.Item1, beaconLocation.Item2 + location.Item2, beaconLocation.Item3 + location.Item3);
                            //    if (!_normalizedBeaconLocations.Contains(normalizedBeaconLocation))
                            //    {
                            //        _normalizedBeaconLocations.Add(normalizedBeaconLocation);
                            //    }
                            //}
                            Console.WriteLine($"12 common beacons found between {knownLocation} and {i}");
                            _scannerLocations.Add(i, (0,0,0)); //Unable to find normalized location so far, so adding default value
                        }
                    }
                }
            }

            return "Done";
        }

        internal static string RunPart2(string input)
        {
            return "";
        }

        #region Private Methods
        private static bool IsBeaconMatch(KeyValuePair<int, List<int>> srcBeacon, KeyValuePair<int, List<int>> tgtBeacon)
        {
            var srcDistances = srcBeacon.Value;
            var matches = tgtBeacon.Value.Where(x => srcDistances.Contains(x)).Count();
            if (matches >= 11) //beacons that are the same should have the same relative distances to at least the other 11 overlapping beacons
            {
                return true;
            }

            return false;
        }

        private static (int, int, int) GetAbsoluteScannerLocation(int srcScanner, int tgtScanner)
        {
            foreach(var srcBeacon in _beaconRelativeDistances[srcScanner])
            {
                foreach(var tgtBeacon in _beaconRelativeDistances[tgtScanner])
                {
                    if (IsBeaconMatch(srcBeacon, tgtBeacon))
                    {
                        var srcBeaconLocation = _scanners[srcScanner][srcBeacon.Key];
                        var tgtBeaconLocation = _scanners[tgtScanner][tgtBeacon.Key];

                        var diffX = tgtBeaconLocation.Item1 - srcBeaconLocation.Item1;
                        var diffY = tgtBeaconLocation.Item2 - srcBeaconLocation.Item2;
                        var diffZ = tgtBeaconLocation.Item3 - srcBeaconLocation.Item3;

                        var srcAbsoluteLocation = _scannerLocations[srcScanner];
                        var tgtAbsoluteLocationX = srcAbsoluteLocation.Item1 + diffX;
                        var tgtAbsoluteLocationY = srcAbsoluteLocation.Item2 + diffY;
                        var tgtAbsoluteLocationZ = srcAbsoluteLocation.Item3 + diffZ;

                        return (tgtAbsoluteLocationX, tgtAbsoluteLocationY, tgtAbsoluteLocationZ);
                    }
                }
            }
            throw new Exception("Could not translate scanner location");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanners"></param>
        /// <param name="lines"></param>
        private static void ProcessInput(List<List<(int, int, int)>> scanners, string[] lines)
        {
            var beaconCount = 0;
            var beacons = new List<(int, int, int)>();

            foreach (var line in lines)
            {
                if (line.StartsWith("---"))
                {
                    if (beacons.Any())
                    {
                        scanners.Add(beacons);
                        beaconCount += beacons.Count();
                    }
                    beacons = new List<(int, int, int)>();
                }
                else
                {
                    var tokens = line.Split(',');
                    beacons.Add((Int32.Parse(tokens[0]), Int32.Parse(tokens[1]), Int32.Parse(tokens[2])));
                }
            }
            if (beacons.Any()) //Add last scanner's beacons
            {
                scanners.Add(beacons);
                beaconCount += beacons.Count();
            }
            Console.WriteLine("Total Beacons: " + beaconCount);
        }

        private static int SetBeaconDistances(Dictionary<int, List<int>> beaconDistances, List<List<(int, int, int)>> scanners)
        {
            var dupes = 0;
            for (int s = 0; s < scanners.Count; s++)
            {
                _beaconRelativeDistances.Add(s, new Dictionary<int, List<int>>());

                var distances = new List<int>();
                var theseBeacons = scanners[s];

                /*
                 * This section of code was added late, so it's a separate set of loops
                 */
                for (int i = 0; i < theseBeacons.Count; i++)
                {
                    for (int j = 0; j < theseBeacons.Count; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        var dist = Math.Abs(theseBeacons[i].Item1 - theseBeacons[j].Item1) + Math.Abs(theseBeacons[i].Item2 - theseBeacons[j].Item2) + Math.Abs(theseBeacons[i].Item3 - theseBeacons[j].Item3);
                        if (!_beaconRelativeDistances[s].ContainsKey(i))
                        {
                            _beaconRelativeDistances[s].Add(i, new List<int> { dist });
                        }
                        else
                        {
                            _beaconRelativeDistances[s][i].Add(dist);
                        }
                    }
                }

                for (int i = 0; i < theseBeacons.Count; i++)
                {
                    for (int j = i + 1; j < theseBeacons.Count; j++)
                    {
                        var dist = Math.Abs(theseBeacons[i].Item1 - theseBeacons[j].Item1) + Math.Abs(theseBeacons[i].Item2 - theseBeacons[j].Item2) + Math.Abs(theseBeacons[i].Item3 - theseBeacons[j].Item3);
                        if (distances.Contains(dist))
                        {
                            dupes++;
                        }
                        else
                        {
                            distances.Add(dist);
                        }
                    }
                }
                beaconDistances.Add(s, distances);
            }

            return dupes;
        }

        internal static bool Have12CommonBeacons(int knownScanner, int newScanner)
        {
            var count = 0;

            var srcBeaconDistances = _beaconDistances[knownScanner];
            var testBeaconDistances = _beaconDistances[newScanner];

            foreach(var testBeaconDistance in testBeaconDistances)
            {
                if (srcBeaconDistances.Contains(testBeaconDistance))
                {
                    count++;
                }
            }

            return count >= 66;
        }
        #endregion
    }
}
