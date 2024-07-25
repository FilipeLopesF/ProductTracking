using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Models
{
    public class TagPosition
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TagEPC { get; set; }

        [ForeignKey("TagEPC")]
        [JsonIgnore]
        public virtual Tag ? Tag { get; set; }

        public double x { get; set; }

        public double y { get; set; }

        public double angleAntenna01 { get; set; }

        public double angleAntenna02 { get; set; }

        public Guid PairAntennaId { get; set; }

        [ForeignKey("PairAntennaId")]
        [JsonIgnore]
        public virtual PairAntenna? PairAntenna { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
