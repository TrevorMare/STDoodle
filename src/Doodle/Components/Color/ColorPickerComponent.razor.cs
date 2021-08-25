using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components.Color
{

    public partial class ColorPickerComponent : Shared.DoodleBaseComponent
    {
 
        #region Members
        public ElementReference ColorPicker { get; set; }
        #endregion

        #region "Parameters"
        [Parameter]
        public string SelectedColor 
        { 
            get => DoodleDrawInteraction.StrokeColor; 
            set 
            {
                DoodleDrawInteraction.SetStrokeColor(value).ConfigureAwait(false);
            } 
        }

        [Parameter]
        public IEnumerable<string> FavouriteColors { get; set; } 
       
        [Parameter]
        public string WrapperClass { get; set; }

        [Parameter]
        public string FavouriteWrapperClass { get; set; }

        [Parameter]
        public string FavouriteOuterClass { get; set; }

        [Parameter]
        public string FavouriteInnerClass { get; set; }

        [Parameter]
        public string CustomWrapperClass { get; set; }

        [Parameter]
        public string ColorInputClass { get; set; }

        [Parameter]
        public bool ShowCustomColor { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCommon JsInteropCommon { get; set; }

        [Parameter]
        public EventCallback OnCloseMenu { get; set; }
        #endregion

        #region Config Init
        private async Task OpenColorPicker()
        {
            await JsInteropCommon.ClickElement(ColorPicker);
        }

        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.ColorPickerConfig == null) return;
            this.FavouriteColors = config.ColorPickerConfig.FavouriteColors ?? new List<string>();
            this.WrapperClass = config.ColorPickerConfig.WrapperClass;
            this.FavouriteWrapperClass = config.ColorPickerConfig.FavouriteWrapperClass;
            this.FavouriteOuterClass = config.ColorPickerConfig.FavouriteOuterClass;
            this.FavouriteInnerClass = config.ColorPickerConfig.FavouriteInnerClass;
            this.CustomWrapperClass = config.ColorPickerConfig.CustomWrapperClass;
            this.ColorInputClass = config.ColorPickerConfig.ColorInputClass;
            this.ShowCustomColor = config.ColorPickerConfig.ShowCustomColor;
        }
        #endregion 

    }

}