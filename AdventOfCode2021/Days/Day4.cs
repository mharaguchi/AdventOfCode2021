using AdventOfCode2021.Models;
using AdventOfCode2021.Utils;

namespace AdventOfCode2021.Days
{
    public static class Day4
    {
        public static string _input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";

        public static string Run(string input)
        {
            //return RunPart1(input);
            //return RunPart1(_input);
            return RunPart2(input);
            //return RunPart2(_input);
        }

        internal static string RunPart1(string input)
        {
            var tokens = FileInputUtils.SplitInputByBlankLines(input);

            var numbersToCall = FileInputUtils.SplitLineIntoIntList(tokens[0], ",");
            var calledNumbers = new List<int>();

            var bingoBoards = new List<Day4BingoBoard>();
            for(int i = 1; i < tokens.Length; i++)
            {
                bingoBoards.Add(GetBingoBoard(tokens[i]));
            }

            var winnerFound = false;
            var winningScore = (long)-1;

            var turnNum = 0;
            while (!winnerFound)
            {
                var thisNumber = numbersToCall[turnNum];
                calledNumbers.Add(thisNumber);
                foreach(var bingoBoard in bingoBoards)
                {
                    if (bingoBoard.IsWinner(calledNumbers))
                    {
                        winnerFound = true;
                        winningScore = bingoBoard.GetScore(calledNumbers);
                    }
                }
                turnNum++;
            }

            return winningScore.ToString();
        }

        internal static string RunPart2(string input)
        {
            var tokens = FileInputUtils.SplitInputByBlankLines(input);

            var numbersToCall = FileInputUtils.SplitLineIntoIntList(tokens[0], ",");
            var calledNumbers = new List<int>();

            var bingoBoards = new List<Day4BingoBoard>();
            for (int i = 1; i < tokens.Length; i++)
            {
                bingoBoards.Add(GetBingoBoard(tokens[i]));
            }

            var winningScore = (long)-1;

            var turnNum = 0;
            while (winningScore < 0)
            {
                var thisNumber = numbersToCall[turnNum];
                calledNumbers.Add(thisNumber);
                var bingoBoardsCopy = new List<Day4BingoBoard>(bingoBoards);   
                foreach (var bingoBoard in bingoBoards)
                {
                    if (bingoBoard.IsWinner(calledNumbers))
                    {
                        bingoBoardsCopy.Remove(bingoBoard);
                        if (bingoBoardsCopy.Count == 0) 
                        {
                            winningScore = bingoBoard.GetScore(calledNumbers);
                        }
                    }
                }
                bingoBoards = bingoBoardsCopy;
                turnNum++;
            }

            return winningScore.ToString();
        }

        internal static Day4BingoBoard GetBingoBoard(string token)
        {
            var bingoBoard = new Day4BingoBoard();
            var lines = FileInputUtils.SplitLinesIntoStringArray(token);
            var intBoard = new List<List<int>>();
            for(int i = 0; i < lines.Length; i++)
            {
                intBoard.Add(FileInputUtils.SplitLineIntoIntList(lines[i], " "));
                bingoBoard.PossibleBingos.Add(intBoard[i]);
            }
            for (int j = 0; j < lines.Length; j++)
            {
                bingoBoard.PossibleBingos.Add(new List<int> { intBoard[0][j], intBoard[1][j], intBoard[2][j], intBoard[3][j], intBoard[4][j] });
            }
            bingoBoard.Numbers = intBoard.SelectMany(x => x).ToList();

            return bingoBoard;
        }
    }
}
