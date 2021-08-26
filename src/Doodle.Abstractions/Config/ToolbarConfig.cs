using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class ToolbarConfig
    {

        /// <summary>
        /// Gets or sets a value indicating if the toolbar should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowToolbar { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the canvas grid toolbar button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowCanvasGrid { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the color picker toolbar button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowColorPicker { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the stroke size picker toolbar button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowSizePicker { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw types should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawType { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw type erasor button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawTypeEraser { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw type pen button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawTypePen { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw type line button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawTypeLine { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw type resizable text button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawTypeResizableText { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw type resizable text button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawTypeResizableImage { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw actions should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the draw actions undo button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActionsUndo { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw actions red button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActionsRedo { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw actions clear button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActionsClear { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if the draw actions save button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActionsSave { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating if the draw actions export button should be displayed
        /// </summary>
        /// <value></value>
        public bool ShowDrawActionsExport { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating if drawing history should be cleared on the clear button click
        /// </summary>
        /// <value></value>
        public bool ClearHistoryOnClear { get; set; } = true;

    }

}