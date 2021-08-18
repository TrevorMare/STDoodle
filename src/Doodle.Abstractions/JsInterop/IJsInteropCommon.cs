using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{
    public interface IJsInteropCommon
    {

        Task ClickElement(ElementReference element);

    }
}