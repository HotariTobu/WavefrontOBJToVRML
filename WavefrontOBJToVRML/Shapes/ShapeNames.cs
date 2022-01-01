using System;
using System.Collections.Generic;
using System.Linq;

namespace WavefrontOBJToVRML
{
    internal class ShapeNames
    {
        static readonly Dictionary<Type, string[]> Table = new Dictionary<Type, string[]>
        {
            { typeof(Box), new string[]{"Cube", "立方体"}},
            { typeof(Cylinder), new string[]{ "Cylinder", "円柱"}},
            { typeof(Cone), new string[]{ "Cone", "円錐"}},
            { typeof(Sphere), new string[]{ "sphere", "球"}},
        };

        public static Type GetShapeType(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (var item in Table)
                {
                    if (item.Value.Any(x => name.Contains(x)))
                    {
                        return item.Key;
                    }
                }
            }

            return typeof(PointSet);
        }
    }
}
