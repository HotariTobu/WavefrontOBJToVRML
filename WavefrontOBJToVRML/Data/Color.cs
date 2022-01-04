namespace WavefrontOBJToVRML
{
    internal struct Color
    {
        public double R, G, B;

        public override string ToString() => $"{R} {G} {B}";

        public override bool Equals(object obj)
        {
            Color color = (Color)obj;
            return color.R == R && color.G == G && color.B == B;
        }

        public override int GetHashCode()
        {
            return (int)((R + G + B) * 1000000);
        }
    }
}
