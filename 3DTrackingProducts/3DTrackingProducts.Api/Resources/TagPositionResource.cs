namespace _3DTrackingProducts.Api.Resources
{
    public class TagPositionResource
    {
        public string TagEPC { get; set; }

        public double x { get; set; }

        public double y { get; set; }

        public double angleAntenna01 { get; set; }

        public double angleAntenna02 { get; set; }

        public Guid ParAntennaId { get; set; }
    }
}
