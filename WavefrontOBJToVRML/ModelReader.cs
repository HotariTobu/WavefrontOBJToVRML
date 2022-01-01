using System;
using System.Collections.Generic;
using System.IO;

namespace WavefrontOBJToVRML
{
    internal class ModelReader
    {
        public static Model ReadModel(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ArgumentException("path");
            }

            IEnumerable<Material> materials = new List<Material>();
            List<ShapeData> shapeData = new List<ShapeData>();
            ShapeData shapeDatum = new ShapeData(typeof(PointSet), 1);

            foreach (string line in File.ReadAllLines(path))
            {
                int index = line.IndexOf(' ');
                if (index < 1)
                {
                    continue;
                }

                string value = line.Substring(index).Trim();
                switch (line.Substring(0, index))
                {
                    case "v":
                        shapeDatum.AddVertex(value);
                        break;
                        
                    case "f":
                        shapeDatum.AddFace(value);
                        break;

                    case "o":
                        shapeDatum = new ShapeData(ShapeNames.GetShapeType(value), shapeDatum.NextIndex);
                        shapeData.Add(shapeDatum);
                        break;

                    case "usemtl":
                        shapeDatum.AppearanceName = value;
                        break;

                    case "mtllib":
                        materials = MaterialReader.ReadMaterial(Path.Combine(Path.GetDirectoryName(path), value));
                        break;
                }
            }

            return new Model(Path.GetFileNameWithoutExtension(path), materials, GetChildren(shapeData));
        }

        static IEnumerable<IShape> GetChildren(IEnumerable<ShapeData> shapeData)
        {
            List<IShape> shapes = new List<IShape>();

            foreach (var shapeDatum in shapeData)
            {
                IShape shape = shapeDatum.CreateInstance();
                if (shape != null)
                {
                    shapes.Add(shape);
                }
            }

            return shapes;
        }
    }
}
