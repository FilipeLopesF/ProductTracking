using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _3DTrackingProducts.Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
