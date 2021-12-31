using System;
using System.IO;

namespace WavefrontOBJToVRML
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);

                try
                {
                    Model model = ModelReader.ReadModel(arg);
                    string parent = Path.GetDirectoryName(arg);
                    string name = Path.GetFileNameWithoutExtension(arg);
                    string path = Path.Combine(parent, $"{name}.wrl");
                    ModelWriter.WriteModel(path, model);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.ReadLine();
                }
            }
        }
    }
}
