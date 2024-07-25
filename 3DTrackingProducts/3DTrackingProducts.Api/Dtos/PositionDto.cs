using System;
namespace _3DTrackingProducts.Api.Dtos
{
	public class PositionDto
	{
        public double X_1 { get; set; }
        public double X_2 { get; set; }
        public double LocX { get; set; }
        public double LocY { get; set; }
        public double Xreal { get; set; }
        public double Yreal { get; set; }
        public double XLdireita { get; set; } = 1.75;
        public double XLesquerdo { get; set; } = 5;
        public double YComp { get; set; } = 12.5;
        public int[] Yrssi { get; set; }
        public double[] Yang { get; set; }
        public int[] Yrssi2 { get; set; }
        public double[] Yang2 { get; set; }
    }
}

