using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.ColorPicker
{

    public partial class ColorPickerComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        [Parameter]
        public bool IsOpen { get; set; }

        #endregion

    }

}