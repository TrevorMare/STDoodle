using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doodle.Components 
{

    public partial class DoodleDraw : ComponentBase
    {

        [Inject]
        private ILogger<DoodleDraw> _logger { get; set;}

        [Parameter]
        public ElementReference RenderWrapper { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropBuffer JsInteropBuffer { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropHtml2Canvas JsInteropHtml2Canvas { get; set; }

        private string imgSource = "";

        public async Task<string> ExportDoodleToImage(Abstractions.Config.Html2CanvasConfig config = null)
        {
            try
            {
                _logger.LogInformation($"Exporting Doodle to image.");

                var bufferId = await JsInteropHtml2Canvas.WriteElementImageToBuffer(RenderWrapper, config);
                if (string.IsNullOrEmpty(bufferId))
                {
                    throw new Exception("No buffer Id returned for the exported image.");
                }

                _logger.LogInformation($"Buffer with Id {bufferId} setup for export.");
                _logger.LogInformation($"Reading stream data from buffer.");

                var base64ImageData = await JsInteropBuffer.ReadBuffer(bufferId);

                this.imgSource = base64ImageData;
                StateHasChanged();

                return base64ImageData;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occured exporting the image");
                throw;
            }
        }

    }

}