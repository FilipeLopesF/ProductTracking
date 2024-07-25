using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace _3DTrackingProducts.Api.Models
{
    public class Room
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public byte[] ? imageByte { get; set; }

        [JsonIgnore]
        public virtual ICollection<PairAntenna> antennas { get; } = new List<PairAntenna>();
    }
}
