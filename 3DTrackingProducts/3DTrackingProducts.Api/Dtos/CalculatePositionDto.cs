namespace _3DTrackingProducts.Api.Dtos
{
    public class CalculatePositionDto
    {
        public double locX { get; set; }

        public double locY { get; set; }

        public double angleAntenna01 { get; set; }

        public double angleAntenna02 { get; set; }

        public PairAntennaDto pairAntenna { get; set; }
        public RoomDto room { get; set; }
    }
}
