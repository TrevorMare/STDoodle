using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Models;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleStateManager
    {

        event EventHandler OnDoodleDrawStateChanged;

        event EventHandler OnRestoreState;

        IDoodleStateDetail CurrentState { get; }

        IEnumerable<IDoodleStateDetail> StateHistory { get; }

        IDoodleDrawState BackgroundState { get; }

        IDoodleDrawState CanvasState { get; }

        IDoodleDrawState ResizableState { get; }

        IEnumerable<BackgroundData> SelectedBackgrounds { get; }

        bool CanUndo { get; }

        bool CanRedo { get; }

        bool IsDirty { get; }

        Task PushBackgroundState(IDoodleDrawState state); 

        Task PushCanvasState(IDoodleDrawState state); 

        Task PushResziableState(IDoodleDrawState state); 

        Task UndoLastAction();

        Task RedoLastAction();

        Task ClearDoodle(bool clearHistory);

    }


}