using System.Collections.Generic;

namespace WavefrontOBJToVRML
{
    internal interface IShape
    {
        string AppearanceName { get; }
        Vector Translation { get; }
        Rotation Rotation { get; }
        IEnumerable<string> Geometry { get; }
    }
}
