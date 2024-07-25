namespace _3DTrackingProducts.Api.Resources
{
    public class RoomResource
    {
        public string Name { get; set; }
        public double Length { get; set; }

        public double Width { get; set; }

        public IFormFile? imageByte { get; set; }
    }
}
