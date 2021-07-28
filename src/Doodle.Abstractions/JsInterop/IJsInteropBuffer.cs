using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropBuffer
    {

        Task LoadBuffer(string bufferId);

        Task ClearBuffer(string bufferId);

        ValueTask<string> ReadBuffer(string bufferId, CancellationToken cancelationToken = default);

        Task<bool> BufferHasData(string bufferId);

        Task<bool> BufferExists(string bufferId);

        Task<long> BufferLength(string bufferId);

    }
}