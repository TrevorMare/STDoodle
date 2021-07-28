using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components 
{

    public partial class DoodleCanvas : ComponentBase
    {

        private bool _canvasInitialised = false;

        [Parameter]
        public ElementReference CanvasElement { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }
       
        public bool CanUndo { get; private set; }

        public bool CanRedo { get; private set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == false && _canvasInitialised == false)
            {
                await JsInteropCanvas.InitialiseCanvas(CanvasElement);
                this.JsInteropCanvas.CanvasCommandsUpdated += async (s, e) => {
                    await UpdateState(e);
                };
                this._canvasInitialised = true;
            }
        }

        #region Update Methods
        private async Task UpdateState(List<Abstractions.Models.CanvasPath> drawCommands)
        {
            this.CanUndo = await this.JsInteropCanvas.CanUndo();
            this.CanRedo = await this.JsInteropCanvas.CanRedo();

            StateHasChanged();
        }
        #endregion

    }
}