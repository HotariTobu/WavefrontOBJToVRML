using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal partial class ShapeData
    {
        public ShapeType Type { get; }
        public int VertexIndex { get; }
        public string AppearanceName { get; set; } = "";

        public IEnumerable<Point> Points => _Points;
        public IEnumerable<int[]> FaceIndices => _FaceIndices;

        readonly List<Point> _Points = new List<Point>();
        readonly List<int[]> _FaceIndices = new List<int[]>();
        readonly BoundingBox BoundingBox = new BoundingBox();

        public ShapeData(ShapeType type, int vertexIndex)
        {
            Type = type;
            VertexIndex = vertexIndex;
        }

        public void AddVertex(string value)
        {
            Point point = new Point(value);
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

        public Point Center => new Point
        {
            X = BoundingBox.Center.X.Round(),
            Y = BoundingBox.Center.Y.Round(),
            Z = BoundingBox.Center.Z.Round(),
        };

        public Size Size => new Size
        {
            Width = BoundingBox.Size.Width.Round(),
            Height = BoundingBox.Size.Height.Round(),
            Depth = BoundingBox.Size.Depth.Round(),
        };
    }
}