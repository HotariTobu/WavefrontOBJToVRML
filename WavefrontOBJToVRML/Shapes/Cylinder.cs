﻿using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class Cylinder : IShape
    {
        public string AppearanceName { get; }
        public Vector Translation { get; }
        public Rotation Rotation { get; }

        readonly double Radius;
        readonly double Height;

        public Cylinder(ShapeData shapeData)
        {
            AppearanceName = shapeData.AppearanceName;
            Translation = shapeData.Center;

            int[] indices = new int[0];
            foreach (var face in shapeData.FaceIndices)
            {
                if (indices.Length < face.Length)
                {
                    indices = face;
                }
            }

            Vector circleCenter = default;
            Vector[] points = shapeData.Points.ToArray();
            foreach (var index in indices)
            {
                circleCenter += points[index];
            }

            if (indices.Length > 0)
            {
                circleCenter /= indices.Length;

                Vector centerVector = circleCenter - Translation;
                Rotation = Rotation.GetRotation(centerVector);
                Height = centerVector.Length * 2;
                Radius = (circleCenter - points[indices[0]]).Length;
            }
        }

        public IEnumerable<string> Geometry
        {
            get
            {
                double radius = Radius.Round();
                double height = Height.Round();
                if (radius == 1 && height == 2)
                {
                    return new string[] { "geometry Cylinder {}" };
                }

                List<string> lines = new List<string>();

                lines.Add("geometry Cylinder {");

                if (radius != 1)
                {
                    lines.Add($"\tradius {radius}");
                }

                if (height != 2)
                {
                    lines.Add($"\theight {height}");
                }

                lines.Add("}");

                return lines;
            }
        }
    }
}