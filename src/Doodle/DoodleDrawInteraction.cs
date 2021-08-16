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

        public double StrokeWidth { get; private set; } = 1;

        public Abstractions.Common.GridType GridType { get; private set; } = Abstractions.Common.GridType.None;
        
        public int GridSize { get; private set; } = 20;

        public string GridColor { get; private set; } = "#000";

        public bool CanUndo { get; private set; } = false;

        public bool CanRedo { get; private set; } = false;
        #endregion

        #region Events
        public event OnBackgroundChangedHandler OnBackgroundAdded;
        public event OnBackgroundChangedHandler OnBackgroundRemoved;
        public event EventHandler OnStateHasChanged;
        public event OnColorChangedHandler OnStrokeColorChanged;
        public event OnSizeChangedHandler OnStrokeWidthChanged;
        public event OnSizeChangedHandler OnCanvasGridSizeChanged;
        public event OnCanvasGridTypeChangedHandler OnCanvasGridTypeChanged;
        public event OnColorChangedHandler OnCanvasGridColorChanged;
        public event OnBoolChangedHandler OnCanRedoChanged;
        public event OnBoolChangedHandler OnCanUndoChanged;
        #endregion

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

        public Task SetCanvasGridType(Abstractions.Common.GridType gridType)
        {
            if (gridType != this.GridType)
            {
                this.GridType = gridType;
                this.OnCanvasGridTypeChanged?.Invoke(this, gridType);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetCanvasGridSize(int gridSize)
        {
            if (gridSize != this.GridSize)
            {
                this.GridSize = gridSize;
                this.OnCanvasGridSizeChanged?.Invoke(this, gridSize);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetCanvasGridColor(string gridColor)
        {
            if (gridColor != this.GridColor)
            {
                this.GridColor = gridColor;
                this.OnCanvasGridColorChanged?.Invoke(this, gridColor);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetCanRedo(bool canRedo)
        {
            if (canRedo != this.CanRedo)
            {
                this.CanRedo = canRedo;
                this.OnCanRedoChanged?.Invoke(this, canRedo);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }
        
        public Task SetCanUndo(bool canUndo)
        {
            if (canUndo != this.CanUndo)
            {
                this.CanUndo = canUndo;
                this.OnCanUndoChanged?.Invoke(this, canUndo);
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