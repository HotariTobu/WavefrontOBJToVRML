using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal static class Extensions
    {
        public static IEnumerable<string> PrependEach(this IEnumerable<string> lines, string value)
        {
            return lines.Select(x => $"{value}{x}");
        }

        public static int digits = 5;
        public static double Round(this double value)
        {
            return Math.Round(value, digits);
        }
    }
}
