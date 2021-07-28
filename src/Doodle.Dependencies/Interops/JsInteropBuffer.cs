using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Doodle.Dependencies.Interops
{

    public class JsInteropBuffer : Abstractions.JsInterop.IJsInteropBuffer, IAsyncDisposable
    {

        #region Members
        private const string _basePath = "./_content/Doodle.Dependencies/js/JsInteropBuffer.min.js";
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        #endregion

        #region ctor
        public JsInteropBuffer(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", _basePath).AsTask());
        }
        #endregion

        #region Interface Methods
        public async Task LoadBuffer(string bufferId)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("LoadBuffer", bufferId);
        }

        public async Task ClearBuffer(string bufferId)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("ClearBuffer", bufferId);
        }

        public async ValueTask<string> ReadBuffer(string bufferId, CancellationToken cancelationToken = default)
        {
            await this.LoadBuffer(bufferId);

            var module = await _moduleTask.Value;

            bool bufferHasData = await this.BufferHasData(bufferId);
            while (bufferHasData == false)
            {
                if (cancelationToken.IsCancellationRequested)
                {
                    return null;
                }

                await Task.Delay(100);
                bufferHasData = await this.BufferHasData(bufferId);
            }

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

        public async Task<bool> BufferHasData(string bufferId)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("BufferHasData", bufferId);
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