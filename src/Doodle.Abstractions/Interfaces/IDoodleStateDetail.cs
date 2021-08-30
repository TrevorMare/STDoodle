using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleStateDetail
    {

        int Sequence { get; }

        IDoodleDrawState BackgroundState { get; set; }

        IDoodleDrawState CanvasState { get; set; }

        IDoodleDrawState ResizableState { get; set; }

        Task<IDoodleStateDetail> CloneState(int sequence);

        Task SetBackgroundState(IDoodleDrawState state);

        Task SetCanvasState(IDoodleDrawState state);

        Task SetResizableState(IDoodleDrawState state);

    }

}