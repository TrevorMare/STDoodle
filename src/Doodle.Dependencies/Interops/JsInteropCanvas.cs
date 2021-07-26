using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Doodle.Dependencies.Interops
{

    public class JsInteropCanvas : Abstractions.JsInterop.IJsInteropCanvas, IAsyncDisposable
    {

        #region Members
        private const string _basePath = "./_content/Doodle.Dependencies/js/JsInteropCanvas.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        #endregion

        #region ctor
        public JsInteropCanvas(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
        }
        #endregion

        #region Interface Methods
        public Task InitialiseCanvas(ElementReference forElement) 
        {
            //throw new System.NotImplementedException();
            return Task.CompletedTask;
        }

        public async ValueTask<string> RenderCanvasToImage(ElementReference forElement)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("RenderCanvasToImage", forElement);
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