using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Box : IShape
    {
        public string AppearanceName { get; }
        public Point Translation { get; }
        public Rotation Rotation { get; }

        readonly Size Size;

        static readonly Size defaultSize = new Size
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
        }

        public IEnumerable<string> Geometry
        {
            get
            {
                List<string> lines = new List<string>();

                if (defaultSize.Equals(Size))
                {
                    return new string[] { "geometry Box {}" };
                }

                lines.Add("geometry Box {");
                lines.Add($"\tsize {Size.Width} {Size.Height} {Size.Depth}");
                lines.Add("}");

                return lines;
            }
        }
    }
}