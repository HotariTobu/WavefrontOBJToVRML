using System;
using System.IO;

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
