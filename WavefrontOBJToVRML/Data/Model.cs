using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Model
    {
        public string Name { get; }

        readonly IEnumerable<Material> Materials;
        readonly IEnumerable<IShape> Children;

        static readonly Vector DefaultTranslation = default;

        public Model(string name, IEnumerable<Material> materials, IEnumerable<IShape> children)
        {
            Name = name;
            Materials = materials;
            Children = children;
        }

        public IEnumerable<string> GetModelLines()
        {
            List<string> lines = new List<string>();

            foreach (var material in Materials)
            {
                lines.AddRange(material.GetMaterialLines());
            }

            lines.Add("");
            lines.Add($"########## {Name} {new string('#', 50 - Name.Length)}");
            lines.Add($"DEF {Name} Group {{");
            lines.Add("\tchildren [");

            foreach (var shape in Children)
            {
                lines.AddRange(GetShapeLines(shape).PrependEach("\t\t"));
            }

            lines.Add("\t]");
            lines.Add("}");

            return lines;
        }

        public IEnumerable<string> GetShapeLines(IShape shape)
        {
            List<string> lines = new List<string>();

            lines.Add("Transform {");

            Vector translation = shape.Translation.Round();
            if (!DefaultTranslation.Equals(translation))
            {
                lines.Add($"\ttranslation {translation.X} {translation.Y} {translation.Z}");
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
