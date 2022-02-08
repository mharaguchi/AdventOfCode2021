using AdventOfCode2021.Utils;
using System.Text;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public static class Day20
    {
        public static string _sampleInput = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";

        public static bool[] conversions = new bool[512];

        public static string Run(string puzzleInput)
        {
            //return RunPart1(_sampleInput);
            //return RunPart1(puzzleInput); //5402 low, 5702 high
            //return RunPart2(_sampleInput);
            return RunPart2(puzzleInput);
        }

        internal static string RunPart1(string input)
        {
            var addedRows = 12;
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            SetConversionValues(lines[0]);
            var dimension = lines[2].Length;
            var newDimension = dimension + addedRows * 2; 
            var image = new bool[newDimension, newDimension];

            var initialImage = GetInitialImage(addedRows, lines, image);
            //PrintImage(initialImage);

            var newImage = RunImageAlgorithm(initialImage);
            //PrintImage(newImage);

            newImage = RunImageAlgorithm(newImage);
            //PrintImage(newImage);

            var count = GetLitCount(newImage);

            return count.ToString();
        }

        internal static string RunPart2(string input)
        {
            var addedRows = 55;
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            SetConversionValues(lines[0]);
            var dimension = lines[2].Length;
            var newDimension = dimension + addedRows * 2;
            var image = new bool[newDimension, newDimension];

            var initialImage = GetInitialImage(addedRows, lines, image);
            //PrintImage(initialImage);

            var newImage = initialImage;
            //PrintImage(newImage);
            for(int i = 0; i < 50; i++)
            {
                newImage = RunImageAlgorithm(newImage);
            }
            //PrintImage(newImage);

            var count = GetLitCount(newImage);

            return count.ToString();
        }

        #region Private Methods
        private static void SetConversionValues(string line)
        {
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    conversions[i] = true;
                }
            }
        }

        private static bool[,] GetInitialImage(int addedRows, string[] lines, bool[,] image)
        {
            for (int lineNum = 1; lineNum < lines.Length; lineNum++)
            {
                if (string.IsNullOrEmpty(lines[lineNum]))
                {
                    continue;
                }
                var y = lineNum + addedRows - 1; //start at x = 3 to account for added empty rows
                for (int i = 0; i < lines[lineNum].Length; i++)
                {
                    var x = i + addedRows;
                    image[x, y] = lines[lineNum][i] == '#';
                }
            }

            return image;
        }

        private static bool[,] RunImageAlgorithm(bool[,] image)
        {
            var newImage = new bool[image.GetLength(0), image.GetLength(0)];
            for(int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(0); y++)
                {
                    newImage[x, y] = CalculateValue(x, y, image);
                }
            }
            return newImage;
        }

        private static void PrintImage(bool[,] image)
        {
            for(int y = 0; y < image.GetLength(0); y++)
            {
                for(int x = 0; x < image.GetLength(0); x++)
                {
                    if (image[x, y])
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool CalculateValue(int x, int y, bool[,] image)
        {
            var max = image.GetLength(0) - 1;
            if (x == 0 || y == 0 || x == max || y == max)
            {
                var value = image[x, y] ? conversions[511] : conversions[0];
                return value;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(image[x - 1, y - 1] ? '1' : '0');
            builder.Append(image[x, y - 1] ? '1' : '0');
            builder.Append(image[x + 1, y - 1] ? '1' : '0');
            builder.Append(image[x - 1, y] ? '1' : '0');
            builder.Append(image[x, y] ? '1' : '0');
            builder.Append(image[x + 1, y] ? '1' : '0');
            builder.Append(image[x - 1, y + 1] ? '1' : '0');
            builder.Append(image[x, y + 1] ? '1' : '0');
            builder.Append(image[x + 1, y + 1] ? '1' : '0');

            var decimalValue = Convert.ToInt32(builder.ToString(), 2);
            return conversions[decimalValue];
        }

        private static int GetLitCount(bool[,] image)
        {
            var count = 0;

            for(int y = 0; y < image.GetLength(0); y++)
            {
                for (int x = 0; x < image.GetLength(0); x++)
                {
                    if (image[x, y] == true)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        #endregion
    }
}
