using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doodle.Components 
{

    public partial class DoodleDraw : ComponentBase
    {

        #region Members
        [Inject]
        private ILogger<DoodleDraw> _logger { get; set;}

        private Abstractions.Config.DoodleDrawConfig _config;

        private Abstractions.Config.DoodleDrawConfig _options;

        private ElementReference RenderWrapper { get; set; }
        
        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => this._config;
            set 
            {
                this._config = value;
                this.InitConfigSettings(this._config);
            } 
        }
        
        [Inject]
        private Abstractions.JsInterop.IJsInteropBuffer JsInteropBuffer { get; set; }

        [Inject]
        private Abstractions.JsInterop.IJsInteropHtml2Canvas JsInteropHtml2Canvas { get; set; }

        private DoodleCanvas DoodleCanvas { get; set; }

        private bool IsResizableContainerActive { get; set; }
        #endregion

        #region Event Callbacks

        [Parameter]
        public EventCallback<string> StrokeColorChanged { get; set; }

        [Parameter]
        public EventCallback<int> StrokeSizeChanged { get; set; }
        #endregion

        #region Parameters
        [Parameter]
        public string DataAttributeName { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set
            {
                if (_options != value)
                {
                    _options = value;
                    this.InitConfigSettings(this._options);
                }
            } 
        }

        [Parameter]
        public string StrokeColor { get; set; }

        [Parameter]
        public int StrokeSize { get; set; }

        [Parameter]
        public IEnumerable<BackgroundData> Backgrounds { get; set; } = new List<BackgroundData> 
        { 
            //new BackgroundData() { DataSource = "./_content/STDoodle/img/mono-kpresenter-kpr.svg" }, 
            //new BackgroundData() { DataSource = "./_content/STDoodle/img/svg1.svg" }, 
            //new BackgroundData() { DataSource = "./_content/STDoodle/img/svg2.svg" }, 
            //new BackgroundData() { DataSource = "./_content/STDoodle/img/svg3.svg" } 
            //new BackgroundData() { DataSource = "./_content/STDoodle/img/svg4.svg", BackgroundClass = "test-svg-stroke" },
            //new BackgroundData() { DataSource = "<svg height='100' width='100'><circle cx='50' cy='50' r='40' stroke='black' stroke-width='3' fill='red' /></svg>", BackgroundClass="test-svg-circle", BackgroundSourceType = Abstractions.Common.BackgroundSourceType.SVG} 
        };
        #endregion
        private string imgSource = "";

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null) return;

            this.StrokeColor = config.DefaultStrokeColor ?? "#000000";
            this.StrokeSize = config.DefaultStrokeSize;
        }
        #endregion

        #region Methods
        
        public async Task<string> ExportDoodleToImage(Abstractions.Config.Html2CanvasConfig config = null)
        {
            try
            {
                _logger.LogInformation($"Exporting Doodle to image.");

                if (config == null)
                {
                    config = _options?.Html2CanvasConfig ?? _config?.Html2CanvasConfig ?? null;
                }

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
        #endregion

    }

}