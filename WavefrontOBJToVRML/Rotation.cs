namespace WavefrontOBJToVRML
{
    internal struct Rotation
    {
        public double X, Y, Z, Angle;

        public Rotation(Vector vector, double angle)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            Angle = angle;
        }
    }
}
