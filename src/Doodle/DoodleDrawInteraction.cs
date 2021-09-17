using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Common;
using Doodle.Abstractions.Interfaces;
using Doodle.Abstractions.Models;

namespace Doodle
{

    public class DoodleDrawInteraction : Abstractions.Interfaces.IDoodleDrawInteraction
    {

        #region Members
        private readonly Abstractions.Config.DoodleDrawConfig _config;
        private readonly IDoodleStateManager _doodleStateManager;
        #endregion

        #region ctor
        public DoodleDrawInteraction(Abstractions.Config.DoodleDrawConfig config,
                                     IDoodleStateManager stateManager)
        {
            this._config = config;
            this._doodleStateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
            this.InitialiseConfiguration();
        }
        #endregion

        #region Initialise Methods
        private void InitialiseConfiguration()
        {
            // Setup the Canvas Configuration
            if (this._config == null) return;

            this.StrokeColor = this._config.DefaultStrokeColor ?? "#000";
            this.StrokeWidth = this._config.DefaultStrokeSize;
            this.EraserWidth = this._config.DefaultEraserSize;
            this.EraserColor = this._config.BackgroundColor ?? "#ffffff";
            this.BackgroundColor = this._config.BackgroundColor ?? "#ffffff";

            // Setup the canvas grid configuration
            if (this._config.CanvasConfig != null)
            {
                this.GridColor = this._config.CanvasConfig.GridColor ?? "#000";
                this.GridSize = this._config.CanvasConfig.GridSize;
                this.GridType = this._config.CanvasConfig.GridType;
                this.UpdateResolution = this._config.CanvasConfig.UpdateResolution;
            }
        }
        #endregion

        #region Properties
        public string StrokeColor { get; private set; } = "#000";

        public double StrokeWidth { get; private set; } = 1;

        public double EraserWidth { get; private set; } = 5;

        public Abstractions.Common.GridType GridType { get; private set; } = Abstractions.Common.GridType.None;
        
        public int GridSize { get; private set; } = 20;

        public string GridColor { get; private set; } = "#000";

        public Abstractions.Common.DrawMode DrawMode { get; private set; } = Abstractions.Common.DrawMode.Canvas;

        public Abstractions.Common.DrawType DrawType { get; private set; } = Abstractions.Common.DrawType.Pen;

        public ToolbarContent ToolbarContent { get; private set; } = ToolbarContent.None; 

        public string EraserColor { get; private set; } = "#ffffff";

        public string BackgroundColor { get; private set; } = "#ffffff";

        public IDoodleStateManager DoodleStateManager => _doodleStateManager; 

        public int UpdateResolution { get; private set; } = 1;
        #endregion

        #region Events
        public event OnIntChangedHandler OnUpdateResolutionChanged;
        public event OnToolbarContentChangedHandler OnToolbarContentChanged;
        public event EventHandler OnStateHasChanged;
        public event OnColorChangedHandler OnStrokeColorChanged;
        public event OnSizeChangedHandler OnStrokeWidthChanged;
        public event OnSizeChangedHandler OnCanvasGridSizeChanged;
        public event OnCanvasGridTypeChangedHandler OnCanvasGridTypeChanged;
        public event OnColorChangedHandler OnCanvasGridColorChanged;
        public event OnDrawModeChangedHandler OnDrawModeChanged;
        public event EventHandler OnExportImage;
        public event EventHandler OnSaveDoodleData;
        public event OnRestoreHandler OnRestoreDoodleData;
        public event EventHandler OnRedrawCanvas;
        public event OnDrawTypeChangedHandler OnDrawTypeChanged;
        public event OnSizeChangedHandler OnEraserSizeChanged;
        public event OnColorChangedHandler OnEraserColorChanged;
        public event OnColorChangedHandler OnBackgroundColorChanged;
        #endregion

        #region Methods
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

        public Task SetEraserColor(string color)
        {
            if (color != this.EraserColor)
            {
                this.EraserColor = color;
                this.OnEraserColorChanged?.Invoke(this, color);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetStrokeWidth(double width)
        {
            width = Math.Max(width, 1);

            if (width != this.StrokeWidth)
            {
                this.StrokeWidth = width;
                this.OnStrokeWidthChanged?.Invoke(this, width);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetEraserWidth(double width)
        {
            width = Math.Max(width, 1);

            if (width != this.EraserWidth)
            {
                this.EraserWidth = width;
                this.OnEraserSizeChanged?.Invoke(this, width);
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
            gridSize = Math.Max(gridSize, 5);
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

        public Task SetDrawMode(Abstractions.Common.DrawMode drawMode)
        {
            if (drawMode != this.DrawMode)
            {
                this.DrawMode = drawMode;
                this.OnDrawModeChanged?.Invoke(this, drawMode);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task SetDrawType(Abstractions.Common.DrawType drawType)
        {
            if (drawType != this.DrawType)
            {
                switch (drawType)
                {
                    case DrawType.Pen:
                    case DrawType.Eraser: 
                    case DrawType.Line:
                    {
                        this.SetDrawMode(DrawMode.Canvas).ConfigureAwait(false);
                        break;
                    }
                    case DrawType.ResizableText:
                    case DrawType.ResizableImage:
                    {
                        this.SetDrawMode(DrawMode.Resizable).ConfigureAwait(false);
                        break;
                    }
                    default:
                        throw new ArgumentException($"Unknown value for draw type {drawType}");
                }

                this.DrawType = drawType;
                this.OnDrawTypeChanged?.Invoke(this, drawType);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task ExportImage()
        {
            OnExportImage?.Invoke(this, null);
            return Task.CompletedTask;
        }

        public Task RestoreDoodleData(string jsonData)
        {
            OnRestoreDoodleData?.Invoke(this, jsonData);
            return Task.CompletedTask;
        }

        public Task SaveDoodleData()
        {
            OnSaveDoodleData?.Invoke(this, null);
            return Task.CompletedTask;
        }

        public Task RedrawCanvas()
        {
            OnRedrawCanvas?.Invoke(this, null);
            return Task.CompletedTask;
        }

        public Task SetToolbarContent(ToolbarContent toolbarContent)
        {
            if (toolbarContent != this.ToolbarContent)
            {
                this.ToolbarContent = toolbarContent;
                this.OnToolbarContentChanged?.Invoke(this, toolbarContent);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }

        public Task ToggleToolbarContent(ToolbarContent toolbarContent)
        {
            if (toolbarContent != ToolbarContent.None)
            {
                if (toolbarContent == this.ToolbarContent)
                {
                    this.ToolbarContent = ToolbarContent.None;
                    this.OnToolbarContentChanged?.Invoke(this, ToolbarContent.None);
                    this.OnStateHasChanged?.Invoke(this, null);
                }
                else
                {
                    this.ToolbarContent = toolbarContent;
                    this.OnToolbarContentChanged?.Invoke(this, toolbarContent);
                    this.OnStateHasChanged?.Invoke(this, null);
                }
            }

            return Task.CompletedTask;
        }

        public async Task SetBackgroundColor(string color, bool setEraserColor)
        {
            if (this.BackgroundColor != color)
            {
                if (setEraserColor)
                {
                    await this.SetEraserColor(color);
                }
                this.BackgroundColor = color;
                this.OnBackgroundColorChanged?.Invoke(this, this.BackgroundColor);
                this.OnStateHasChanged?.Invoke(this, null);
            }
        }

        public Task SetUpdateResolution(int updateResolution)
        {
            if (this.UpdateResolution != updateResolution)
            {
                this.UpdateResolution = updateResolution;
                this.OnUpdateResolutionChanged?.Invoke(this, this.UpdateResolution);
                this.OnStateHasChanged?.Invoke(this, null);
            }
            return Task.CompletedTask;
        }
        #endregion
        
    }
}