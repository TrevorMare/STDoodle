namespace Doodle.Abstractions.Config
{

    public class DoodleDrawConfig
    {
        public Interfaces.ITheme Theme { get; set; }

        public int DefaultStrokeSize { get; set; } = 1;

        public int DefaultEraserSize { get; set; } = 5;

        public string DefaultStrokeColor { get; set; } = "#000000";

        public string BackgroundColor { get; set; } = "#ffffff";

        public ColorPickerConfig ColorPickerConfig { get; set; } = new ColorPickerConfig();

        public SizePickerConfig SizePickerConfig { get; set; } = new SizePickerConfig();

        public CanvasConfig CanvasConfig { get; set; } = new CanvasConfig();

        public Html2CanvasConfig Html2CanvasConfig { get; set; } = new Html2CanvasConfig();

        public ToolbarConfig ToolbarConfig { get; set; } = new ToolbarConfig();

        public ResizableElementConfig ResizableElementConfig { get; set; } = new ResizableElementConfig();

        public BackgroundConfig BackgroundConfig { get; set; } = new BackgroundConfig();
        
    }

}