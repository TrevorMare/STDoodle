using System;
using System.Threading;
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

        public async ValueTask<string> RenderCanvasToImage(ElementReference forElement, CancellationToken cancelationToken = default)
        {
            var module = await _moduleTask.Value;
            var bufferId = await module.InvokeAsync<string>("RenderCanvasToImage", forElement);

            bool isBufferReady = await BufferExists(bufferId);
            while (isBufferReady == false)
            {
                if (cancelationToken != null && cancelationToken.IsCancellationRequested)
                {
                    return null;
                }
                await Task.Delay(100);
                isBufferReady = await BufferExists(bufferId);;
            }

            return bufferId;
        }

        public async Task ClearBufferedImage(string bufferId)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ClearBufferedImage", bufferId);
        }

        public async Task<bool> BufferExists(string bufferId)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("BufferExists", bufferId);
        }

        public async Task<long> BufferLength(string bufferId)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<long>("BufferLength", bufferId);
        }

        public async ValueTask<string> ReadBufferedImage(string bufferId, CancellationToken cancelationToken = default)
        {
            var module = await _moduleTask.Value;
            using (var jsBufferStream = new Helpers.JsBufferStream(module, bufferId))
            {
                using (var resultStream = new System.IO.MemoryStream())
                {
                    await jsBufferStream.CopyToAsync(resultStream, cancelationToken);
                    var byteArray = resultStream.ToArray();

                    var base64Image = Convert.ToBase64String(byteArray);
                    return $"data:image/png;base64,{base64Image}";
                }
            }
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