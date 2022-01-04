using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class Box : IShape
    {
        public string AppearanceName { get; }
        public Vector Translation { get; }
        public Rotation Rotation { get; }

        readonly Size Size;

        static readonly Size DefaultSize = new Size
        {
            Width = 2,
            Height = 2,
            Depth = 2,
        };

        public Box(ShapeData shapeData)
        {
            AppearanceName = shapeData.AppearanceName;
            Translation = shapeData.Center;
            Size = shapeData.Size;

            Vector[] points = shapeData.Points.ToArray();
            var vs = shapeData.FaceIndices
                .Where(x => x.Length > 0)
                .Select(indices =>
                {
                    Vector vector = default;
                    foreach (var index in indices)
                    {
                        vector += points[index];
                    }
                    vector /= indices.Length;
                    return vector - Translation;
                });

            if (vs.Any())
            {
                Vector vectorX = nearest(Vector.UnitX);
                Vector vectorY = nearest(Vector.UnitY, vectorX);
                Vector vectorZ = nearest(Vector.UnitZ, vectorX, vectorY);

                Rotation = Rotation.GetRotation(vectorX, vectorY);

                if (Rotation.Angle != 0)
                {
                    Size.Width = vectorX.Length * 2;
                    Size.Height = vectorY.Length * 2;
                    Size.Depth = vectorZ.Length * 2;
                }

                Vector nearest(Vector unit, params Vector[] excludeVectors)
                {
                    excludeVectors = excludeVectors.Concat(excludeVectors.Select(x => -x)).ToArray();

                    var nearestVector = vs.First();

                    int skipCount;
                    for (skipCount = 1; isExcluded(nearestVector); skipCount++)
                    {
                        nearestVector = vs.ElementAt(skipCount);
                    }

                    double minAngle = unit.Angle(nearestVector);
                    foreach (var vector in vs.Skip(skipCount))
                    {
                        if (isExcluded(vector))
                        {
                            continue;
                        }

                        double angle = unit.Angle(vector);
                        if (angle < minAngle)
                        {
                            nearestVector = vector;
                            minAngle = angle;
                        }
                    }
                    return nearestVector;

                    bool isExcluded(Vector vector)
                    {
                        foreach (var excludeVector in excludeVectors)
                        {
                            if (excludeVector.Equals(vector))
                            {
                                return true;
                            }
                        }

                        return false;
                    }
                }
            }
        }

        public IEnumerable<string> Geometry
        {
            get
            {
                Size size = Size.Round();
                if (DefaultSize.Equals(size))
                {
                    return new string[] { "geometry Box {}" };
                }

                List<string> lines = new List<string>();

                lines.Add("geometry Box {");
                lines.Add($"\tsize {size.Width} {size.Height} {size.Depth}");
                lines.Add("}");

                return lines;
            }
        }
    }
}