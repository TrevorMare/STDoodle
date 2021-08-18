using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Doodle.Interops
{

    public class JsInteropCommon : Abstractions.JsInterop.IJsInteropCommon, IAsyncDisposable
    {

        #region Members
        private const string _basePath = "./_content/STDoodle/js/JsInteropCommon.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        #endregion

        #region ctor
        public JsInteropCommon(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
        }
        #endregion

        #region Methods
        public async Task ClickElement(ElementReference element)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ClickElement", element);
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