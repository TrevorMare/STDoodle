using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace Doodle.State
{

    public class DoodleStateManager : Abstractions.Interfaces.IDoodleStateManager
    {

        #region Events
        public event EventHandler OnDoodleDrawStateChanged;
        #endregion

        #region Members
        private int _currentSequence = 0;
        private readonly ILogger<DoodleStateManager> _logger;
        #endregion

        #region ctor
        public DoodleStateManager(ILogger<DoodleStateManager> logger)
        {
            this._logger = logger;
        }
        #endregion

        #region Properties
        public IDoodleStateDetail CurrentState { get; private set; } = new DoodleStateDetail();
        
        public IEnumerable<IDoodleStateDetail> StateHistory { get; private set; } = new List<IDoodleStateDetail>();

        public IDoodleDrawState BackgroundState => CurrentState.BackgroundState;

        public IDoodleDrawState CanvasState => CurrentState.CanvasState;

        public IDoodleDrawState ResizableState => CurrentState.ResizableState;
        #endregion

        #region Public Methods

        public async Task PushBackgroundState(IDoodleDrawState backgroundState)
        {
            if (backgroundState != null)
            {
                var newState = await CurrentState.CloneState(this._currentSequence++);
                await newState.SetBackgroundState(backgroundState);
                await PushNewState(newState);
            }
        }

        public async Task PushCanvasState(IDoodleDrawState canvasState)
        {
            if (canvasState != null)
            {
                var newState = await CurrentState.CloneState(this._currentSequence++);
                await newState.SetCanvasState(canvasState);
                await PushNewState(newState);
            }
        }

        public async Task PushResziableState(IDoodleDrawState canvasState)
        {
            if (canvasState != null)
            {
                var newState = await CurrentState.CloneState(this._currentSequence++);
                await newState.SetResizableState(canvasState);
                await PushNewState(newState);
            }
        }
        #endregion

        #region Private Methods
        private Task PushNewState(IDoodleStateDetail newState)
        {
            // Push the change to the history
            var stateList = StateHistory.ToList();
            stateList.Add(newState);
            this.StateHistory = stateList;
            // Set the current state
            this.CurrentState = newState;
            this.OnDoodleDrawStateChanged?.Invoke(this, null);
            
            return Task.CompletedTask;
        }
        #endregion

    }

}