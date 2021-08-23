using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Doodle.Abstractions.Common;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{

    public delegate void OnCanvasUpdatedHandler(object sender, List<Abstractions.Models.CanvasPath> commands);

    public interface IJsInteropCanvas
    {
        event OnCanvasUpdatedHandler CanvasCommandsUpdated;

        Task InitialiseCanvas(ElementReference forElement, ElementReference resizeElement, string brushColor, int brushSize, int gridSize = 10, string gridColor = "", Common.GridType gridType = Common.GridType.Grid, DrawType drawType = DrawType.Pen, string eraserColor = "#ffffff");

        Task SetBrushColor(string color);

        Task SetBrushSize(int size);

        Task SetEraserSize(int size);

        Task SetEraserColor(string color);

        Task SetGridSize(int size);

        Task SetGridColor(string color);

        Task Destroy();

        Task Clear(bool clearHistory);

        Task Refresh();

        Task Restore(string commandJson);

        ValueTask<bool> Undo();
        
        ValueTask<bool> Redo();

        ValueTask<bool> CanUndo();

        ValueTask<bool> CanRedo();

        Task OnCanvasUpdated(string commandJson);
        
        Task SetGridType(GridType gridType);

        Task SetDrawType(DrawType drawType);
    }
}