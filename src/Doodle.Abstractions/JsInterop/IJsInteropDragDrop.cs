using System.Threading.Tasks;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{

    public delegate void OnElementResizedHandler(object sender, ElementDimensions dimension);
    public delegate void OnElementMovedHandler(object sender, ElementDimensions dimension);
    public delegate void OnElementUpdatedHandler(object sender, ElementDimensions dimension);
    public delegate void OnSetIsActiveHandler(object sender, bool active);

    public interface IJsInteropDragDrop
    {
       
        event OnElementResizedHandler OnElementResized;

        event OnElementMovedHandler OnElementMoved;

        event OnElementUpdatedHandler OnElementUpdated;

        event OnSetIsActiveHandler OnSetIsActive;

        Task InitialiseResizable(ElementReference element, bool autoHandleEvents, bool elementActive = false, bool allowResize = true, bool allowMove = true, double? minWidth = null, double? minHeight = null);

        Task ActivateElement();

        Task DeActivateElement();

        Task SetAllowResize(bool value);

        Task SetAllowMove(bool value);

        Task SetMinWidth(double? value);
        
        Task SetMinHeight(double? value);

        Task SetAutoHandleEvents(bool value);

        Task ElementMoved(string dimension);

        Task ElementResized(string dimension);

        Task ElementUpdated(string dimension);

        Task SetIsActivate(bool value);
    }


}