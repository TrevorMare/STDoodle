using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Doodle.State
{

    public class CanvasState : Abstractions.Interfaces.IDoodleDrawState
    {

        #region Properties
        public string Id => Guid.NewGuid().ToString();

        public string TypeName => nameof(BackgroundState);
        
        public string Detail { get; set; } = "";
        #endregion     

        #region ctor
        public CanvasState()
        {
            
        }

        public CanvasState(IEnumerable<Abstractions.Models.CanvasPath> commands)
        {
            this.Detail = JsonSerializer.Serialize(commands);
        }
        #endregion

    }

}