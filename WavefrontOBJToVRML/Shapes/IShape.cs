using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal interface IShape
    {
        string AppearanceName { get; }
        IEnumerable<string> Transform { get; }
        IEnumerable<string> Geometry { get; }
    }
}
