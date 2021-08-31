using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public interface IDoodleStateDetail
    {

        int Sequence { get; }

        bool Reverted { get; }

        Task<IDoodleStateDetail> CloneState(int sequence);

        Task SetBackgroundState(IDoodleDrawState state);

        Task SetCanvasState(IDoodleDrawState state);

        Task SetResizableState(IDoodleDrawState state);

        Task SetReverted(bool reverted);

    }

}