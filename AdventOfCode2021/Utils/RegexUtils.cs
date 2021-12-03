using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2021.Utils
{
    public static class RegexUtils
    {

        /* Example Patterns */
        /*
         * ^[0-9]{9}$ - 9 digits
         * ^[#][0-9a-f]{6}$ - # followed by a mix of 0-9 or a-f for 6 characters
         * ^amb|blu|brn|gry|grn|hzl|oth$ - specific enum values
         */
        public static bool IsMatch(string pattern, string input)
        {
            Regex rx = new Regex(pattern);
            if (!rx.IsMatch(input))
            {
                return false;
            }
            return true;
        }
    }
}
