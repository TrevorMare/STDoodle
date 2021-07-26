using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropCanvas
    {

        ValueTask<string> InitialiseCanvas(ElementReference forElement);

    }
}