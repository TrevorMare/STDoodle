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
        private bool _canUndo = false;
        private bool _canRedo = false;
        private bool _disposed = false;
        
        
        [Inject]
        private ILogger<CanvasComponent> Logger { get; set; }

        private ElementReference ResizeElement { get; set; }
        
        private ElementReference CanvasElement { get; set; }
        #endregion

        #region Event Callbacks
        [Parameter]
        public EventCallback<IEnumerable<Abstractions.Models.CanvasPath>> OnCommandPathsUpdated { get; set; }

        [Parameter]
        public EventCallback OnCanvasUpdated { get; set; }

        [Parameter]
        public EventCallback OnCanvasReady { get; set; }

        [Parameter]
        public EventCallback<bool> OnRedoCompleted { get; set; }

        [Parameter]
        public EventCallback<bool> OnUndoCompleted { get; set; }

        [Parameter]
        public EventCallback OnCanvasCleared { get; set; }

        [Parameter]
        public EventCallback OnCanvasRestored { get; set; }
        
        [Parameter]
        public EventCallback OnRedrawCompleted { get; set; }
        #endregion

        #region Properties
  
        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        public bool CanUndo 
        { 
            get => _canUndo; 
            private set
            {
                if (_canUndo != value)
                {
                    _canUndo = value;
                    this.CanUndoChanged.InvokeAsync(_canUndo);
                }
            } 
        }

        public bool Dirty 
        {
            get => (this.PathCommands?.Count() > 0);
        }

        public EventCallback<bool> CanUndoChanged { get; set; }

        public bool CanRedo 
        { 
            get => _canRedo; 
            private set
            {
                if (_canRedo != value)
                {
                    _canRedo = value;
                    this.CanRedoChanged.InvokeAsync(_canRedo);
                }
            } 
        }

        public EventCallback<bool> CanRedoChanged { get; set; }

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
            
            this.DoodleDrawInteraction.DoodleStateManager.OnRestoreState += (s, e) => 
            {

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
                    this.DoodleDrawInteraction.EraserColor);

                this.JsInteropCanvas.CanvasCommandsUpdated += async (s, e) => {
                    await UpdateState(e);
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
        
        public async ValueTask<bool> Undo()
        {
            if (this.CanUndo)
            {
                Logger.LogDebug($"Undo the previous step");
                var result = await this.JsInteropCanvas.Undo();

                Logger.LogDebug($"Undo reported {result}");
                await OnUndoCompleted.InvokeAsync(result);
                
                return result;
            }
            else
                Logger.LogWarning($"Unable to Undo as there is not more steps.");
            return false;
        }
        
        public async ValueTask<bool> Redo()
        {
            if (this.CanRedo)
            {
                Logger.LogDebug($"Redo the previous step");
                var result = await this.JsInteropCanvas.Redo();

                Logger.LogDebug($"Redo reported {result}");
                await OnRedoCompleted.InvokeAsync(result);

                return result;
            }
            else 
                Logger.LogWarning($"Unable to Redo as there is not more steps.");
            return false;
        }

        public async Task ClearCanvas(bool clearAllCommands)
        {
            Logger.LogDebug($"Clearing canvas - Clear All Commands: {clearAllCommands}");
            await this.JsInteropCanvas.Clear(clearAllCommands);
            await OnCanvasCleared.InvokeAsync();
        }

        public async Task Restore()
        {
            await Restore("");
        }

        public async Task Restore(string restoreCommandsJson)
        {
            if (string.IsNullOrEmpty(restoreCommandsJson) || restoreCommandsJson.Trim() == "")
                Logger.LogWarning($"Restoring canvas with no commands");
            else
                Logger.LogDebug($"Restoring canvas with Json Commands {restoreCommandsJson}");

            await this.JsInteropCanvas.Restore(restoreCommandsJson);
            await OnCanvasRestored.InvokeAsync();
        }

        public async Task Restore(List<Abstractions.Models.CanvasPath> restoreCommands)
        {
            string jsonCommands = JsonSerializer.Serialize(restoreCommands);
            await Restore(jsonCommands);
        }

        public async Task Redraw()
        {
            await this.JsInteropCanvas.Refresh();
            await OnRedrawCompleted.InvokeAsync();
        }
        #endregion

        #region Update Methods
        private async Task UpdateState(List<Abstractions.Models.CanvasPath> drawCommands)
        {
            this.CanUndo = await this.JsInteropCanvas.CanUndo();
            this.CanRedo = await this.JsInteropCanvas.CanRedo();

            this.PathCommands = drawCommands;

            await this.DoodleDrawInteraction.DoodleStateManager.PushCanvasState(new CanvasState(drawCommands));

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
                this.JsInteropCanvas.CanvasCommandsUpdated -= async (s, e) => {
                    await UpdateState(e);
                };
                _disposed = true;
            }
        }
        #endregion

    }



}