﻿using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class PointSet: IShape
    {
        public string AppearanceName { get; }

        readonly Point Center;
        readonly Size Size;
        readonly IEnumerable<Point> Points;
        readonly IEnumerable<int[]> FaceIndices;

        public PointSet(ShapeData shapeData)
        {
            AppearanceName = shapeData.AppearanceName;

            Center = shapeData.Center;
            Size = shapeData.Size;
            Points = shapeData.Points;
            FaceIndices = shapeData.FaceIndices;
        }

        public IEnumerable<string> Transform => new string[0];

        public IEnumerable<string> Geometry
        {
            get
            {
                List<string> lines = new List<string>();

                lines.Add("geometry IndexedFaceSet {");

                lines.Add("\tcoord Coordinate {");
                lines.Add("\t\tpoint [");
                foreach (var point in Points)
                {
                    lines.Add($"\t\t\t{point.X.Round()} {point.Y.Round()} {point.Z.Round()},");
                }
                lines.Add("\t\t]");
                lines.Add("\t}");

                lines.Add("\tcoordIndex [");
                foreach (var face in FaceIndices)
                {
                    lines.Add($"\t\t{string.Join(", ", face)}, -1,");
                }
                lines.Add("\t]");

                lines.Add("}");

                return lines;
            }
        }
    }
}