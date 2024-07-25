using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Models
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public int RSSI { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public double Angle { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string TagEPC { get; set; }
      
        [ForeignKey("TagEPC")]
        [JsonIgnore]
        public virtual Tag ? Tag { get; set; }
    }
}
