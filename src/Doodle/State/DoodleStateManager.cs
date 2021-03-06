using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        private IEnumerable<IResizableContent> _resizableContent = new List<IResizableContent>();

        private IEnumerable<CanvasPath> _canvasContent = new List<CanvasPath>();
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

        public DoodleStateDetail CurrentState { get; private set; } = new DoodleStateDetail();
        
        public IEnumerable<IDoodleStateDetail> StateHistory { get; private set; } = new List<IDoodleStateDetail>();

        public IDoodleDrawState BackgroundState => CurrentState.BackgroundState;

        public IDoodleDrawState CanvasState => CurrentState.CanvasState;

        public IDoodleDrawState ResizableState => CurrentState.ResizableState;

        public IEnumerable<BackgroundData> SelectedBackgrounds => _selectedBackgrounds;

        public IEnumerable<IResizableContent> ResizableContent => _resizableContent;

        public IEnumerable<CanvasPath> CanvasContent => _canvasContent;
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
                this.CurrentState = (DoodleStateDetail)sequencedItems[0];
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
                this.CurrentState = (DoodleStateDetail)sequencedItems[0];
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

        public Task<string> SaveCurrentState()
        {
            string result = null;
            if (this.CurrentState != null)
            {
                result = JsonConverters.Serialization.Serialize(this.CurrentState);
            }
            return Task.FromResult(result);
        }

        public async Task RestoreCurrentState(string json)
        {
           
            this._currentSequence = 0;
            this.StateHistory = new List<IDoodleStateDetail>();
            this.CurrentState = new DoodleStateDetail();
            if (!string.IsNullOrEmpty(json))
            {
                this.CurrentState = JsonConverters.Serialization.Deserialize<DoodleStateDetail>(json);
                this.CurrentState.Sequence = this._currentSequence++;
            }
            await PushNewState(this.CurrentState);
            
            this.OnDoodleDrawStateChanged?.Invoke(this, null);
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
            this.CurrentState = (DoodleStateDetail)newState;
            this._isDirty = true;
           
            await this.SetupState();
            this.OnDoodleDrawStateChanged?.Invoke(this, null);
        }
       
        private Task SetupState()
        {
            this._selectedBackgrounds = new List<BackgroundData>();
            this._resizableContent = new List<IResizableContent>();
            if (this.CurrentState != null)
            {
                string jsonBackgroundData = this.CurrentState?.BackgroundState?.Detail;
                if (!string.IsNullOrEmpty(jsonBackgroundData))
                {
                    this._selectedBackgrounds = JsonConverters.Serialization.Deserialize<List<BackgroundData>>(jsonBackgroundData);
                }
                
                string jsonResizableContent = this.CurrentState?.ResizableState?.Detail;
                if (!string.IsNullOrEmpty(jsonResizableContent))
                {
                    this._resizableContent = JsonConverters.Serialization.Deserialize<List<IResizableContent>>(jsonResizableContent);
                }

                string jsonCanvasData = this.CurrentState?.CanvasState?.Detail;
                if (!string.IsNullOrEmpty(jsonCanvasData))
                {
                    this._canvasContent = JsonConverters.Serialization.Deserialize<List<CanvasPath>>(jsonCanvasData);
                }
            }
            return Task.CompletedTask;
        }
        #endregion

    }

}