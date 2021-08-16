using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doodle.Abstractions.Interfaces
{

    public delegate void OnStrokeColorChangedHandler(object sender, string strokeColor);
    public delegate void OnStrokeWidthChangedHandler(object sender, double strokeWidth);
    public delegate void OnBackgroundAddedHandler(object sender, Models.BackgroundData backgroundData);
    public delegate void OnBackgroundRemovedHandler(object sender, Models.BackgroundData backgroundData);
    public delegate void OnClearDoodleHandler(object sender, bool clearHistory);


    public interface IDoodleDrawInteraction
    {

        #region Events
        event EventHandler OnStateHasChanged;
        event OnStrokeColorChangedHandler OnStrokeColorChanged;
        event OnStrokeWidthChangedHandler OnStrokeWidthChanged;
        event OnBackgroundAddedHandler OnBackgroundAdded;
        event OnBackgroundRemovedHandler OnBackgroundRemoved;
        event EventHandler OnUndoLastAction;
        event EventHandler OnRedoLastAction;
        event OnClearDoodleHandler OnClearDoodle;
        event EventHandler OnExportImage;
        event EventHandler OnSaveDoodleData;
        event EventHandler OnRestoreDoodleData;
        #endregion

        #region Properties

        IEnumerable<Models.BackgroundData> SelectedBackgrounds { get; }

        string StrokeColor { get; }

        double StrokeWidth { get; }
        #endregion
        
        Task SetStrokeColor(string color);

        Task SetStrokeWidth(double width);

        Task AddBackground(Models.BackgroundData backgroundData);

        Task RemoveBackground(Models.BackgroundData backgroundData);

        Task<bool> ContainsBackground(Models.BackgroundData backgroundData);

        Task UndoLastAction();

        Task RedoLastAction();

        Task ClearDoodle(bool clearHistory);

        Task ExportImage();

        Task SaveDoodleData();

        Task RestoreDoodleData();

    }

}