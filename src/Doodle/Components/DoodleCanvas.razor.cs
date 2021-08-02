using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components 
{

    public partial class DoodleCanvas : ComponentBase, IDisposable
    {

        #region Members
        private bool _canUndo = false;
        private bool _canRedo = false;
        private bool _disposed = false;
        private string _strokeColor = "#000000";
        private int _strokeSize = 1;
        private Abstractions.Config.DoodleDrawConfig _config;
        private Abstractions.Config.DoodleDrawConfig _options;

        [Inject]
        private ILogger<DoodleCanvas> Logger { get; set; }

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => _config; 
            set
            {
                if (_config != value)
                {
                    _config = value;
                    InitConfigSettings(value);
                }
            } 
        }
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
        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitConfigSettings(value);
                }
            } 
        }

        [Parameter]
        public string DataAttributeName { get; set; }

        [Parameter]
        public string CanvasClass { get; set; }

        [Inject]
        public Abstractions.JsInterop.IJsInteropCanvas JsInteropCanvas { get; set; }

        [Parameter]
        public string StrokeColor 
        { 
            get => _strokeColor; 
            set 
            {
                if (this._strokeColor != value)
                {
                    this._strokeColor = value;
                    this.SetBrushColor(value).ConfigureAwait(false);
                }
            } 
        }

        [Parameter]
        public int StrokeSize 
        { 
            get => _strokeSize; 
            set 
            {
                if (this._strokeSize != value)
                {
                    this._strokeSize = value;
                    this.SetBrushSize(value);
                }
            } 
        }

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
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null) return;

            this.CanvasClass = config.CanvasConfig?.CanvasClass;
        }
        #endregion

        #region Overload Methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender == true && CanvasInitialised == false)
            {
                Logger.LogDebug($"Initialising Canvas");
                await JsInteropCanvas.InitialiseCanvas(CanvasElement, ResizeElement, this.StrokeColor, this.StrokeSize);

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
            if (this.CanvasInitialised)
            {
                Logger.LogDebug($"Setting brush color to {color}");
                await this.JsInteropCanvas.SetBrushColor(color);
            }
        }

        public async Task SetBrushSize(int size)
        {
            if (this.CanvasInitialised)
            {
                Logger.LogDebug($"Setting brush size to {size}");
                await this.JsInteropCanvas.SetBrushSize(size);
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