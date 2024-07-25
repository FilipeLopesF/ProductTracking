namespace _3DTrackingProducts.Api.Dtos
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public byte[]? imageByte { get; set; }

    }
}
