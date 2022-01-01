namespace WavefrontOBJToVRML
{
    internal struct Size
    {
        public double Width, Height, Depth;

        public override string ToString() => $"{Width}x{Height}x{Depth}";

        public override bool Equals(object obj)
        {
            Size size = (Size)obj;
            return size.Width == Width && size.Height == Height && size.Depth == Depth;
        }

        public override int GetHashCode()
        {
            return (int)((Width + Height + Depth) * 1000000);
        }
    }
}
