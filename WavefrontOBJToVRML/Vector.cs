using System;

namespace WavefrontOBJToVRML
{
    internal struct Vector
    {
        public double X, Y, Z;

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public void Normalize()
        {
            double length = Length;
            X /= length;
            Y /= length;
            Z /= length;
        }

        public Vector CrossProduct(Vector vector)
        {
            return new Vector
            {
                X = Y * vector.Z - Z * vector.Y,
                Y = Z * vector.X - X * vector.Z,
                Z = X * vector.Y - Y * vector.X,
            };
        }

        public double InnerProduct(Vector vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector
            {
                X = -vector.X,
                Y = -vector.Y,
                Z = -vector.Z,
            };
        }

        public static Vector operator +(Vector vector, Point point)
        {
            return new Vector
            {
                X = vector.X + point.X,
                Y = vector.Y + point.Y,
                Z = vector.Z + point.Z
            };
        }

        public static Vector operator -(Vector vector, Point point)
        {
            return new Vector
            {
                X = vector.X - point.X,
                Y = vector.Y - point.Y,
                Z = vector.Z - point.Z,
            };
        }

        public static Vector operator /(Vector vector, double value)
        {
            return new Vector
            {
                Y = vector.Y / value,
                X = vector.X / value,
                Z = vector.Z / value,
            };
        }
    }
}
