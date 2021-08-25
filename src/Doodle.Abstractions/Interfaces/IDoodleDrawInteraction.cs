using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Interfaces
{

    public delegate void OnToolbarContentChangedHandler(object sender, ToolbarContent toolbarContent);
    public delegate void OnColorChangedHandler(object sender, string color);
    public delegate void OnSizeChangedHandler(object sender, double size);
    public delegate void OnBoolChangedHandler(object sender, bool size);
    public delegate void OnBackgroundChangedHandler(object sender, Models.BackgroundData backgroundData);
    public delegate void OnClearDoodleHandler(object sender, bool clearHistory);
    public delegate void OnCanvasGridTypeChangedHandler(object sender, Abstractions.Common.GridType gridType);
    public delegate void OnDrawModeChangedHandler(object sender, Abstractions.Common.DrawMode drawMode);
    public delegate void OnDrawTypeChangedHandler(object sender, Abstractions.Common.DrawType drawMode);
    public delegate void OnRestoreHandler(object sender, string jsonContent);
    public delegate void OnResizableContentsChangedHandler(object sender, IEnumerable<IResizableContent> contents);

    public interface IDoodleDrawInteraction
    {

        #region Events
        event EventHandler OnStateHasChanged;
        event OnColorChangedHandler OnStrokeColorChanged;
        event OnColorChangedHandler OnEraserColorChanged;
        event OnSizeChangedHandler OnStrokeWidthChanged;
        event OnBackgroundChangedHandler OnBackgroundAdded;
        event OnBackgroundChangedHandler OnBackgroundRemoved;
        event OnSizeChangedHandler OnCanvasGridSizeChanged;
        event OnSizeChangedHandler OnEraserSizeChanged;
        event OnCanvasGridTypeChangedHandler OnCanvasGridTypeChanged;
        event OnColorChangedHandler OnCanvasGridColorChanged;
        event OnBoolChangedHandler OnCanRedoChanged;
        event OnBoolChangedHandler OnCanUndoChanged;
        event OnDrawModeChangedHandler OnDrawModeChanged;
        event OnBoolChangedHandler OnIsDirtyChanged;
        event EventHandler OnUndoLastAction;
        event EventHandler OnRedoLastAction;
        event OnClearDoodleHandler OnClearDoodle;
        event EventHandler OnExportImage;
        event EventHandler OnSaveDoodleData;
        event OnRestoreHandler OnRestoreDoodleData;
        event EventHandler OnRedrawCanvas; 
        event OnToolbarContentChangedHandler OnToolbarContentChanged;
        event OnDrawTypeChangedHandler OnDrawTypeChanged;
        event OnResizableContentsChangedHandler OnResizableContentsChanged;
        event OnColorChangedHandler OnBackgroundColorChanged;
        #endregion

        #region Properties

        IEnumerable<Models.BackgroundData> SelectedBackgrounds { get; }

        IEnumerable<IResizableContent> ResizableContents { get; }

        string StrokeColor { get; }

        double StrokeWidth { get; }

        double EraserWidth { get; }

        string EraserColor { get; }

        string BackgroundColor { get; }

        Abstractions.Common.GridType GridType { get; } 

        int GridSize { get; } 

        string GridColor { get; }

        bool CanUndo { get; }

        bool CanRedo { get; }

        bool IsDirty { get; }

        Abstractions.Common.DrawMode DrawMode { get; }

        ToolbarContent ToolbarContent { get; }
        
        DrawType DrawType { get; }
        #endregion

        #region Methods        
        Task SetStrokeColor(string color);

        Task SetStrokeWidth(double width);

        Task SetEraserWidth(double width);

        Task SetEraserColor(string color);

        Task AddBackground(Models.BackgroundData backgroundData);

        Task RemoveBackground(Models.BackgroundData backgroundData);

        Task<bool> ContainsBackground(Models.BackgroundData backgroundData);

        Task SetCanvasGridType(Abstractions.Common.GridType gridType);

        Task SetCanvasGridSize(int size);

        Task SetCanvasGridColor(string color);

        Task SetCanRedo(bool canRedo);
        
        Task SetCanUndo(bool canUndo);

        Task SetDrawMode(Abstractions.Common.DrawMode drawMode);

        Task SetIsDirty(bool value);

        Task RedrawCanvas();

        Task UndoLastAction();

        Task RedoLastAction();

        Task ClearDoodle(bool clearHistory);

        Task ExportImage();

        Task SaveDoodleData();

        Task RestoreDoodleData(string jsonData);

        Task SetToolbarContent(ToolbarContent toolbarContent);

        Task ToggleToolbarContent(ToolbarContent toolbarContent);
        
        Task SetDrawType(DrawType drawType);

        Task AddResizableContent(IResizableContent content);

        Task RemoveResizableContent(IResizableContent content);

        Task ClearResizableContent();

        Task SetBackgroundColor(string color, bool setEraserColor);
        #endregion

    }

}