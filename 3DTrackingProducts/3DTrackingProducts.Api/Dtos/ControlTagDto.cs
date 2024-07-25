using System;
using _3DTrackingProducts.Api.Models;

namespace _3DTrackingProducts.Api.Dtos
{
	public class ControlTagDto
	{
		public string Epc { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public RoomDto Room { get; set; }
    }
}

