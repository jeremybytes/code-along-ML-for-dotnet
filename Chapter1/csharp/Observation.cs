namespace csharp
{
    public class Observation
    {
        public Observation(string label, int[] pixels)
        {
            Label = label;
            Pixels = pixels;
        }

        public string Label { get; private set; }
        public int[] Pixels { get; private set; }
    }
}