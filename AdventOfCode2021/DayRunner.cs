using AdventOfCode2021.Days;
using AdventOfCode2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class DayRunner
    {
        const int DAY = 4;

        public static string GetAnswer()
        {
            var input = FileInputUtils.GetInput(DAY);
            return Day4.Run(input).ToString();
        }
    }
}
