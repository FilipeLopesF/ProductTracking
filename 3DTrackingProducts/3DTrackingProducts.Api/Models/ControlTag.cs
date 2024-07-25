using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace _3DTrackingProducts.Api.Models
{
	public class ControlTag
	{
        [Key]
        [Required]
        public string EPC { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public double PositionX { get; set; }

        [Required]
        public double PositionY{ get; set; }

        [ForeignKey("RoomId")]
        [JsonIgnore]
        public virtual Room? Room { get; set; }

        public virtual ICollection<Tag3DPosition> Tag3DPositionsLeft { get; set; } = new List<Tag3DPosition>();

        public virtual ICollection<Tag3DPosition> Tag3DPositionsRight{ get; set; } = new List<Tag3DPosition>();

    }
}

