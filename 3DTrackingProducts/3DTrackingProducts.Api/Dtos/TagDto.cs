using System;
namespace _3DTrackingProducts.Api.Dtos
{
	public class TagDto
	{
		public string Epc { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }

        public TagDto(string epc, string category, string description)
        {
            Epc = epc;
            Category = category;
            Description = description;
        }
    }
}

