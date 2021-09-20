using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Doodle.State;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components.Canvas
{

    public partial class CanvasComponent : Shared.DoodleBaseComponent
    {
        
        #region Members
        private bool _disposed = false;
        
        [Inject]
        private ILogger<CanvasComponent> Logger { get; set; }

        private ElementReference ResizeElement { get; set; }
        
        private ElementReference CanvasElement { get; set; }

        private string CanvasCommandHiddenValue { get; set; }
        #endregion

        #region Event Callbacks
        [Parameter]
        public EventCallback<IEnumerable<Abstractions.Models.CanvasPath>> OnCommandPathsUpdated { get; set; }

        [Parameter]
        public EventCallback OnCanvasUpdated { get; set; }

        [Parameter]
        public EventCallback OnCanvasReady { get; set; }
       
        [Parameter]
        public EventCallback OnRedrawCompleted { get; set; }
        #endregion

        #region Properties
  
        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        public bool CanvasInitialised { get; private set; }

        public IEnumerable<Abstractions.Models.CanvasPath> PathCommands { get; private set; }
        #endregion

        #region Config Init
        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            
        }

        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnStrokeColorChanged += (s, strokeColor) => {
                this.SetBrushColor(strokeColor).ConfigureAwait(false);
            };  

            this.DoodleDrawInteraction.OnStrokeWidthChanged += (s, strokeWidth) => {
                this.SetBrushWidth(strokeWidth).ConfigureAwait(false);
            };

            this.DoodleDrawInteraction.OnCanvasGridColorChanged += (s, gridColor) => {
                this.SetGridColor(gridColor).ConfigureAwait(false);
            };

            this.DoodleDrawInteraction.OnCanvasGridSizeChanged += (s, gridSize) => {
                this.SetGridSize(gridSize).ConfigureAwait(false);
            };

            this.DoodleDrawInteraction.OnCanvasGridTypeChanged += (s, gridType) => {
                this.SetGridType(gridType).ConfigureAwait(false);
            };

             this.DoodleDrawInteraction.OnUpdateResolutionChanged += (s, updateResolution) => {
                this.SetUpdateResolution(updateResolution).ConfigureAwait(false);
            };
            
            this.DoodleDrawInteraction.DoodleStateManager.OnRestoreState += (s, e) =>  {
                if (string.IsNullOrEmpty(DoodleDrawInteraction.DoodleStateManager?.CanvasState?.Detail)) {
                    this.Restore("").ConfigureAwait(false);
                } 
                else {
                    this.Restore(DoodleDrawInteraction.DoodleStateManager.CanvasState.Detail).ConfigureAwait(false);
                }
            };

            this.DoodleDrawInteraction.OnEraserSizeChanged += (s, size) => {
                this.SetEraserSize(size).ConfigureAwait(false);
            };

            this.DoodleDrawInteraction.OnDrawTypeChanged += (s, drawType) => {
                switch (drawType)
                {
                    case Abstractions.Common.DrawType.Pen:
                    case Abstractions.Common.DrawType.Eraser:
                    case Abstractions.Common.DrawType.Line:
                    {
                        this.SetDrawType(drawType).ConfigureAwait(false);
                        break;
                    }
                    default:
                        break;
                }
                
            };
            base.OnInitialized();
        }
        #endregion

        #region Overload Methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true && CanvasInitialised == false)
            {
                await JsInteropCanvas.InitialiseCanvas(CanvasElement, ResizeElement, 
                    this.DoodleDrawInteraction.StrokeColor, 
                    (int)this.DoodleDrawInteraction.StrokeWidth, 
                    this.DoodleDrawInteraction.GridSize, 
                    this.DoodleDrawInteraction.GridColor, 
                    this.DoodleDrawInteraction.GridType,
                    this.DoodleDrawInteraction.DrawType,
                    this.DoodleDrawInteraction.EraserColor,
                    this.DoodleDrawInteraction.UpdateResolution);

                this.JsInteropCanvas.CanvasCommandsUpdated += async (s, e) => {
                    await UpdateState();
                };

                this.CanvasInitialised = true;
                await OnCanvasReady.InvokeAsync();
            }
        }
        #endregion

        #region Interop Methods
        private async Task SetDrawType(Abstractions.Common.DrawType drawType)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetDrawType(drawType);
            }
        }

        private async Task SetGridColor(string color)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetGridColor(color);
            }
        }

        private async Task SetGridSize(double size)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetGridSize((int)size);
            }
        }

        private async Task SetGridType(Abstractions.Common.GridType gridType)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetGridType(gridType);
            }
        }

        private async Task SetUpdateResolution(int updateResolution)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetUpdateResolution(updateResolution);
            }
        }

        public async Task SetBrushColor(string color)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetBrushColor(color);
            }
        }

        public async Task SetBrushWidth(double width)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetBrushSize((int)width);
            }
        }

        public async Task SetEraserSize(double size)
        {
            if (this.CanvasInitialised)
            {
                await this.JsInteropCanvas.SetEraserSize((int)size);
            }
        }

        public async Task ClearCanvas()
        {
            this.CanvasCommandHiddenValue = string.Empty;
            await this.JsInteropCanvas.Clear();
        }

        private async Task Restore(string restoreCommandsJson)
        {
            if (string.IsNullOrEmpty(restoreCommandsJson) || restoreCommandsJson.Trim() == "")
                Logger.LogWarning($"Restoring canvas with no commands");
            else
                Logger.LogDebug($"Restoring canvas with Json Commands {restoreCommandsJson}");

            await this.JsInteropCanvas.Restore(restoreCommandsJson);
        }

        public async Task Redraw()
        {
            await this.JsInteropCanvas.Refresh();
            await OnRedrawCompleted.InvokeAsync();
        }
        #endregion

        #region Update Methods
        private async Task UpdateState()
        {
            this.PathCommands = JsonConverters.Serialization.Deserialize<List<Abstractions.Models.CanvasPath>>(this.CanvasCommandHiddenValue);
            await this.DoodleDrawInteraction.DoodleStateManager.PushCanvasState(new CanvasState(this.PathCommands));
            await this.OnCommandPathsUpdated.InvokeAsync(this.PathCommands);
            await this.OnCanvasUpdated.InvokeAsync();

            StateHasChanged();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false && disposing)
            {
                _disposed = true;
            }
        }
        #endregion

    }



}