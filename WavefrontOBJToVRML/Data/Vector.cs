using System;

namespace WavefrontOBJToVRML
{
    internal struct Vector
    {
        public double X, Y, Z;

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public static readonly Vector UnitX = new Vector { X = 1 };
        public static readonly Vector UnitY = new Vector { Y = 1 };
        public static readonly Vector UnitZ = new Vector { Z = 1 };

        public void Normalize()
        {
            double length = Length;
            if (length == 0)
            {
                return;
            }

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

        public Vector ProjectTo(Vector vector)
        {
            vector.Normalize();
            return vector * InnerProduct(vector);
        }

        public double Angle(Vector vector)
        {
            Vector v = this;
            v.Normalize();
            vector.Normalize();
            return Math.Acos(v.InnerProduct(vector));
        }

        public double Distance(Vector vector)
        {
            return (this - vector).Length;
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
        
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector
            {
                X = vector1.X + vector2.X,
                Y = vector1.Y + vector2.Y,
                Z = vector1.Z + vector2.Z
            };
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector
            {
                X = vector1.X - vector2.X,
                Y = vector1.Y - vector2.Y,
                Z = vector1.Z - vector2.Z,
            };
        }

        public static Vector operator *(Vector vector, double value)
        {
            return new Vector
            {
                Y = vector.Y * value,
                X = vector.X * value,
                Z = vector.Z * value,
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

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object obj)
        {
            Vector vector = (Vector)obj;
            return vector.X == X && vector.Y == Y && vector.Z == Z;
        }

        public override int GetHashCode()
        {
            return (int)((X + Y + Z) * 1000000);
        }
    }
}
