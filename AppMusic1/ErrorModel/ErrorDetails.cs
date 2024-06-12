using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppMusic1.ErrorModel
{
    public class ErrorDetails
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
