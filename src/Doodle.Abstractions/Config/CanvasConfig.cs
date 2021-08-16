namespace Doodle.Abstractions.Config
{

    public class CanvasConfig
    {

        public string CanvasClass { get; set; }

        public int GridSize { get; set; } = 10;

        public string GridColor { get; set; } = "rgb(166, 241, 169)";

        public Abstractions.Common.GridType GridType { get; set; } = Abstractions.Common.GridType.Grid;
    }

}