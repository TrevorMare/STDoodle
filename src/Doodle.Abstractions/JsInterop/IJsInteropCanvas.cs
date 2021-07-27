using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropCanvas
    {

        Task InitialiseCanvas(ElementReference forElement);

        ValueTask<string> RenderCanvasToImage(ElementReference forElement);

        Task ClearBufferedImage(string bufferId);

        Task<bool> BufferExists(string bufferId);

        Task<long> BufferLength(string bufferId);

        Task<byte[]> ReadBufferedImage(string bufferId);

    }
}