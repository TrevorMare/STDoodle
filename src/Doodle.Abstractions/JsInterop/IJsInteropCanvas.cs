using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Abstractions.JsInterop
{

    public delegate void OnCanvasUpdatedHandler(object sender, List<Abstractions.Models.CanvasPath> commands);

    public interface IJsInteropCanvas
    {
        event OnCanvasUpdatedHandler CanvasCommandsUpdated;

        Task InitialiseCanvas(ElementReference forElement, ElementReference resizeElement, string brushColor, int brushSize, bool drawGrid = false, int gridSize = 10, string gridColor = "", Common.GridType gridType = Common.GridType.Grid);

        Task SetBrushColor(string color);

        Task SetBrushSize(int size);

        Task ShowGrid(bool show);

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
    }
}