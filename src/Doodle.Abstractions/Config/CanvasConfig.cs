namespace Doodle.Abstractions.Config
{

    public class CanvasConfig
    {

        /// <summary>
        /// Gets or sets the initial grid size
        /// </summary>
        /// <value></value>
        public int GridSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the initial Grid Color
        /// </summary>
        /// <returns></returns>
        public string GridColor { get; set; } = "rgb(166, 241, 169)";

        /// <summary>
        /// Gets or sets the initial Grid Type
        /// </summary>
        /// <value></value>
        public Abstractions.Common.GridType GridType { get; set; } = Abstractions.Common.GridType.Grid;

        /// <summary>
        /// Gets or sets the update resolution for the canvas points
        /// </summary>
        /// <value></value>
        public int UpdateResolution { get; set; } = 2;

    }

}