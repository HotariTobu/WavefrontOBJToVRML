using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class Sphere : IShape
    {
        public string AppearanceName { get; }
        public Vector Translation { get; }
        public Rotation Rotation { get; }

        readonly double Radius;

        public Sphere(ShapeData shapeData)
        {
            AppearanceName = shapeData.AppearanceName;
            Translation = shapeData.Center;
            Radius = (shapeData.Size.Width / 2);
        }

        public IEnumerable<string> Geometry
        {
            get
            {
                double radius = Radius.Round();
                if (radius == 1)
                {
                    return new string[] { "geometry Sphere {}" };
                }

                List<string> lines = new List<string>();

                lines.Add("geometry Sphere {");
                lines.Add($"\tradius {radius}");
                lines.Add("}");

                return lines;
            }
        }
    }
}