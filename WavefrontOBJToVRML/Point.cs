using System;

namespace WavefrontOBJToVRML
{
    internal struct Point
    {
        public double X, Y, Z;

        public Point(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value");
            }

            string[] tokens = value.Split(' ');

            if (tokens.Length < 3)
            {
                X = 0;
                Y = 0;
                Z = 0;
                return;
            }

            X = double.Parse(tokens[0]);
            Y = double.Parse(tokens[1]);
            Z = double.Parse(tokens[2]);
        }
    }
}
