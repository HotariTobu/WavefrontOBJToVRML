using System;

namespace WavefrontOBJToVRML
{
    internal struct Rotation
    {
        public double X, Y, Z, Angle;
        public Vector Vector => new Vector { X = X, Y = Y, Z = Z, };

        static readonly double PI2 = Math.PI * 2;

        public Rotation(Vector vector, double angle)
        {
            vector.Normalize();
            vector = vector.Round();

            Vector invertedVector = -vector;
            if (countNegative(vector) > countNegative(invertedVector))
            {
                vector = invertedVector;
                if (vector.X < 1 && vector.Y < 1 && vector.Z < 1)
                {
                    angle = -angle;
                }
            }

            angle = angle.Round();
            double pi2 = PI2.Round();
            while (angle < 0)
            {
                angle += pi2;
            }
            while (angle > pi2)
            {
                angle -= pi2;
            }

            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            Angle = angle.Round();
            
            int countNegative(Vector v)
            {
                return v.X < 0 ? 1 : 0 + v.Y < 0 ? 1 : 0 + v.Z < 0 ? 1 : 0;
            }
        }

        public static Rotation GetRotation(Vector vector)
        {
            Vector unit = Vector.UnitY;
            vector.Normalize();
            Vector rotationVector = unit.CrossProduct(vector);
            double angle = Math.Acos(unit.InnerProduct(vector));
            return new Rotation(rotationVector, angle);
        }

        public static Rotation GetRotation(Vector newUnitX, Vector newUnitY)
        {
            newUnitX.Normalize();
            newUnitY.Normalize();

            Vector sumX = Vector.UnitX + newUnitX;
            Vector sumY = Vector.UnitY + newUnitY;

            Vector crossX = Vector.UnitX.CrossProduct(newUnitX);
            Vector crossY = Vector.UnitY.CrossProduct(newUnitY);

            Vector nX = crossX.CrossProduct(sumX);
            Vector nY = crossY.CrossProduct(sumY);

            Vector rotationVector = nX.CrossProduct(nY);
            rotationVector.Normalize();

            Vector projectX = Vector.UnitX.ProjectTo(rotationVector);

            Vector project0 = Vector.UnitX - projectX;
            Vector project1 = newUnitX - projectX;

            double angle = project0.Angle(project1);

            return new Rotation(rotationVector, angle);
        }

        public override string ToString() => $"({X}, {Y}, {Z}, {Angle})";

        public override bool Equals(object obj)
        {
            Rotation rotation = (Rotation)obj;
            return rotation.X == X && rotation.Y == Y && rotation.Z == Z && rotation.Angle == Angle;
        }

        public override int GetHashCode()
        {
            return (int)((X + Y + Z + Angle) * 1000000);
        }
    }
}
