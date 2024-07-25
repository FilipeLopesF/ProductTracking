using System;
namespace _3DTrackingProducts.Api.Dtos
{
	public class Position3DDto
	{
        public double TagX { get; set; }
        public double TagY { get; set; }
        public double TagZ { get; set; }

        public Position3DDto(double tagX, double tagY, double tagZ)
        {
            TagX = tagX;
            TagY = tagY;
            TagZ = tagZ;
        }

        public Position3DDto()
        {
        }
    }
}

