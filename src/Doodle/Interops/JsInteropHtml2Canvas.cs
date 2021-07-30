using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Doodle.Interops
{

    public class JsInteropHtml2Canvas : Abstractions.JsInterop.IJsInteropHtml2Canvas, IAsyncDisposable
    {

        #region Members
        private const string _basePath = "./_content/STDoodle/js/JsInteropHtml2Canvas.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        #endregion

        #region ctor
        public JsInteropHtml2Canvas(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
        }
        #endregion

        #region Interface Methods
        public async ValueTask<string> WriteElementImageToBuffer(ElementReference forElement, Abstractions.Config.Html2CanvasConfig config = default, CancellationToken cancelationToken = default)
        {
            string configOptionsJson = "";
            if (config != null)
            {
                var options = new JsonSerializerOptions 
                {
                    WriteIndented = false, 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true
                };
                configOptionsJson = JsonSerializer.Serialize(config, options);
            }

            var module = await _moduleTask.Value;
            var bufferId = await module.InvokeAsync<string>("ConvertElementToImage", cancelationToken, forElement, configOptionsJson);
            return bufferId;
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