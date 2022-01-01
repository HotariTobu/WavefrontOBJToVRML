namespace WavefrontOBJToVRML
{
    internal class BoundingBox
    {
        Range XRange = default;
        Range YRange = default;
        Range ZRange = default;

        public Vector Center => new Vector { X = XRange.Center, Y = YRange.Center, Z = ZRange.Center };
        public Size Size => new Size { Width = XRange.Length, Height = YRange.Length, Depth = ZRange.Length };

        public void Expand(Vector point)
        {
            XRange.Expand(point.X);
            YRange.Expand(point.Y);
            ZRange.Expand(point.Z);
        }
    }
}
