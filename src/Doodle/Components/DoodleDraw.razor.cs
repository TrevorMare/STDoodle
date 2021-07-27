using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Doodle.Components 
{

    public partial class DoodleDraw : ComponentBase
    {

        [Parameter]
        public ElementReference CanvasElement { get; set; }

        [Parameter]
        public ElementReference RenderWrapper { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCommon JsInteropCommon { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        protected override async Task OnParametersSetAsync()
        {

            await JsInteropCanvas.InitialiseCanvas(CanvasElement);

        }

        public async Task RenderTest()
        {
            try
            {
                var bufferId = await JsInteropCanvas.RenderCanvasToImage(RenderWrapper);

                var buffer = await JsInteropCanvas.ReadBufferedImage(bufferId);

            }
            catch (Exception ex)
            {

            }
        }


    }

}