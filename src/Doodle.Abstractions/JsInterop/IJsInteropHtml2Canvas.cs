using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropHtml2Canvas
    {

        ValueTask<string> WriteElementImageToBuffer(ElementReference forElement, Config.Html2CanvasConfig config = default, CancellationToken cancelationToken = default);

    }
}