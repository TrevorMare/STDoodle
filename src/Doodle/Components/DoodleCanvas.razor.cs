using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components 
{

    public partial class DoodleCanvas : ComponentBase
    {

        [Parameter]
        public ElementReference CanvasElement { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsInteropCanvas.InitialiseCanvas(CanvasElement);
        }

    }
}