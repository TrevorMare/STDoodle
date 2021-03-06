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

        [Inject]
        private Abstractions.Interfaces.IDoodleExportHandler DoodleExportHandler { get; set; }

        [Inject]
        private Abstractions.Interfaces.IDoodleSaveHandler DoodleSaveHandler { get; set; }

        private Components.Canvas.CanvasComponent DoodleCanvas { get; set; }

        private bool IsResizableContainerActive => (DoodleDrawInteraction.DrawMode == Abstractions.Common.DrawMode.Resizable);
        #endregion

        #region Parameters
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

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

        [Inject]
        public Abstractions.Interfaces.IDoodleDrawInteraction DoodleDrawInteraction { get; set; }

        public string BackgroundColor => DoodleDrawInteraction.BackgroundColor;

        [Parameter]
        public EventCallback<string> OnExportImage { get; set; }

        [Parameter]
        public EventCallback<string> OnSaveDoodle { get; set; }
        #endregion

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
           
        }
        #endregion

        #region Methods

        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.DoodleStateManager.OnRestoreState += (s, e) => {
                this.StateHasChanged();
            };
            this.DoodleDrawInteraction.DoodleStateManager.OnDoodleDrawStateChanged += (s, e) => {
                this.StateHasChanged();
            };
            this.DoodleDrawInteraction.OnStateHasChanged += (s, e) => {
                StateHasChanged();
            };
            this.DoodleDrawInteraction.OnExportImage += (s, e) => {
                this.ExportDoodleToImage().ConfigureAwait(false);
            };
            this.DoodleDrawInteraction.OnSaveDoodleData += (s, e) => {
                this.SaveCurrentDrawState().ConfigureAwait(false);
            };
            base.OnInitialized();
        }
       
        public async Task<string> SaveCurrentDrawState()
        {
            try
            {

                string jsonData = await this.DoodleSaveHandler.SaveDoodleDraw(this.DoodleDrawInteraction);

                await OnSaveDoodle.InvokeAsync(jsonData);

                return jsonData;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occured saving the data");
                throw;
            }
        }

        public async Task RestoreCurrentDrawState(string json)
        {
            try
            {
                await this.DoodleDrawInteraction.DoodleStateManager.RestoreCurrentState(json);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occured saving the data");
                throw;
            }
        }

        public async Task<string> ExportDoodleToImage(Abstractions.Config.Html2CanvasConfig config = null)
        {
            var originalDrawMode = this.DoodleDrawInteraction.DrawMode;
            var originalDrawType = this.DoodleDrawInteraction.DrawType;
            try
            {

                await this.DoodleDrawInteraction.SetDrawMode(Abstractions.Common.DrawMode.Canvas);
                await this.DoodleDrawInteraction.SetDrawType(Abstractions.Common.DrawType.Pen);

                if (config == null)
                {
                    config = _options?.Html2CanvasConfig ?? _config?.Html2CanvasConfig ?? null;
                }

                var bufferId = await JsInteropHtml2Canvas.WriteElementImageToBuffer(RenderWrapper, config);
                if (string.IsNullOrEmpty(bufferId))
                {
                    throw new Exception("No buffer Id returned for the exported image.");
                }
                var base64ImageData = await JsInteropBuffer.ReadBuffer(bufferId);
                
                await this.DoodleExportHandler.ExportImageBase64(base64ImageData);
                
                await this.OnExportImage.InvokeAsync(base64ImageData);

                return base64ImageData;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occured exporting the image");
                throw;
            }
            finally
            {
                await this.DoodleDrawInteraction.SetDrawMode(originalDrawMode);
                await this.DoodleDrawInteraction.SetDrawType(originalDrawType);
            }
        }
        #endregion

    }

}