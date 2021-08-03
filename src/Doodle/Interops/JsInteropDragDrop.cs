using System.Threading.Tasks;
using Doodle.Abstractions.JsInterop;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Interops
{

    public class JsInteropDragDrop : Abstractions.JsInterop.IJsInteropDragDrop
    {
        public event OnElementResizedHandler OnElementResized;
        public event OnElementMovedHandler OnElementMoved;
        public event OnElementUpdatedHandler OnElementUpdated;

        public Task ActivateElement(ElementReference element)
        {
            throw new System.NotImplementedException();
        }

        public Task DeActivateElement(ElementReference element)
        {
            throw new System.NotImplementedException();
        }

        public Task ElementMoved(ElementReference element, ElementDimensions dimension)
        {
            throw new System.NotImplementedException();
        }

        public Task ElementResized(ElementReference element, ElementDimensions dimension)
        {
            throw new System.NotImplementedException();
        }

        public Task ElementUpdated(ElementReference element, ElementDimensions dimension)
        {
            throw new System.NotImplementedException();
        }
    }


}