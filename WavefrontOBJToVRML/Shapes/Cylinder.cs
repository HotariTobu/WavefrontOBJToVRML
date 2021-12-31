using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class Cylinder : IShape
    {
        public string AppearanceName { get; }

        readonly Point Center;
        readonly Vector RotationVector;
        readonly double Angle;
        readonly double Radius;
        readonly double Height;

        public Cylinder(ShapeData shapeData)
        {
            AppearanceName = shapeData.AppearanceName;

            Center = shapeData.Center;

            int[] indices = new int[0];
            foreach (var face in shapeData.FaceIndices)
            {
                if (indices.Length < face.Length)
                {
                    indices = face;
                }
            }

            Vector circleCenter = new Vector();
            Point[] points = shapeData.Points.ToArray();
            foreach (var index in indices)
            {
                circleCenter += points[index];
            }

            if (indices.Length > 0)
            {
                circleCenter /= indices.Length;

                Radius = (circleCenter - points[indices[0]]).Length.Round();
            }

            Vector centerVector = circleCenter - Center;
            Height = (centerVector.Length * 2).Round();

            Vector baseVector = new Vector { Y = 1 };
            centerVector.Normalize();
            Vector rotationVector = baseVector.CrossProduct(centerVector);
            double angle = Math.Acos(baseVector.InnerProduct(centerVector));

            rotationVector.Normalize();
            rotationVector.X = rotationVector.X.Round();
            rotationVector.Y = rotationVector.Y.Round();
            rotationVector.Z = rotationVector.Z.Round();

            Vector invertedRotationVector = -rotationVector;
            if (countNegative(rotationVector) > countNegative(invertedRotationVector))
            {
                rotationVector = invertedRotationVector;
            }

            RotationVector = rotationVector;
            Angle = angle.Round();

            int countNegative(Vector vector)
            {
                return vector.X < 0 ? 1 : 0 + vector.Y < 0 ? 1 : 0 + vector.Z < 0 ? 1 : 0;
            }
        }

        public IEnumerable<string> Transform
        {
            get
            {
                List<string> lines = new List<string>();

                lines.Add($"translation {Center.X} {Center.Y} {Center.Z}");

                if (Angle != 0)
                {
                    lines.Add($"rotation {RotationVector.X} {RotationVector.Y} {RotationVector.Z} {Angle}");
                }

                return lines;
            }
        }

        public IEnumerable<string> Geometry
        {
            get
            {
                List<string> lines = new List<string>();
                lines.Add("geometry Cylinder {");

                if (Radius != 1)
                {
                    lines.Add($"\tradius {Radius}");
                }

                if (Height != 2)
                {
                    lines.Add($"\theight {Height}");
                }

                lines.Add("}");

                return lines;
            }
        }
    }
}