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

        public static int digits = 3;
        public static double Round(this double value)
        {
            return Math.Round(value, digits);
        }

        public static Vector Round(this Vector vector)
        {
            return new Vector
            {
                X = vector.X.Round(),
                Y = vector.Y.Round(),
                Z = vector.Z.Round(),
            };
        }

        public static Size Round(this Size size)
        {
            return new Size
            {
                Width = size.Width.Round(),
                Height = size.Height.Round(),
                Depth = size.Depth.Round(),
            };
        }

        public static Color Round(this Color color)
        {
            return new Color
            {
                R = color.R.Round(),
                G = color.G.Round(),
                B = color.B.Round(),
            };
        }
    }
}
