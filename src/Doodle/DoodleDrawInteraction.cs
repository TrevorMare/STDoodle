using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Interfaces;
using Doodle.Abstractions.Models;

namespace Doodle
{

    public class DoodleDrawInteraction : Abstractions.Interfaces.IDoodleDrawInteraction
    {

        #region Properties
        public IEnumerable<BackgroundData> SelectedBackgrounds { get; private set; } = new List<BackgroundData>();

        public string StrokeColor { get; private set; } = "#000";

        public double StrokeWidth { get; set; } = 1;
        #endregion

        #region Events
        public event OnBackgroundAddedHandler OnBackgroundAdded;
        public event OnBackgroundRemovedHandler OnBackgroundRemoved;
        public event EventHandler OnStateHasChanged;
        #endregion

        public event OnStrokeColorChangedHandler OnStrokeColorChanged;
        public event OnStrokeWidthChangedHandler OnStrokeWidthChanged;
 
        public event EventHandler OnUndoLastAction;
        public event EventHandler OnRedoLastAction;
        public event OnClearDoodleHandler OnClearDoodle;
        public event EventHandler OnExportImage;
        public event EventHandler OnSaveDoodleData;
        public event EventHandler OnRestoreDoodleData;
        

        #region Methods
        public Task AddBackground(BackgroundData backgroundData)
        {
            if (!this.SelectedBackgrounds.Contains(backgroundData))
            {
                var workingList = this.SelectedBackgrounds.ToList();
                workingList.Add(backgroundData);
                this.SelectedBackgrounds = workingList;
                this.OnBackgroundAdded?.Invoke(this, backgroundData);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task RemoveBackground(BackgroundData backgroundData)
        {
            if (this.SelectedBackgrounds.Contains(backgroundData))
            {
                var workingList = this.SelectedBackgrounds.ToList();
                workingList.Remove(backgroundData);
                this.SelectedBackgrounds = workingList;
                this.OnBackgroundRemoved?.Invoke(this, backgroundData);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }
        
        public Task<bool> ContainsBackground(BackgroundData backgroundData)
        {
            bool result = this.SelectedBackgrounds.Contains(backgroundData);
            return Task.FromResult(result);
        }

        public Task SetStrokeColor(string color)
        {
            if (color != this.StrokeColor)
            {
                this.StrokeColor = color;
                this.OnStrokeColorChanged?.Invoke(this, color);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetStrokeWidth(double width)
        {
            if (width != this.StrokeWidth)
            {
                this.StrokeWidth = width;
                this.OnStrokeWidthChanged?.Invoke(this, width);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }
        #endregion

        public Task ClearDoodle(bool clearHistory)
        {
            throw new NotImplementedException();
        }

        public Task ExportImage()
        {
            throw new NotImplementedException();
        }

        public Task RedoLastAction()
        {
            throw new NotImplementedException();
        }

        public Task RestoreDoodleData()
        {
            throw new NotImplementedException();
        }

        public Task SaveDoodleData()
        {
            throw new NotImplementedException();
        }

        

       

        public Task UndoLastAction()
        {
            throw new NotImplementedException();
        }
    }


}