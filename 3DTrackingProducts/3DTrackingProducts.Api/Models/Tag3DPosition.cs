using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace _3DTrackingProducts.Api.Models
{
	public class Tag3DPosition
	{
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string TagEPC { get; set; }

        public string ControlTagEPCLeft { get; set; }

        public string ControlTagEPCRight { get; set; }

        public double RelativePosX { get; set; } //relative x

        public double RelativePosY { get; set; } //relative y

        public double RelativePosZ { get; set; } //relative z

        public double DistanceLeft { get; set; }

        public double DistanceRight { get; set; }

        public double DistanceHigh { get; set; }

        public DateTime DateTimeRegistered { get; set; } = DateTime.Now;

        [ForeignKey("TagEPC")]
        public virtual Tag Tag { get; set; }

        public virtual ControlTag ControlTagLeft { get; set; }

        public virtual ControlTag ControlTagRight { get; set; }
    }
}

