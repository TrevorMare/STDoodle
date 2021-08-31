using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Doodle.State
{

    public class ResizableState : Abstractions.Interfaces.IDoodleDrawState
    {

        #region Properties
        public string Id => Guid.NewGuid().ToString();

        public string TypeName => nameof(BackgroundState);
        
        public string Detail { get; set; } = "";
        #endregion     

        #region ctor
        public ResizableState()
        {
            
        }

        public ResizableState(IEnumerable<Abstractions.Interfaces.IResizableContent> elements)
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new JsonConverters.ResizableElementConverter());
            this.Detail = JsonSerializer.Serialize(elements, serializeOptions);
        }
        #endregion

    }

}