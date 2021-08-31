namespace Doodle.Abstractions.Config
{

    public class ResizableElementConfig
    {

        /// <summary>
        /// Gets or sets a value indicating if the element can be moved
        /// </summary>
        /// <value></value>
        public bool AllowMove { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the element can be resized
        /// </summary>
        /// <value></value>
        public bool AllowResize { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the element should internally handle the select commands
        /// </summary>
        /// <value></value>
        public bool AutoHandleEvents { get; set; } = true;

        /// <summary>
        /// Gets or sets the initial height of the element
        /// </summary>
        /// <value></value>
        public double Height { get; set; } = 20;

        /// <summary>
        /// Gets or sets the initial width of the element
        /// </summary>
        /// <value></value>
        public double Width { get; set; } = 100;

        /// <summary>
        /// Gets or sets the initial top of the element
        /// </summary>
        /// <value></value>
        public double Top { get; set; } = 50;

        /// <summary>
        /// Gets or sets the initial left of the element
        /// </summary>
        /// <value></value>
        public double Left { get; set; } = 50;

        /// <summary>
        /// Gets or sets the minimum width of the element
        /// </summary>
        /// <value></value>
        public double? MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the minimum height of the element
        /// </summary>
        /// <value></value>
        public double? MinHeight { get; set; }

    }

}