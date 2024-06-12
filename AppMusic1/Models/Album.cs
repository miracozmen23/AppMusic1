using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppMusic1.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SingerId { get; set; }
        public Singer Singers { get; set; }

        [JsonIgnore]
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
