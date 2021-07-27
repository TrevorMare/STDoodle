using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropCanvas
    {

        Task InitialiseCanvas(ElementReference forElement);

        ValueTask<string> RenderCanvasToImage(ElementReference forElement, CancellationToken cancelationToken = default);

        ValueTask<string> ReadBufferedImage(string bufferId, CancellationToken cancelationToken = default);

        Task ClearBufferedImage(string bufferId);

        Task<bool> BufferExists(string bufferId);

        Task<long> BufferLength(string bufferId);

        

    }
}