namespace Doodle.Abstractions.Common
{

    public enum Orientation
    {
        Vertical = 1,
        Horizontal = 2
    }

    public enum GridType
    {
        None = 0,
        Grid = 1,
        Point = 2
    }

    public enum BackgroundSourceType
    {
        Url = 1,
        SVG = 2
    }

    public enum ResizableContentType
    {
        Text = 1,
        Image = 2
    }

    public enum DrawMode
    {
        Canvas = 1,
        Resizable = 2
    }

}