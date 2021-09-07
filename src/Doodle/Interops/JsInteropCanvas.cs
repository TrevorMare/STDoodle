using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Doodle.Abstractions.Common;
using Doodle.Abstractions.JsInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Doodle.Interops
{

    public class JsInteropCanvas : Abstractions.JsInterop.IJsInteropCanvas, IAsyncDisposable
    {

        #region Events
        public event OnCanvasUpdatedHandler CanvasCommandsUpdated;
        #endregion

        #region Members
        private const string _basePath = "./_content/STDoodle/js/JsInteropCanvas.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private readonly DotNetObjectReference<JsInteropCanvas> _thisRef;
        #endregion

        #region ctor
        public JsInteropCanvas(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
            _thisRef = DotNetObjectReference.Create(this);
        }
        #endregion

        #region Interface Methods
        public async Task InitialiseCanvas(ElementReference forElement, ElementReference resizeElement, string brushColor, int brushSize, int gridSize = 10, string gridColor = "", GridType gridType = GridType.Grid, DrawType drawType = DrawType.Pen, string eraserColor = "#ffffff") 
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("InitialiseCanvas", forElement, resizeElement, _thisRef, brushColor, brushSize, gridSize, gridColor, gridType, drawType, eraserColor);
        }

        public async Task SetBrushColor(string color)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetBrushColor", color);
        }

        public async Task SetBrushSize(int size)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetBrushSize", size);
        }

        public async Task SetEraserSize(int size)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetEraserSize", size);
        }

        public async Task SetEraserColor(string color)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetEraserColor", color);
        }

        public async Task SetGridSize(int size)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetGridSize", size);
        }

        public async Task SetGridColor(string color)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetGridColor", color);
        }

        public async Task SetGridType(GridType gridType)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetGridType", gridType);
        }

        public async Task SetDrawType(DrawType drawType)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetDrawType", drawType);
        }

        public async Task Destroy()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("Destroy");
        }

        public async Task Clear()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("Clear");            
        }

        public async Task Refresh()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("Refresh");  
        }

        public async Task Restore(string commandJson)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("Restore", commandJson);
        }


        [JSInvokable("OnCanvasUpdated")]
        public Task OnCanvasUpdated()
        {
            CanvasCommandsUpdated?.Invoke(this, null);
            return Task.CompletedTask;
        }
        #endregion

        #region Dispose
        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
        #endregion

    }
}