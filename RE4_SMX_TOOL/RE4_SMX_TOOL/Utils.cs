using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4_SMX_TOOL
{
    public static class Utils
    {
        public static string ReturnValidHexValue(string cont)
        {
            string res = "";
            foreach (var c in cont.ToUpperInvariant())
            {
                if (char.IsDigit(c)
                    || c == 'A'
                    || c == 'B'
                    || c == 'C'
                    || c == 'D'
                    || c == 'E'
                    || c == 'F'
                    )
                {
                    res += c;
                }
            }
            return res;
        }

        public static string ReturnValidDecValue(string cont)
        {
            string res = "";
            foreach (var c in cont)
            {
                if (char.IsDigit(c))
                {
                    res += c;
                }
            }
            return res;
        }

        public static string ReturnValidDecWithNegativeValue(string cont)
        {
            bool negative = false;

            string res = "";
            foreach (var c in cont)
            {
                if (negative == false && c == '-')
                {
                    res = c + res;
                    negative = true;
                }
                else if (char.IsDigit(c))
                {
                    res += c;
                }
            }
            return res;
        }

        public static string ReturnValidFloatValue(string cont)
        {
            bool dot = false;
            bool negative = false;

            string res = "";
            foreach (var c in cont)
            {
                if (negative == false && c == '-')
                {
                    res = c + res;
                    negative = true;
                }
                else if (dot == false && c == '.')
                {
                    res += c;
                    dot = true;
                }
                else if (char.IsDigit(c))
                {
                    res += c;
                }
            }
            return res;
        }

    }
}
