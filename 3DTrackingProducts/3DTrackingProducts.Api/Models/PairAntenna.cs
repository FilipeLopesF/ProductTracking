using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Models
{
    public class PairAntenna
    {
        [Key]
        public Guid Id { get; set; }
        public string antenna01IP { get; set; }
        public int antenna01X { get; set; }
        public int antenna01Y { get; set; }
        public string antenna02IP { get; set; }
        public int antenna02X { get; set; }
        public int antenna02Y { get; set; }
        public int DetectingState { get; set; } = -1;

        //-1 -> Not verified yet
        //0 -> No Control Tag Detected
        //1 -> Detected Control Tag With Err6ytror
        //2 -> Detected Control Tag With Success
        public DateTime? LastVerificationTimeStamp { get; set; } = null;

        public Guid idRoom { get; set; }

        [ForeignKey("idRoom")]
        [JsonIgnore]
        public virtual Room Room { get; set; } = null!;

    }
}
