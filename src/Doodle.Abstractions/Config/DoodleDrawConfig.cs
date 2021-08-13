namespace Doodle.Abstractions.Config
{

    public class DoodleDrawConfig
    {
        public int DefaultStrokeSize { get; set; } = 1;

        public string DefaultStrokeColor { get; set; } = "#000000";

        public ColorPickerConfig ColorPickerConfig { get; set; } = new ColorPickerConfig();

        public SizePickerConfig SizePickerConfig { get; set; } = new SizePickerConfig();

        public CanvasConfig  CanvasConfig { get; set; } = new CanvasConfig();

        public Html2CanvasConfig Html2CanvasConfig { get; set; } = new Html2CanvasConfig();

        public ToolbarConfig ToolbarConfig { get; set; } = new ToolbarConfig();

        public ResizableElementConfig ResizableElementConfig { get; set; } = new ResizableElementConfig();

        public ResizableContainerConfig ResizableContainerConfig { get; set; } = new ResizableContainerConfig();

        public BackgroundConfig BackgroundConfig { get; set; } = new BackgroundConfig();
        
    }

}