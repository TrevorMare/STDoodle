using System.Text.Json.Serialization;

namespace Doodle.Abstractions.Models
{

    public class CanvasPathPoint
    {

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
        
    }

}