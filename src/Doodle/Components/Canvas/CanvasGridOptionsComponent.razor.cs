using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Canvas
{

    public partial class CanvasGridOptionsComponent : Shared.DoodleBaseComponent
    {
        
        #region Members
        private ElementReference ColorPicker { get; set; }
        
        [Inject]
        public Abstractions.JsInterop.IJsInteropCommon JsInteropCommon { get; set; } 

        [Parameter]
        public EventCallback OnCloseMenu { get; set; }
        #endregion

        #region "Methods"
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            base.InitConfigSettings(config);
        }

        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnCanvasGridColorChanged += (s, color) => {
                StateHasChanged();
            };

            base.OnInitialized();
        }

        private async Task OpenColorPicker()
        {
            await JsInteropCommon.ClickElement(ColorPicker);
        }
        #endregion



    }

}