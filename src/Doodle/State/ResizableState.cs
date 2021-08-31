using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Doodle.State
{

    public class ResizableState : Abstractions.Interfaces.IDoodleDrawState
    {

        #region Properties
        public string Id => Guid.NewGuid().ToString();

        public string TypeName => nameof(ResizableState);
        
        public string Detail { get; set; } = "";
        #endregion     

        #region ctor
        public ResizableState()
        {
            
        }

        public ResizableState(IEnumerable<Abstractions.Interfaces.IResizableContent> elements)
        {
            this.Detail = JsonConverters.Serialization.Serialize(elements);
        }
        #endregion

    }

}