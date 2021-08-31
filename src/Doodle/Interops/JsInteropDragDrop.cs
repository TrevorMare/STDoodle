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
        public event OnSetIsActiveHandler OnSetIsActive;
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
        public async Task InitialiseResizable(ElementReference element, bool autoHandleEvents, bool elementActive = false, bool allowResize = true, bool allowMove = true, double? minWidth = null, double? minHeight = null)
        {
            var module = await _moduleTask.Value;
            this._resizeElement = element;

            await module.InvokeVoidAsync("InitialiseResizable", element, element.Id, _thisRef, autoHandleEvents, elementActive, allowResize, allowMove, minWidth, minHeight);
        }

        public async Task ActivateElement()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ActivateElement", this._resizeElement.Id);
        }

        public async Task DeActivateElement()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("DeActivateElement", this._resizeElement.Id);
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

        public async Task SetMinWidth(double? value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetMinWidth", this._resizeElement.Id, value);
        }

        public async Task SetMinHeight(double? value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetMinHeight", this._resizeElement.Id, value);
        }

        public async Task SetAutoHandleEvents(bool value)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("SetAutoHandleEvents", this._resizeElement.Id, value);
        }

        [JSInvokable("ElementMoved")]
        public Task ElementMoved(string dimensionJson)
        {
            this.OnElementMoved?.Invoke(_resizeElement, GetDimensions(dimensionJson));
            return Task.CompletedTask;
        }

        [JSInvokable("ElementResized")]
        public Task ElementResized(string dimensionJson)
        {
            this.OnElementResized?.Invoke(_resizeElement, GetDimensions(dimensionJson));
            return Task.CompletedTask;
        }

        [JSInvokable("ElementUpdated")]
        public Task ElementUpdated(string dimensionJson)
        {
            this.OnElementUpdated?.Invoke(_resizeElement, GetDimensions(dimensionJson));
            return Task.CompletedTask;
        }

        [JSInvokable("SetIsActivate")]
        public Task SetIsActivate(bool value)
        {
            this.OnSetIsActive?.Invoke(_resizeElement, value);
            return Task.CompletedTask;
        }

        private ElementDimensions GetDimensions(string dimensionJson)
        {
            if (string.IsNullOrEmpty(dimensionJson))
            {
                return null;
            }
            var dimensions = JsonConverters.Serialization.Deserialize<ElementDimensions>(dimensionJson);
            return dimensions;
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