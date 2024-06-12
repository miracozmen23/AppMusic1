using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppMusic1.Models
{
    public class Singer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
