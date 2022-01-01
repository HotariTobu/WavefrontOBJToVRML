using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class Cone : IShape
    {
        public string AppearanceName { get; }
        public Vector Translation { get; }
        public Rotation Rotation { get; }

        readonly double Radius;
        readonly double Height;

        public Cone(ShapeData shapeData)
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

            Vector[] points = shapeData.Points.ToArray();

            Vector circleCenter = default;
            List<int> allIndices = Enumerable.Range(0, points.Length).ToList();
            foreach (var index in indices)
            {
                circleCenter += points[index];
                allIndices.Remove(index);
            }

            if (indices.Length > 0)
            {
                circleCenter /= indices.Length;
                Vector head = points[allIndices.FirstOrDefault()];

                Vector axisY = head - circleCenter;
                Translation = (head + circleCenter) / 2;
                Rotation = Rotation.GetRotation(axisY);
                Radius = (circleCenter - points[indices[0]]).Length;
                Height = axisY.Length;
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
                    return new string[] { "geometry Cone {}" };
                }

                List<string> lines = new List<string>();

                lines.Add("geometry Cone {");

                if (radius != 1)
                {
                    lines.Add($"\tbottomRadius {radius}");
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