using System;
namespace _3DTrackingProducts.Api.Resources
{
    public class ControlTagResource
    {
        public string Epc { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public string Description { get; set; }
        public string RoomId { get; set; }
    }
}

