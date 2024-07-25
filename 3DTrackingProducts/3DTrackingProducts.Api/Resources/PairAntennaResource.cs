namespace _3DTrackingProducts.Api.Resources
{
    public class PairAntennaResource
    {
        public string antenna01IP { get; set; }
        public int antenna01X { get; set; }
        public int antenna01Y { get; set; }
        public string antenna02IP { get; set; }
        public int antenna02X { get; set; }
        public int antenna02Y { get; set; }
        public Guid idRoom { get; set; }

    }
}
