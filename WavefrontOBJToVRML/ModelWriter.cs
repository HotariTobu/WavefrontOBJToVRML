using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavefrontOBJToVRML
{
    internal class ModelWriter
    {
        public static void WriteModel(string path, Model model)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("path");
            }

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (File.Exists(path))
            {
                throw new IOException("exist");
            }

            File.WriteAllLines(path, model.GetModelLines());
        }
    }
}
