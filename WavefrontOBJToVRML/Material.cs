using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal class Material
    {
        //public string EmissiveColor;
        public string DiffuseColor;
        public string SpecularColor;
        public string Transparency;

        string Name;
        
        public Material(string name)
        {
            Name = name;
        }

        public List<string> GetMaterialLines()
        {
            List<string> lines = new List<string>();

            lines.Add($"DEF {Name} Appearance {{");
            lines.Add("\tmaterial Material {");

            //lines.Add($"\t\temissiveColor {EmissiveColor}");
            lines.Add($"\t\tdiffuseColor {DiffuseColor}");
            lines.Add($"\t\tspecularColor {SpecularColor}");

            if (double.TryParse(Transparency, out double transparency) && transparency != 0)
            {
                lines.Add($"\t\ttransparency {Transparency}");
            }

            lines.Add("\t}");
            lines.Add("}");

            return lines;
        }
    }
}