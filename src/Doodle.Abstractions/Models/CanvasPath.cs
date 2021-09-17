using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Doodle.Abstractions.Models
{

    public class CanvasPath
    {

        [JsonPropertyName("i")]
        public string Id { get; set; }

        [JsonPropertyName("s")]
        public int BrushSize { get; set; }

        [JsonPropertyName("c")]
        public string BrushColor { get; set; }

        [JsonPropertyName("p")]
        public IEnumerable<CanvasPathPoint> Points { get; set; }

        [JsonPropertyName("t")]
        public long Created { get; set; }
        
    }

}