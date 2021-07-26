using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Dependencies
{

    public class JsInteropCanvas : Abstractions.JsInterop.IJsInteropCanvas
    {


        public ValueTask<string> InitialiseCanvas(ElementReference forElement)
        {
            throw new System.NotImplementedException();
        }
        
    }

}