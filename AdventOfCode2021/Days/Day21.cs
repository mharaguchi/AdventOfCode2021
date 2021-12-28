using AdventOfCode2021.Utils;
using System.Text;

namespace AdventOfCode2021.Days
{
    public static class Day21
    {
        public static string _sampleInput = @"Player 1 starting position: 4
Player 2 starting position: 8";
        public static long _player1Wins = (long)0;
        public static long _player2Wins = (long)0;

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
            var tokens = StringUtils.SplitInOrder(lines[0], new string[] { "Player 1 starting position: " });
            var player1Loc = Int32.Parse(tokens[0]);
            tokens = StringUtils.SplitInOrder(lines[1], new string[] { "Player 2 starting position: " });
            var player2Loc = Int32.Parse(tokens[0]);

            var player1Score = 0;
            var player2Score = 0;
            var dieValue = 1;
            var dieRolls = (long)0;

            while (true)
            {
                var dist = 0;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;

                player1Loc = (player1Loc + dist) % 10 == 0 ? 10 : (player1Loc + dist) % 10;
                player1Score += player1Loc;
                dieRolls += 3;

                if (player1Score >= 1000)
                {
                    break;
                }

                dist = 0;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;
                dist += dieValue;
                dieValue = dieValue == 100 ? 1 : dieValue + 1;

                player2Loc = (player2Loc + dist) % 10 == 0 ? 10 : (player2Loc + dist) % 10;
                player2Score += player2Loc;
                dieRolls += 3;

                if (player2Score >= 1000)
                {
                    break;
                }

            }

            var result = dieRolls * Math.Min(player1Score, player2Score);

            return result.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var tokens = StringUtils.SplitInOrder(lines[0], new string[] { "Player 1 starting position: " });
            var player1Loc = Int32.Parse(tokens[0]);
            tokens = StringUtils.SplitInOrder(lines[1], new string[] { "Player 2 starting position: " });
            var player2Loc = Int32.Parse(tokens[0]);

            var player1Score = 0;
            var player2Score = 0;

            var threads = new List<Thread>();

            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 3, 1);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 4, 3);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 5, 6);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 6, 7);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 7, 6);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 8, 3);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, 9, 1);

            //for (int i = 1; i <= 3; i++)
            //{
            //    for (int j = 1; j <= 3; j++)
            //    {
            //        for (int k = 1; k <= 3; k++)
            //        {
            //            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, 1, i + j + k);
            //        }
            //    }
            //}

            //foreach (var t in threads)
            //{
            //    t.Join();
            //}

            var result = Math.Max(_player1Wins, _player2Wins);

            return result.ToString();
        }

        #region Private Methods
        private static void ProcessTurn(int player1Score, int player1Loc, int player2Score, int player2Loc, int playerTurn, int dist, long universes)
        {
            var nextPlayerTurn = playerTurn == 1 ? 2 : 1;

            if (playerTurn == 1)
            {
                player1Loc = (player1Loc + dist) % 10 == 0 ? 10 : (player1Loc + dist) % 10;
                player1Score += player1Loc;
                if (player1Score >= 21)
                {
                    _player1Wins += universes;
                    //_player1Wins++;

                    return;
                }
            }
            else
            {
                player2Loc = (player2Loc + dist) % 10 == 0 ? 10 : (player2Loc + dist) % 10;
                player2Score += player2Loc;
                if (player2Score >= 21)
                {
                    _player2Wins += universes;
                    //_player2Wins++;

                    return;
                }
            }

            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 3, universes * 1);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 4, universes * 3);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 5, universes * 6);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 6, universes * 7);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 7, universes * 6);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 8, universes * 3);
            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, 9, universes * 1);

            //for (int i = 1; i <= 3; i++)
            //{
            //    for (int j = 1; j <= 3; j++)
            //    {
            //        for (int k = 1; k <= 3; k++)
            //        {
            //            ProcessTurn(player1Score, player1Loc, player2Score, player2Loc, nextPlayerTurn, i + j + k);
            //        }
            //    }
            //}
        }
        #endregion
    }
}
