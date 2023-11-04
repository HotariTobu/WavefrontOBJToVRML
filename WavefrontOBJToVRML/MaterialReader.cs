using System;
using System.Collections.Generic;
using System.IO;

namespace WavefrontOBJToVRML
{
    internal class MaterialReader
    {
        public static IEnumerable<Material> ReadMaterial(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ArgumentException("path");
            }

            List<Material> result = new List<Material>();
            Material material = new Material("");

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
                    case "newmtl":
                        material = new Material(value);
                        result.Add(material);
                        break;
                    /*case "Ka":
                        material.EmissiveColor = value;
                        break;*/
                    case "Kd":
                        material.DiffuseColor = parse(value);
                        break;
                    case "Ks":
                        material.SpecularColor = parse(value);
                        break;
                    case "d":
                        {
                            if (double.TryParse(value, out double d))
                            {
                                material.Transparency = 1 - d;
                            }
                        }
                        break;
                    case "map_Kd":
                        material.
                        break;
                }
            }

            return result;

            Color parse(string value)
            {
                string[] tokens = value.Split(' ');
                if (tokens.Length == 3)
                {
                    return new Color
                    {
                        R = double.Parse(tokens[0]),
                        G = double.Parse(tokens[1]),
                        B = double.Parse(tokens[2]),
                    };
                }

                return default;
            }
        }
    }
}
