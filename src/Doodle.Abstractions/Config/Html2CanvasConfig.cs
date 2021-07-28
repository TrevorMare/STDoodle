namespace Doodle.Abstractions.Config
{
    public class Html2CanvasConfig
    {
        
        public bool AllowTaint { get; set; } = true;

        public string BackgroundColor { get; set; } = null;

        public bool? ForeignObjectRendering { get; set; }

        public int? ImageTimeout { get; set; }

        public bool? Logging { get; set; }

        public string Proxy { get; set; } = null;

        public bool? RemoveContainer { get; set; }

        public bool UseCORS { get; set; } = true;

        public double? Scale { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }

        public double? ScrollX { get; set; }

        public double? ScrollY { get; set; }

        public double? WindowHeight { get; set; }

        public double? WindowWidth { get; set; }

    }
}