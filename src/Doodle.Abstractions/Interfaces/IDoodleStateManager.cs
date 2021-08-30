using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleStateManager
    {

        event EventHandler OnDoodleDrawStateChanged;

        IDoodleStateDetail CurrentState { get; }

        IEnumerable<IDoodleStateDetail> StateHistory { get; }

        IDoodleDrawState BackgroundState { get; }

        IDoodleDrawState CanvasState { get; }

        IDoodleDrawState ResizableState { get; }

        Task PushBackgroundState(IDoodleDrawState state); 

        Task PushCanvasState(IDoodleDrawState state); 

        Task PushResziableState(IDoodleDrawState state); 

    }


}