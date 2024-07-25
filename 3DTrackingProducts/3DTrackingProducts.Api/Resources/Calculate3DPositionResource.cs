using System;
namespace _3DTrackingProducts.Api.Resources
{
	public class Calculate3DPositionResource
	{
		public string EPC_Left { get; set; }
		public string EPC_Right { get; set; }
        public string EPC_Unknown { get; set; }
        public double Distance_Left { get; set; }
		public double Distance_Right { get; set; }
		public double Distance_Hight { get; set; }
	}
}

