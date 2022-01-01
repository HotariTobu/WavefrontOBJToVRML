using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal partial class ShapeData
    {
        public string AppearanceName { get; set; } = "";
        public int NextIndex => VertexIndex + _Points.Count;

        public IEnumerable<Vector> Points => _Points;
        public IEnumerable<int[]> FaceIndices => _FaceIndices;

        public Vector Center => BoundingBox.Center;
        public Size Size => BoundingBox.Size;

        readonly Type Type;
        readonly int VertexIndex;

        readonly List<Vector> _Points = new List<Vector>();
        readonly List<int[]> _FaceIndices = new List<int[]>();
        readonly BoundingBox BoundingBox = new BoundingBox();

        public ShapeData(Type type, int vertexIndex)
        {
            Type = type;
            VertexIndex = vertexIndex;
        }

        public void AddVertex(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value");
            }

            Vector point = default;

            string[] tokens = value.Split(' ');

            if (tokens.Length < 3)
            {
                point.X = 0;
                point.Y = 0;
                point.Z = 0;
                return;
            }
            else
            {
                point.X = double.Parse(tokens[0]);
                point.Y = double.Parse(tokens[1]);
                point.Z = double.Parse(tokens[2]);
            }

            _Points.Add(point);
            BoundingBox.Expand(point);
        }

        public void AddFace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value");
            }

            _FaceIndices.Add(value
                       .Split(' ')
                       .Select(x => int.TryParse(x.Split('/')[0], out int y) ? y - VertexIndex : -1)
                       .ToArray());
        }

        public IShape CreateInstance()
        {
            var constructor = Type.GetConstructor(new Type[] { typeof(ShapeData) });
            return constructor?.Invoke(new object[] { this }) as IShape;
        }
    }
}