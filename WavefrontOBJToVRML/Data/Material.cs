using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Material
    {
        //public string EmissiveColor;
        public Color DiffuseColor;
        public Color SpecularColor;
        public double Transparency;

        readonly string Name;

        static readonly Color DefaultDiffuseColor = new Color { R = 1, G = 1, B = 1 };
        static readonly Color DefaultSpecularColor = default;
        
        public Material(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetMaterialLines()
        {
            Color diffuseColor = DiffuseColor.Round();
            Color specularColor = SpecularColor.Round();
            double transparency = Transparency.Round();

            List<string> lines = new List<string>();

            lines.Add("Shape {");
            lines.Add($"\tappearance DEF {Name} Appearance {{");
            lines.Add("\t\tmaterial Material {");

            //lines.Add($"\t\temissiveColor {EmissiveColor}");

            if (!diffuseColor.Equals(DefaultDiffuseColor))
            {
                lines.Add($"\t\t\tdiffuseColor {diffuseColor}");
            }

            if (!specularColor.Equals(DefaultSpecularColor))
            {
                lines.Add($"\t\t\tspecularColor {specularColor}");
            }

            if (transparency != 0)
            {
                lines.Add($"\t\t\ttransparency {transparency}");
            }

            lines.Add("\t\t}");
            lines.Add("\t}");
            lines.Add("}");

            return lines;
        }
    }
}