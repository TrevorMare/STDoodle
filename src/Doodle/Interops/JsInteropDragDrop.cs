using System;
using System.Text.Json;
using System.Threading.Tasks;
using Doodle.Abstractions.JsInterop;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Doodle.Interops
{

    public class JsInteropDragDrop : Abstractions.JsInterop.IJsInteropDragDrop, IAsyncDisposable
    {


        #region Events
        public event OnElementResizedHandler OnElementResized;
        public event OnElementMovedHandler OnElementMoved;
        public event OnElementUpdatedHandler OnElementUpdated;
        #endregion

        #region Members
        private const string _basePath = "./_content/STDoodle/js/JsInteropDragDrop.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private readonly DotNetObjectReference<JsInteropDragDrop> _thisRef;
        private ElementReference _resizeElement;
        #endregion

        #region ctor
        public JsInteropDragDrop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
            _thisRef = DotNetObjectReference.Create(this);
        }
        #endregion

        #region Interface Methods
        public async Task InitialiseResizable(ElementReference element, ElementDimensions dimensions, bool elementActive = false, bool allowResize = true, bool allowMove = true)
        {
            var module = await _moduleTask.Value;
            this._resizeElement = element;

            string dimensionsJson = "";
            if (dimensions != null)
            {
                var options = new JsonSerializerOptions 
                {
                    WriteIndented = false, 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true
                };
                dimensionsJson = JsonSerializer.Serialize(dimensions, options);
            }

            await module.InvokeVoidAsync("InitialiseResizable", element, element.Id, _thisRef, dimensionsJson, elementActive, allowResize, allowMove);
        }

        public async Task ActivateElement()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ActivateElement", this._resizeElement.Id);
        }

        public async Task DeActivateElement()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ActivateElement", this._resizeElement.Id);
        }

        public async Task SetAllowResize(bool value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetAllowResize", this._resizeElement.Id, value);
        }

        public async Task SetAllowMove(bool value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetAllowMove", this._resizeElement.Id, value);
        }

        [JSInvokable("ElementMoved")]
        public Task ElementMoved(ElementReference element, ElementDimensions dimension)
        {
            this.OnElementMoved?.Invoke(element, dimension);
            return Task.CompletedTask;
        }

        [JSInvokable("ElementResized")]
        public Task ElementResized(ElementReference element, ElementDimensions dimension)
        {
            this.OnElementResized?.Invoke(element, dimension);
            return Task.CompletedTask;
        }

        [JSInvokable("ElementUpdated")]
        public Task ElementUpdated(ElementReference element, ElementDimensions dimension)
        {
            this.OnElementUpdated?.Invoke(element, dimension);
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