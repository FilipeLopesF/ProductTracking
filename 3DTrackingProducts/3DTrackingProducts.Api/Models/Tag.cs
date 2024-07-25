using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public string EPC { get; set; }

        [JsonIgnore]
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        public string ? Description { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public virtual Category ? Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

        [JsonIgnore]
        public virtual ICollection<TagPosition> TagPositions  { get; set; } = new List<TagPosition>();
    }
}
