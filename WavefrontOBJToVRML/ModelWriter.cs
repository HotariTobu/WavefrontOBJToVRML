using System;
using System.Collections.Generic;
using System.IO;

namespace WavefrontOBJToVRML
{
    internal class ModelWriter
    {
        public static void WriteModel(string path, params Model[] models)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("path");
            }

            if (models == null || models.Length == 0)
            {
                throw new ArgumentException("model");
            }

            List<string> lines = new List<string>();

            lines.Add("#VRML V2.0 utf8");
            lines.Add("Switch {");
            lines.Add("\tchoice [");

            foreach (Model model in models)
            {
                lines.AddRange(model.GetModelLines().PrependEach("\t\t"));
                lines.Add("");
            }

            lines.RemoveAt(lines.Count - 1);

            lines.Add("\t]");
            lines.Add("}");

            if (models.Length == 1)
            {
                lines.Add($"USE {models[0].Name}");
            }

            File.WriteAllLines(path, lines);
        }
    }
}
