using System;
using System.Collections.Generic;
using System.IO;

namespace WavefrontOBJToVRML
{
    class Program
    {

        static void Main(string[] args)
        {
            bool isUnion = false;
            string unionPath = "";
            List<Model> models = new List<Model>();

            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    string arg = args[i];
                    switch (arg)
                    {
                        case "-u":
                            if (isUnion)
                            {
                                union();
                            }
                            isUnion = true;
                            unionPath = args[i + 1];
                            i++;
                            continue;
                    }

                    Console.WriteLine(arg);

                    Model model = ModelReader.ReadModel(arg);

                    if (isUnion)
                    {
                        models.Add(model);
                    }
                    else
                    {
                        string parent = Path.GetDirectoryName(arg);
                        string name = Path.GetFileNameWithoutExtension(arg);
                        string path = Path.Combine(parent, $"{name}.wrl");

                        if (File.Exists(path))
                        {
                            Console.WriteLine($"overwrite {path}");
                        }

                        ModelWriter.WriteModel(path, model);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.ReadLine();
                }
            }

            if (isUnion)
            {
                try
                {

                    union();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Console.ReadLine();
                }
            }

            void union()
            {
                Console.WriteLine($"union {unionPath}");
                ModelWriter.WriteModel(unionPath, models.ToArray());
            }
        }
    }
}
