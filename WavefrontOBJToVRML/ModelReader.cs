using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            ShapeData shapeDatum = new ShapeData(ShapeType.PointSet, 1);

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
                        shapeDatum = new ShapeData(GetShapeType(value), shapeDatum.VertexIndex + shapeDatum.Points.Count());
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
                switch (shapeDatum.Type)
                {
                    case ShapeType.Box:
                        shapes.Add(new Box(shapeDatum));
                        break;

                    case ShapeType.Cylinder:
                        shapes.Add(new Cylinder(shapeDatum));
                        break;

                    case ShapeType.PointSet:
                        shapes.Add(new PointSet(shapeDatum));
                        break;
                }
            }

            return shapes;
        }

        static ShapeType GetShapeType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ShapeType.PointSet;
            }

            if (name.Contains("立方体"))
            {
                return ShapeType.Box;
            }

            if (name.Contains("円柱"))
            {
                return ShapeType.Cylinder;
            }

            return ShapeType.PointSet;
        }
    }
}
