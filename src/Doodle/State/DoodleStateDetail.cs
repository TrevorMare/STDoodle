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
        public int Sequence { get; internal set; }

        public bool Reverted { get; internal set; }

        [JsonConverter(typeof(BackgroundStateConverter))]
        public IDoodleDrawState BackgroundState { get; internal set; }

        [JsonConverter(typeof(CanvasStateConverter))]
        public IDoodleDrawState CanvasState { get; internal set; }

        [JsonConverter(typeof(ResizableStateConverter))]
        public IDoodleDrawState ResizableState { get; internal set; }
        #endregion

        #region Methods
        public Task SetBackgroundState(IDoodleDrawState state)
        {
            this.BackgroundState = state;
            return Task.CompletedTask;
        }

        public Task SetCanvasState(IDoodleDrawState state)
        {
            this.CanvasState = state;
            return Task.CompletedTask;
        }

        public Task SetResizableState(IDoodleDrawState state)
        {
            this.ResizableState = state;
            return Task.CompletedTask;
        }

        public Task SetReverted(bool reverted)
        {
            this.Reverted = reverted;
            return Task.CompletedTask;
        }

        public Task<IDoodleStateDetail> CloneState(int sequence)
        {
            var serializerOptions = new JsonSerializerOptions 
            {
                WriteIndented = false, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
                
            };

            var jsonData = System.Text.Json.JsonSerializer.Serialize(this, serializerOptions);
            var result = System.Text.Json.JsonSerializer.Deserialize<DoodleStateDetail>(jsonData, serializerOptions);
            
            result.Sequence = sequence;

            return Task.FromResult<IDoodleStateDetail>(result);
        }
        #endregion



    }

}