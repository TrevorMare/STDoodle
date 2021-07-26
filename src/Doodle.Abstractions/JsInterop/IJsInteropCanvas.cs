using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropCanvas
    {

        Task InitialiseCanvas(ElementReference forElement);

        ValueTask<string> RenderCanvasToImage(ElementReference forElement);

    }
}