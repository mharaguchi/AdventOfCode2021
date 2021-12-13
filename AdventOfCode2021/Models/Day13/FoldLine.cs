using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Models.Day13
{
    public class FoldLine
    {
        public string XorY { get; set; }

        public int RowOrColNum { get; set; }

        public FoldLine(string xOrY, int rowOrColNum)
        {
            XorY = xOrY;
            RowOrColNum = rowOrColNum;
        }
    }
}
