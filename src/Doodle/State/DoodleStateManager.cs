using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Interfaces;
using Doodle.Abstractions.Models;
using Microsoft.Extensions.Logging;

namespace Doodle.State
{

    public class DoodleStateManager : Abstractions.Interfaces.IDoodleStateManager 
    {

        #region Events
        public event EventHandler OnDoodleDrawStateChanged;

        public event EventHandler OnRestoreState;
        #endregion

        #region Members
        private int _currentSequence = 0;
        private bool _isDirty = false;
        private readonly ILogger<DoodleStateManager> _logger;

        private IEnumerable<BackgroundData> _selectedBackgrounds = new List<BackgroundData>();
        #endregion

        #region ctor
        public DoodleStateManager(ILogger<DoodleStateManager> logger)
        {
            this._logger = logger;
        }
        #endregion

        #region Properties
        public bool CanUndo => StateHistory.Any(x => x.Reverted == false);

        public bool CanRedo => StateHistory.Any(x => x.Reverted == true);

        public bool IsDirty => _isDirty;

        public IDoodleStateDetail CurrentState { get; private set; } = new DoodleStateDetail();
        
        public IEnumerable<IDoodleStateDetail> StateHistory { get; private set; } = new List<IDoodleStateDetail>();

        public IDoodleDrawState BackgroundState => CurrentState.BackgroundState;

        public IDoodleDrawState CanvasState => CurrentState.CanvasState;

        public IDoodleDrawState ResizableState => CurrentState.ResizableState;

        public IEnumerable<BackgroundData> SelectedBackgrounds => _selectedBackgrounds;
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

        public async Task UndoLastAction()
        {
            await this.CurrentState.SetReverted(true);

            var sequencedItems = this.StateHistory.Where(x => x.Reverted == false && x.Sequence < this.CurrentState.Sequence).OrderByDescending(x => x.Sequence).ToList();
            if (sequencedItems.Count > 0)
            {
                this.CurrentState = sequencedItems[0];
            }
            else 
            {
                this.CurrentState = new DoodleStateDetail();
            }
            await this.SetupState();
            this.OnRestoreState?.Invoke(this, null);
        }

        public async Task RedoLastAction()
        {
            var sequencedItems = this.StateHistory.Where(x => x.Reverted == true && x.Sequence >= this.CurrentState.Sequence).OrderBy(x => x.Sequence).ToList();
            if (sequencedItems.Count > 0)
            {
                this.CurrentState = sequencedItems[0];
                await this.CurrentState.SetReverted(false);
            }
            await this.SetupState();
            this.OnRestoreState?.Invoke(this, null);
        }

        public async Task ClearDoodle(bool clearHistory)
        {
            if (clearHistory == true)
            {
                this.StateHistory = new List<IDoodleStateDetail>();
                this._isDirty = false;
                this._currentSequence = 0;
            }
            this.CurrentState = new DoodleStateDetail();
            await this.SetupState();
            
            this.OnRestoreState?.Invoke(this, null);
        }
        #endregion

        #region Private Methods
        private async Task PushNewState(IDoodleStateDetail newState)
        {
            // Push the change to the history
            var stateList = StateHistory.ToList();
            stateList.Add(newState);
            this.StateHistory = stateList;
            // Set the current state
            this.CurrentState = newState;
            this._isDirty = true;
           
            await this.SetupState();
            this.OnDoodleDrawStateChanged?.Invoke(this, null);
        }
       
        private Task SetupState()
        {
            this._selectedBackgrounds = new List<BackgroundData>();
            if (this.CurrentState != null)
            {
                string jsonBackgroundData = this.CurrentState?.BackgroundState?.Detail;
                if (!string.IsNullOrEmpty(jsonBackgroundData))
                {
                    this._selectedBackgrounds = System.Text.Json.JsonSerializer.Deserialize<List<BackgroundData>>(jsonBackgroundData);
                }
                
            }
            return Task.CompletedTask;
        }
        #endregion

    }

}