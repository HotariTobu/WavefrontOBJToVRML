using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Model
    {
        readonly string Name;
        readonly IEnumerable<Material> Materials;
        readonly IEnumerable<IShape> Children;

        static readonly Point DefaultTranslation = new Point();

        public Model(string name, IEnumerable<Material> materials, IEnumerable<IShape> children)
        {
            Name = name;
            Materials = materials;
            Children = children;
        }

        public IEnumerable<string> GetModelLines()
        {
            List<string> lines = new List<string>();

            lines.Add("Switch {");
            lines.Add("\tchoice [");

            foreach (var material in Materials)
            {
                lines.AddRange(material.GetMaterialLines().PrependEach("\t\t"));
            }

            lines.Add("");
            lines.Add($"\t\t########## {Name} {new string('#', 50 - Name.Length)}");
            lines.Add($"\t\tDEF {Name} Group {{");
            lines.Add("\t\t\tchildren [");

            foreach (var shape in Children)
            {
                lines.AddRange(GetShapeLines(shape).PrependEach("\t\t\t\t"));
            }

            lines.Add("\t\t\t]");
            lines.Add("\t\t}");

            lines.Add("\t]");
            lines.Add("}");

            return lines;
        }

        public IEnumerable<string> GetShapeLines(IShape shape)
        {
            List<string> lines = new List<string>();

            lines.Add("Transform {");

            if (!DefaultTranslation.Equals(shape.Translation))
            {
                lines.Add($"\ttranslation {shape.Translation.X} {shape.Translation.Y} {shape.Translation.Z}");
            }

            if (shape.Rotation.Angle != 0)
            {
                lines.Add($"\trotation {shape.Rotation.X} {shape.Rotation.Y} {shape.Rotation.Z} {shape.Rotation.Angle}");
            }

            lines.Add("\tchildren [");
            lines.Add("\t\tShape {");

            lines.AddRange(shape.Geometry.PrependEach("\t\t\t"));

            if (!string.IsNullOrEmpty(shape.AppearanceName))
            {
                lines.Add($"\t\t\tappearance USE {shape.AppearanceName}");
            }

            lines.Add("\t\t}");
            lines.Add("\t]");
            lines.Add("}");

            return lines;
        }
    }
}
