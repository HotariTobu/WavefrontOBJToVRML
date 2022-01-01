using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Material
    {
        //public string EmissiveColor;
        public string DiffuseColor;
        public string SpecularColor;
        public double Transparency;

        string Name;
        
        public Material(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetMaterialLines()
        {
            List<string> lines = new List<string>();

            lines.Add($"DEF {Name} Appearance {{");
            lines.Add("\tmaterial Material {");

            //lines.Add($"\t\temissiveColor {EmissiveColor}");
            lines.Add($"\t\tdiffuseColor {DiffuseColor}");
            lines.Add($"\t\tspecularColor {SpecularColor}");

            if (Transparency != 0)
            {
                lines.Add($"\t\ttransparency {Transparency}");
            }

            lines.Add("\t}");
            lines.Add("}");

            return lines;
        }
    }
}