using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Doodle.Abstractions.Interfaces;
using Doodle.JsonConverters;

namespace Doodle.State
{

    public class DoodleStateDetail : Abstractions.Interfaces.IDoodleStateDetail
    {

        #region Properties
        public int Sequence { get; set; }

        public bool Reverted { get; set; }
        
        public BackgroundState BackgroundState { get; set; }
        
        public CanvasState CanvasState { get; set; }
        
        public ResizableState ResizableState { get; set; }
        #endregion

        #region Methods
        public Task SetBackgroundState(IDoodleDrawState state)
        {
            this.BackgroundState = (BackgroundState)state;
            return Task.CompletedTask;
        }

        public Task SetCanvasState(IDoodleDrawState state)
        {
            this.CanvasState = (CanvasState)state;
            return Task.CompletedTask;
        }

        public Task SetResizableState(IDoodleDrawState state)
        {
            this.ResizableState = (ResizableState)state;
            return Task.CompletedTask;
        }

        public Task SetReverted(bool reverted)
        {
            this.Reverted = reverted;
            return Task.CompletedTask;
        }

        public Task<IDoodleStateDetail> CloneState(int sequence)
        {
            var jsonData = JsonConverters.Serialization.Serialize(this);
            var result = JsonConverters.Serialization.Deserialize<DoodleStateDetail>(jsonData);
            result.Sequence = sequence;
            return Task.FromResult<IDoodleStateDetail>(result);
        }
        #endregion



    }

}