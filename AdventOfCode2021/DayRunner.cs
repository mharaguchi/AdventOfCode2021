﻿using AdventOfCode2021.Days;
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
        const int DAY = 11;

        public static string GetAnswer()
        {
            var input = FileInputUtils.GetInput(DAY);
            return Day11.Run(input).ToString();
        }
    }
}
