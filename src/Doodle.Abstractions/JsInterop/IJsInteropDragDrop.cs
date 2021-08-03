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

        Task InitialiseResizable(ElementReference element, ElementDimensions dimensions, bool elementActive = false, bool allowResize = true, bool allowMove = true);

        Task ActivateElement();

        Task DeActivateElement();

        Task SetAllowResize(bool value);

        Task SetAllowMove(bool value);

        Task ElementMoved(ElementReference element, ElementDimensions dimension);

        Task ElementResized(ElementReference element, ElementDimensions dimension);

        Task ElementUpdated(ElementReference element, ElementDimensions dimension);
    }


}