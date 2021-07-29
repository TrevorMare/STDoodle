using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components 
{

    public partial class DoodleCanvas : ComponentBase, IDisposable
    {

        #region Members
        private bool _disposed = false;

        [Inject]
        private ILogger<DoodleCanvas> Logger { get; set; }
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
        [Parameter]
        public string E2ETestingName { get; set; }

        [Parameter]
        public string CanvasClass { get; set; }

        [Parameter]
        public ElementReference CanvasElement { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        [Parameter]
        public string StrokeColor { get; set; } = "#000000";

        [Parameter]
        public EventCallback StrokeColorChanged { get; set; }
       
        [Parameter]
        public int StrokeSize { get; set; } = 1;
        
        [Parameter]
        public EventCallback StrokeSizeChanged { get; set; }

        public bool CanUndo { get; private set; }

        public bool CanRedo { get; private set; }

        public bool CanvasInitialised { get; private set; }

        public IEnumerable<Abstractions.Models.CanvasPath> PathCommands { get; private set; }
        #endregion

        #region Overload Methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true && CanvasInitialised == false)
            {
                Logger.LogDebug($"Initialising Canvas");
                await JsInteropCanvas.InitialiseCanvas(CanvasElement);

                this.JsInteropCanvas.CanvasCommandsUpdated += async (s, e) => {
                    Logger.LogDebug($"Updating state");
                    await UpdateState(e);
                };
                Logger.LogDebug($"Canvas Initialised");
                this.CanvasInitialised = true;
                await OnCanvasReady.InvokeAsync();
            }
        }
        #endregion

        #region Interop Methods

        public async Task SetBrushColor(string color)
        {
            Logger.LogDebug($"Setting brush color to {color}");
            await this.JsInteropCanvas.SetBrushColor(color);
        }

        public async Task SetBrushSize(int size)
        {
            Logger.LogDebug($"Setting brush size to {size}");
            await this.JsInteropCanvas.SetBrushSize(size);
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
            Logger.LogWarning($"Redrawing the canvas");
            await this.JsInteropCanvas.Refresh();
            Logger.LogWarning($"Redraw canvas complete");
            await OnRedrawCompleted.InvokeAsync();
        }
        #endregion

        #region Update Methods
        private async Task UpdateState(List<Abstractions.Models.CanvasPath> drawCommands)
        {
            this.CanUndo = await this.JsInteropCanvas.CanUndo();
            this.CanRedo = await this.JsInteropCanvas.CanRedo();

            this.PathCommands = drawCommands;

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