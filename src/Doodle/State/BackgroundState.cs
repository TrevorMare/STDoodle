using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Doodle.State
{

    public class BackgroundState : Abstractions.Interfaces.IDoodleDrawState
    {

        #region Properties
        public string Id => Guid.NewGuid().ToString();

        public string TypeName => nameof(BackgroundState);
        
        public string Detail { get; set; } = "";
        #endregion     

        #region ctor
        public BackgroundState()
        {
            
        }

        public BackgroundState(IEnumerable<Abstractions.Models.BackgroundData> sources)
        {
            this.Detail = JsonConverters.Serialization.Serialize(sources);
        }
        #endregion

    }

}