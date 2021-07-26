using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{

    public partial class DoodleDraw : ComponentBase
    {

        [Parameter]
        public ElementReference CanvasElement { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCommon JsInteropCommon { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }


    }

}