using Doodle.Abstractions.Config;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.ColorPicker
{

    public partial class ColorPickerComponent : Shared.DoodleBaseComponent
    {

        #region "Properties"
        [Parameter]
        public bool Show { get; set; } = true;
        #endregion

        #region Overrides
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config.ToolbarConfig == null)
            {
                return;
            }
            this.Show = config.ToolbarConfig.ShowColorPicker;
        }
        #endregion
        
    }

}