using System;
using System.Collections.Generic;

namespace Doodle.Abstractions.Models
{

    public class CanvasPath
    {

        public string Id { get; set; }

        public int BrushSize { get; set; }

        public string BrushColor { get; set; }

        public bool Display { get; set; }

        public IEnumerable<CanvasPathPoint> Points { get; set; }

        public DateTime Created { get; set; }
        
    }

}