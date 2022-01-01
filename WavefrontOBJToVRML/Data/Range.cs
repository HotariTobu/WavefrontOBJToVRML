namespace WavefrontOBJToVRML
{
    internal struct Range
    {
        public double Min, Max;

        public double Length => Max - Min;
        public double Center => (Min + Max) / 2;

        bool IsExpanded;

        public void Expand(double value)
        {
            if (IsExpanded)
            {
                if (value < Min)
                {
                    Min = value;
                }
                else if (value > Max)
                {
                    Max = value;
                }
            }
            else
            {
                Min = value;
                Max = value;
                IsExpanded = true;
            }
        }

        public override string ToString() => $"{Min} - {Max}";
    }
}
