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

        private string imgSource = "";

        protected override async Task OnParametersSetAsync()
        {

            await JsInteropCanvas.InitialiseCanvas(CanvasElement);

        }

        public async Task RenderTest()
        {
            var cancellationTimeout = new CancellationTokenSource();
            cancellationTimeout.CancelAfter(TimeSpan.FromSeconds(5));

            var bufferId = await JsInteropCanvas.RenderCanvasToImage(RenderWrapper, cancellationTimeout.Token);

            var base64Image = await JsInteropCanvas.ReadBufferedImage(bufferId);
            this.imgSource = base64Image;
            
            StateHasChanged();
        }


    }

}