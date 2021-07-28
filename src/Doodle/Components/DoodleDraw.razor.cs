using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
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

        [Inject]
        public Abstractions.JsInterop.IJsInteropBuffer JsInteropBuffer { get; set; }

        private string imgSource = "";

        protected override async Task OnParametersSetAsync()
        {

            await JsInteropCanvas.InitialiseCanvas(CanvasElement);

        }

        public async Task RenderTest()
        {

            var bufferId = await JsInteropCanvas.RenderCanvasToImage(RenderWrapper);

            var base64Image = await JsInteropBuffer.ReadBuffer(bufferId);
            this.imgSource = base64Image;
            
            StateHasChanged();
        }


    }

}