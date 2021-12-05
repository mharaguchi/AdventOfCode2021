using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models
{
    public class Day4BingoBoard
    {
        public List<int> Numbers { get; set; }
        public List<List<int>> PossibleBingos { get; set; }

        public Day4BingoBoard()
        {
            PossibleBingos = new List<List<int>>();
            Numbers = new List<int>();
        }

        public bool IsWinner(List<int> calledNumbers)
        {
            var isWinner = false;
            var winningLines = PossibleBingos.Where(x => x.Intersect(calledNumbers).Count() == 5);
            if (winningLines?.Count() > 0) 
            { 
                isWinner = true; 
            }
            return isWinner;
        }

        public long GetScore(List<int> calledNumbers)
        {
            var unmarkedNumbers = Numbers.Except(calledNumbers);
            var sum = unmarkedNumbers.Sum();

            return sum * calledNumbers.Last();
        }
    }
}
