using Doodle.Abstractions.Config;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.DrawType
{

    public partial class DrawTypeComponent : Shared.DoodleBaseComponent
    {
        
        #region "Properties"
        [Parameter]
        public bool Show { get; set; } = true;

        [Parameter]
        public bool ShowEraser { get; set; }

        [Parameter]
        public bool ShowPen { get; set; }

        [Parameter]
        public bool ShowLine { get; set; }

        [Parameter]
        public bool ShowResizableText { get; set; }

        [Parameter]
        public bool ShowResizableImage { get; set; }
        #endregion

        #region Overrides
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config.ToolbarConfig == null)
            {
                return;
            }
            this.Show = config.ToolbarConfig.ShowDrawType;
            this.ShowEraser = config.ToolbarConfig.ShowDrawTypeEraser;
            this.ShowPen = config.ToolbarConfig.ShowDrawTypePen;
            this.ShowLine = config.ToolbarConfig.ShowDrawTypeLine;
            this.ShowResizableText = config.ToolbarConfig.ShowDrawTypeResizableText;
            this.ShowResizableImage = config.ToolbarConfig.ShowDrawTypeResizableImage;
        }
        #endregion
    }
}