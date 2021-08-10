namespace Doodle.Abstractions.Config
{

    public class ResizableElementConfig
    {

        public bool AllowMove { get; set; } = true;

        public bool AllowResize { get; set; } = true;

        public bool AutoHandleEvents { get; set; } = true;

        public double Height { get; set; } = 20;

        public double Width { get; set; } = 100;

        public double Top { get; set; } = 50;

        public double Left { get; set; } = 50;

        public double? MinWidth { get; set; }

        public double? MinHeight { get; set; }

        public string ResizeElementClass { get; set; }

        public string MoveAdornerClass { get; set; }

    }

}