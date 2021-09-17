using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Interfaces
{

    public delegate void OnToolbarContentChangedHandler(object sender, ToolbarContent toolbarContent);
    public delegate void OnColorChangedHandler(object sender, string color);
    public delegate void OnSizeChangedHandler(object sender, double size);
    public delegate void OnBoolChangedHandler(object sender, bool value);
    public delegate void OnIntChangedHandler(object sender, int value);
    public delegate void OnCanvasGridTypeChangedHandler(object sender, Abstractions.Common.GridType gridType);
    public delegate void OnDrawModeChangedHandler(object sender, Abstractions.Common.DrawMode drawMode);
    public delegate void OnDrawTypeChangedHandler(object sender, Abstractions.Common.DrawType drawMode);
    public delegate void OnRestoreHandler(object sender, string jsonContent);

    public interface IDoodleDrawInteraction
    {

        #region Events
        event EventHandler OnStateHasChanged;
        event OnColorChangedHandler OnStrokeColorChanged;
        event OnColorChangedHandler OnEraserColorChanged;
        event OnSizeChangedHandler OnStrokeWidthChanged;
        event OnSizeChangedHandler OnCanvasGridSizeChanged;
        event OnSizeChangedHandler OnEraserSizeChanged;
        event OnCanvasGridTypeChangedHandler OnCanvasGridTypeChanged;
        event OnColorChangedHandler OnCanvasGridColorChanged;
        event OnDrawModeChangedHandler OnDrawModeChanged;
        event EventHandler OnExportImage;
        event EventHandler OnSaveDoodleData;
        event OnRestoreHandler OnRestoreDoodleData;
        event EventHandler OnRedrawCanvas; 
        event OnToolbarContentChangedHandler OnToolbarContentChanged;
        event OnDrawTypeChangedHandler OnDrawTypeChanged;
        event OnColorChangedHandler OnBackgroundColorChanged;
        event OnIntChangedHandler OnUpdateResolutionChanged;
        #endregion

        #region Properties
        string StrokeColor { get; }

        double StrokeWidth { get; }

        double EraserWidth { get; }

        string EraserColor { get; }

        string BackgroundColor { get; }

        int UpdateResolution { get; }

        Abstractions.Common.GridType GridType { get; } 

        int GridSize { get; } 

        string GridColor { get; }

        Abstractions.Common.DrawMode DrawMode { get; }

        ToolbarContent ToolbarContent { get; }
        
        DrawType DrawType { get; }

        IDoodleStateManager DoodleStateManager { get; }
        #endregion

        #region Methods        
        Task SetStrokeColor(string color);

        Task SetStrokeWidth(double width);

        Task SetEraserWidth(double width);

        Task SetEraserColor(string color);

        Task SetCanvasGridType(Abstractions.Common.GridType gridType);

        Task SetCanvasGridSize(int size);

        Task SetCanvasGridColor(string color);

        Task SetDrawMode(Abstractions.Common.DrawMode drawMode);

        Task RedrawCanvas();

        Task ExportImage();

        Task SaveDoodleData();

        Task RestoreDoodleData(string jsonData);

        Task SetToolbarContent(ToolbarContent toolbarContent);

        Task ToggleToolbarContent(ToolbarContent toolbarContent);
        
        Task SetDrawType(DrawType drawType);

        Task SetBackgroundColor(string color, bool setEraserColor);

        Task SetUpdateResolution(int updateResolution);
        #endregion

    }

}