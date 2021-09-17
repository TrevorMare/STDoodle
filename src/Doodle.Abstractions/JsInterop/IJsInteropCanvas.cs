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

        Task InitialiseCanvas(ElementReference forElement, ElementReference resizeElement, string brushColor, int brushSize, int gridSize = 10, string gridColor = "", Common.GridType gridType = Common.GridType.Grid, DrawType drawType = DrawType.Pen, string eraserColor = "#ffffff", int updateResolution = 1);

        Task SetBrushColor(string color);

        Task SetBrushSize(int size);

        Task SetEraserSize(int size);

        Task SetEraserColor(string color);

        Task SetGridSize(int size);

        Task SetGridColor(string color);

        Task Destroy();

        Task Clear();

        Task Refresh();

        Task Restore(string commandJson);

        Task OnCanvasUpdated();
        
        Task SetGridType(GridType gridType);

        Task SetDrawType(DrawType drawType);

        Task SetUpdateResolution(int updateResolution);
    }
}