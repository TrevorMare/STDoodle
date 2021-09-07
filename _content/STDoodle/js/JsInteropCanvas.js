export class DoodleCanvas {
    constructor(canvas, resizeElement, callbackRef, initColor, initSize, gridSize, gridColor, gridType, drawType, eraserColor) {
        this._isDrawing = false;
        this._commands = [];
        this._gridSize = 10;
        this._gridColor = "gray";
        this._gridType = 1;
        this._drawType = 1;
        this._brushSize = 1;
        this._brushColor = "#000000";
        this._erasorBrushSize = 5;
        this._erasorColor = "#ffffff";
        this._drawSize = 1;
        this._drawColor = "#000000";
        this._canvas = canvas;
        this._context = this._canvas.getContext('2d');
        this._callbackRef = callbackRef;
        this._resizeElement = resizeElement;
        this._gridSize = gridSize;
        this._gridColor = gridColor;
        this._gridType = gridType;
        this._drawType = drawType;
        this._drawPreviewCanvas = this._resizeElement.querySelector('.canvas-preview');
        if (!!this._drawPreviewCanvas) {
            this._drawPreviewContext = this._drawPreviewCanvas.getContext('2d');
        }
        this._inputCanvasCommands = this._resizeElement.querySelector('[data-canvas-commands]');
        this._originalOverscrollBehaviour = document.body.style.overscrollBehavior;
        this.SetupHandlers();
        if (!!initColor && initColor !== '') {
            this.SetBrushColor(initColor);
        }
        if (!!initSize && initSize > 0) {
            this.SetBrushSize(initSize);
        }
        this.ResizeComponent();
        this.DrawGridLayout();
    }
    SetBrushColor(color) {
        this._drawColor = color;
    }
    SetBrushSize(size) {
        this._drawSize = size;
    }
    SetEraserSize(size) {
        this._erasorBrushSize = size;
    }
    SetEraserColor(color) {
        this._erasorColor = color;
    }
    Destroy() {
        this.DestroyHandlers();
        this._commands = [];
        this._context = null;
        this._canvas = null;
    }
    SetGridSize(size) {
        this._gridSize = size;
        this.Refresh();
    }
    SetGridColor(color) {
        this._gridColor = color;
        this.Refresh();
    }
    SetGridType(gridType) {
        this._gridType = gridType;
        this.Refresh();
    }
    SetDrawType(drawType) {
        this._drawType = drawType;
        this.PrepareDrawType();
    }
    PrepareDrawType() {
        if (!!this._drawPreviewCanvas) {
            if (this._drawType === 2) {
                this._drawPreviewCanvas.style.display = 'block';
            }
            else {
                this._drawPreviewCanvas.style.display = 'none';
            }
        }
    }
    Restore(commandJson) {
        if (!!commandJson && commandJson !== '') {
            this._commands = JSON.parse(commandJson);
        }
        else {
            this._commands = [];
        }
        this.Refresh();
    }
    Refresh() {
        this.Clear();
        if (!!this._commands && this._commands.length > 0) {
            this._commands.forEach(command => {
                this.DrawPathFromCommand(command);
            });
        }
    }
    Clear() {
        this._context.clearRect(0, 0, this._canvas.width, this._canvas.height);
        this.DrawGridLayout();
    }
    SetupHandlers() {
        this._drawStartHandler = this.StartDraw.bind(this);
        this._drawEndHandler = this.EndDraw.bind(this);
        this._drawMoveHandler = this.DrawMovement.bind(this);
        this._resizeHandler = this.ResizeComponent.bind(this);
        this.AttachHandlers(this._canvas);
        this.AttachHandlers(this._drawPreviewCanvas);
        if (!!this._resizeElement) {
            this._resizeElement.addEventListener('resize', this._resizeHandler, false);
        }
    }
    AttachHandlers(canvasElement) {
        canvasElement.addEventListener('touchstart', this._drawStartHandler, { passive: false });
        canvasElement.addEventListener('mousedown', this._drawStartHandler, false);
        canvasElement.addEventListener('touchend', this._drawEndHandler, { passive: false });
        canvasElement.addEventListener('mouseup', this._drawEndHandler, false);
        canvasElement.addEventListener('touchmove', this._drawMoveHandler, { passive: false });
        canvasElement.addEventListener('mousemove', this._drawMoveHandler, false);
    }
    DestroyHandlers() {
        this.RemoveHandlers(this._canvas);
        this.RemoveHandlers(this._drawPreviewCanvas);
        if (!!this._resizeElement) {
            this._resizeElement.removeEventListener('resize', this._resizeHandler);
        }
    }
    RemoveHandlers(canvasElement) {
        canvasElement.removeEventListener('touchstart', this._drawStartHandler);
        canvasElement.removeEventListener('mousedown', this._drawStartHandler);
        canvasElement.removeEventListener('touchend', this._drawEndHandler);
        canvasElement.removeEventListener('mouseup', this._drawEndHandler);
        canvasElement.removeEventListener('touchmove', this._drawMoveHandler);
        canvasElement.removeEventListener('mousemove', this._drawMoveHandler);
    }
    GetEventPosition(event) {
        var rect = event.target.getBoundingClientRect();
        var x = event.clientX - rect.left;
        var y = event.clientY - rect.top;
        return { x: x, y: y };
    }
    StartDraw(e) {
        if (this._isDrawing == false) {
            this._lastMoveEvent = null;
            const event = this.GetInternalEvent(e);
            e.preventDefault();
            document.body.style.overscrollBehavior = "contain";
            this.SetupBrush();
            const coords = this.GetEventPosition(event);
            if (this._drawType === 2) {
                this._shapeDrawDownCoords = { x: coords.x, y: coords.y };
            }
            else {
                this._context.lineWidth = this._brushSize;
                this._context.strokeStyle = this._brushColor;
                this._context.beginPath();
                this._context.moveTo(coords.x, coords.y);
                this.StartCurrentPath(coords.x, coords.y);
            }
            this._isDrawing = true;
        }
    }
    SetupBrush() {
        if (this._drawType === 3) {
            this._brushSize = this._erasorBrushSize;
            this._brushColor = this._erasorColor;
        }
        else {
            this._brushSize = this._drawSize;
            this._brushColor = this._drawColor;
        }
    }
    DrawMovement(e) {
        if (this._isDrawing == false || !e)
            return;
        const event = this.GetInternalEvent(e);
        e.preventDefault();
        this._lastMoveEvent = event;
        const coords = this.GetEventPosition(event);
        if (this._drawType === 2) {
            this._drawPreviewContext.clearRect(0, 0, this._drawPreviewCanvas.width, this._drawPreviewCanvas.height);
            this._drawPreviewContext.lineWidth = this._brushSize;
            this._drawPreviewContext.strokeStyle = this._brushColor;
            this._drawPreviewContext.beginPath();
            this._drawPreviewContext.moveTo(this._shapeDrawDownCoords.x, this._shapeDrawDownCoords.y);
            this._drawPreviewContext.lineTo(coords.x, coords.y);
            this._drawPreviewContext.stroke();
        }
        else {
            this._context.lineTo(coords.x, coords.y);
            this._context.stroke();
            if (!!this._currentCanvasPath) {
                this._currentCanvasPath.points.push({ x: coords.x, y: coords.y });
            }
        }
    }
    EndDraw(e) {
        if (this._isDrawing == false)
            return;
        if (this._drawType === 2) {
            const coords = this.GetEventPosition(this._lastMoveEvent);
            this.StartCurrentPath(this._shapeDrawDownCoords.x, this._shapeDrawDownCoords.y);
            this._currentCanvasPath.points.push({ x: coords.x, y: coords.y });
            this.EndCurrentPath();
            this._drawPreviewContext.clearRect(0, 0, this._drawPreviewCanvas.width, this._drawPreviewCanvas.height);
            this._isDrawing = false;
            this.Refresh();
        }
        else {
            this._isDrawing = false;
            this.EndCurrentPath();
        }
        document.body.style.overscrollBehavior = this._originalOverscrollBehaviour;
    }
    StartCurrentPath(x, y) {
        const id = Date.now().toString(18) + Math.random().toString(36).substring(2);
        const path = { brushColor: this._brushColor, brushSize: this._brushSize, created: Date.now(), display: true, id: id, points: [], type: this._drawType };
        path.points.push({ x: x, y: y });
        this._currentCanvasPath = path;
    }
    EndCurrentPath() {
        if (!!this._currentCanvasPath) {
            this._commands.push(this._currentCanvasPath);
            this._currentCanvasPath = null;
            this.NotifyBlazorCommands();
        }
    }
    DrawPathFromCommand(path) {
        if (!!path && path.display === true && path.points.length > 0) {
            this._context.lineWidth = path.brushSize;
            this._context.strokeStyle = path.brushColor;
            this._context.beginPath();
            this._context.moveTo(path.points[0].x, path.points[0].y);
            for (let iPoint = 0; iPoint < path.points.length; iPoint++) {
                this._context.lineTo(path.points[iPoint].x, path.points[iPoint].y);
            }
            this._context.stroke();
        }
    }
    NotifyBlazorCommands() {
        if (!!this._callbackRef && !!this._inputCanvasCommands) {
            const commandJson = JSON.stringify(this._commands);
            this._inputCanvasCommands.value = commandJson;
            this._inputCanvasCommands.dispatchEvent(new Event('change'));
            this._callbackRef.invokeMethodAsync("OnCanvasUpdated", "");
        }
    }
    ResizeComponent() {
        if (!!this._resizeElement) {
            this._canvas.width = this._resizeElement.clientWidth;
            this._canvas.height = this._resizeElement.clientHeight;
            if (!!this._drawPreviewCanvas) {
                this._drawPreviewCanvas.width = this._resizeElement.clientWidth;
                this._drawPreviewCanvas.height = this._resizeElement.clientHeight;
            }
        }
    }
    DrawGridLayout() {
        let svgData = "";
        if (this._gridType === 1) {
            svgData = `<svg width="100%" height="100%" xmlns="http://www.w3.org/2000/svg"> 
                    <defs> 
                        <pattern id="smallGrid" width="${this._gridSize}" height="${this._gridSize}" patternUnits="userSpaceOnUse"> 
                            <path d="M ${this._gridSize} 0 L 0 0 0 ${this._gridSize}" fill="none" stroke="${this._gridColor}" stroke-width="0.5" /> 
                        </pattern> 
                        <pattern id="grid" width="${this._gridSize * 10}" height="${this._gridSize * 10}" patternUnits="userSpaceOnUse"> 
                            <rect width="${this._gridSize * 10}" height="${this._gridSize * 10}" fill="url(#smallGrid)" /> 
                            <path d="M ${this._gridSize * 10} 0 L 0 0 0 ${this._gridSize * 10}" fill="none" stroke="${this._gridColor}" stroke-width="1" /> 
                        </pattern> 
                    </defs> 
                    <rect width="100%" height="100%" fill="url(#grid)" /> 
                </svg>`;
        }
        else if (this._gridType === 2) {
            svgData = `<svg width="100%" height="100%" xmlns="http://www.w3.org/2000/svg"> 
                    <defs> 
                        <pattern id="smallGrid" width="${this._gridSize}" height="${this._gridSize}" patternUnits="userSpaceOnUse"> 
                          <circle cx='${this._gridSize}' cy='${this._gridSize}' r='1' stroke-width="1" stroke='${this._gridColor}' />
                        </pattern> 
                    </defs> 
                    <rect width="100%" height="100%" fill="url(#smallGrid)" /> 
                </svg>`;
        }
        else {
            return;
        }
        var DOMURL = window.URL || window.webkitURL || window;
        var img = new Image();
        var svg = new Blob([svgData], { type: 'image/svg+xml;charset=utf-8' });
        var url = DOMURL.createObjectURL(svg);
        const that = this;
        img.onload = function () {
            that._context.drawImage(img, 0, 0);
            DOMURL.revokeObjectURL(url);
        };
        img.src = url;
    }
    GetInternalEvent(e) {
        if (!!e && 'touches' in e) {
            return e.touches[0];
        }
        return e;
    }
}
export let _doodleCanvas;
export function InitialiseCanvas(renderElement, resizeElement, callbackRef, initColor, initSize, gridSize, gridColor, gridType, drawType, eraserColor) { _doodleCanvas = new DoodleCanvas(renderElement, resizeElement, callbackRef, initColor, initSize, gridSize, gridColor, gridType, drawType, eraserColor); }
export function SetBrushColor(color) { _doodleCanvas.SetBrushColor(color); }
export function SetBrushSize(size) { _doodleCanvas.SetBrushSize(size); }
export function Destroy() { _doodleCanvas.Destroy(); }
export function Clear() { _doodleCanvas.Clear(); }
export function Refresh() { _doodleCanvas.Refresh(); }
export function Restore(commandJson) { _doodleCanvas.Restore(commandJson); }
export function SetGridSize(size) { _doodleCanvas.SetGridSize(size); }
export function SetGridColor(color) { _doodleCanvas.SetGridColor(color); }
export function SetGridType(gridType) { _doodleCanvas.SetGridType(gridType); }
export function SetDrawType(drawType) { _doodleCanvas.SetDrawType(drawType); }
export function SetEraserSize(size) { _doodleCanvas.SetEraserSize(size); }
export function SetEraserColor(color) { _doodleCanvas.SetEraserColor(color); }
