namespace WavefrontOBJToVRML
{
    internal class BoundingBox
    {
        Range XRange = default;
        Range YRange = default;
        Range ZRange = default;

        public Point Center => new Point { X = XRange.Center, Y = YRange.Center, Z = ZRange.Center };
        public Size Size => new Size { Width = XRange.Length, Height = YRange.Length, Depth = ZRange.Length };

        public void Expand(Point point)
        {
            XRange.Expand(point.X);
            YRange.Expand(point.Y);
            ZRange.Expand(point.Z);
        }
    }
}
