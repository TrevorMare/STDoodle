using System.Threading.Tasks;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{

    public delegate void OnElementResizedHandler(object sender, ElementDimensions dimension);
    public delegate void OnElementMovedHandler(object sender, ElementDimensions dimension);
    public delegate void OnElementUpdatedHandler(object sender, ElementDimensions dimension);

    public interface IJsInteropDragDrop
    {
        event OnElementResizedHandler OnElementResized;

        event OnElementMovedHandler OnElementMoved;

        event OnElementUpdatedHandler OnElementUpdated;

        Task ActivateElement(ElementReference element);

        Task DeActivateElement(ElementReference element);

        Task ElementMoved(ElementReference element, ElementDimensions dimension);

        Task ElementResized(ElementReference element, ElementDimensions dimension);

        Task ElementUpdated(ElementReference element, ElementDimensions dimension);
    }


}