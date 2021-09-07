
//import './vendor.min.js';

export interface ICanvasPathPoint {
  x: number;
  y: number;
}

export interface ICanvasPath {
  brushSize: number;
  brushColor: string;
  id: string;
  display: boolean;
  points: ICanvasPathPoint[];
  created: number;
  type: DrawType
}

const enum GridType {
  None = 0,
  Grid = 1,
  Point = 2
}

const enum DrawType {
  Pen = 1,
  Line = 2,
  Eraser = 3
}

export class DoodleCanvas {

  private _canvas: HTMLCanvasElement;
  private _isDrawing: boolean = false;
  private _context: CanvasRenderingContext2D;
  private _commands: ICanvasPath[] = [];
  private _callbackRef: any;
  private _resizeElement: HTMLElement;
  private _gridSize: number = 10;
  private _gridColor: string = "gray";
  private _gridType: GridType = GridType.Grid;
  private _drawType: DrawType = DrawType.Pen;
  private _currentCanvasPath: ICanvasPath;
  private _brushSize: number = 1;
  private _brushColor: string = "#000000";
  private _erasorBrushSize: number = 5;
  private _erasorColor: string = "#ffffff";
  private _drawSize: number = 1;
  private _drawColor: string = "#000000";
  private _drawStartHandler: any;
  private _drawEndHandler: any;
  private _drawMoveHandler: any;

  private _resizeHandler: any;

  private _drawPreviewCanvas: HTMLCanvasElement;
  private _drawPreviewContext: CanvasRenderingContext2D;
  private _shapeDrawDownCoords: ICanvasPathPoint;
  private _originalOverscrollBehaviour: string;

  constructor(canvas: HTMLCanvasElement, resizeElement: HTMLElement, callbackRef: any, initColor: string, initSize: number, gridSize: number, gridColor: string, gridType: GridType, drawType: DrawType, eraserColor: string) {
    this._canvas = canvas;
    this._context = this._canvas.getContext('2d');
    this._callbackRef = callbackRef;
    this._resizeElement = resizeElement;
    this._gridSize = gridSize;
    this._gridColor = gridColor;
   
    this._gridType = gridType;
    this._drawType = drawType;

    this._drawPreviewCanvas = this._resizeElement.querySelector('.canvas-preview') as HTMLCanvasElement;
    if (!!this._drawPreviewCanvas) {
      this._drawPreviewContext = this._drawPreviewCanvas.getContext('2d');
    }

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

  public SetBrushColor(color: string): void {
    this._drawColor = color;
  }

  public SetBrushSize(size: number): void {
    this._drawSize = size;
  }

  public SetEraserSize(size: number): void {
    this._erasorBrushSize = size;
  }

  public SetEraserColor(color: string): void {
    this._erasorColor = color;
  }

  public Destroy(): void {
    this.DestroyHandlers();
    this._commands = [];
    this._context = null;
    this._canvas = null;
  }

  public SetGridSize(size: number): void {
    this._gridSize = size;
    this.Refresh();
  }

  public SetGridColor(color: string): void {
    this._gridColor = color;
    this.Refresh();
  }

  public SetGridType(gridType: GridType): void {
    this._gridType = gridType;
    this.Refresh();
  }

  public SetDrawType(drawType: DrawType): void {
    this._drawType = drawType;
    this.PrepareDrawType();
  }

  private PrepareDrawType(): void {
    if (!!this._drawPreviewCanvas) {

      if (this._drawType === DrawType.Line) {
        this._drawPreviewCanvas.style.display = 'block';
      } else {
        this._drawPreviewCanvas.style.display = 'none';
      }
    }
  }
  
  public Restore(commandJson: string): void {

    if (!!commandJson && commandJson !== '') {
      this._commands = JSON.parse(commandJson);
    } else {
      this._commands = [];
    }
    
    this.Refresh();
  }

  public Refresh(): void {
    this.Clear();

    if (!!this._commands && this._commands.length > 0) {
      this._commands.forEach(command => {
        this.DrawPathFromCommand(command);
      });
    }
  }

  public Clear(): void {
    this._context.clearRect(0, 0, this._canvas.width, this._canvas.height);
    this.DrawGridLayout();
  }

  
  private SetupHandlers(): void {

    this._drawStartHandler = this.StartDraw.bind(this);
    this._drawEndHandler = this.EndDraw.bind(this);
    this._drawMoveHandler = this.DrawMovement.bind(this);
    this._resizeHandler = this.ResizeComponent.bind(this);

        // Attach the event handlers
    this.AttachHandlers(this._canvas);
    this.AttachHandlers(this._drawPreviewCanvas);

    if (!!this._resizeElement) {
      this._resizeElement.addEventListener('resize', this._resizeHandler, false);
    }
  }

  private AttachHandlers(canvasElement: HTMLCanvasElement): void {
    canvasElement.addEventListener('touchstart', this._drawStartHandler, { passive: false});
    canvasElement.addEventListener('mousedown', this._drawStartHandler, false);

    canvasElement.addEventListener('touchend', this._drawEndHandler, { passive: false});
    canvasElement.addEventListener('mouseup', this._drawEndHandler, false);

    canvasElement.addEventListener('touchmove', this._drawMoveHandler, { passive: false});
    canvasElement.addEventListener('mousemove', this._drawMoveHandler, false);
  }

  private DestroyHandlers(): void {
    // Remove the event handlers
    this.RemoveHandlers(this._canvas);
    this.RemoveHandlers(this._drawPreviewCanvas);

    if (!!this._resizeElement) {
      this._resizeElement.removeEventListener('resize', this._resizeHandler);
    }
  }

  private RemoveHandlers(canvasElement: HTMLCanvasElement): void {
    canvasElement.removeEventListener('touchstart', this._drawStartHandler);
    canvasElement.removeEventListener('mousedown', this._drawStartHandler);

    canvasElement.removeEventListener('touchend', this._drawEndHandler);
    canvasElement.removeEventListener('mouseup', this._drawEndHandler);

    canvasElement.removeEventListener('touchmove', this._drawMoveHandler);
    canvasElement.removeEventListener('mousemove', this._drawMoveHandler);
  }

  private GetEventPosition(event: any) : any {
    var rect = event.target.getBoundingClientRect();
    var x = event.clientX - rect.left; 
    var y = event.clientY - rect.top;  
    return { x: x, y: y };
  }

  private StartDraw(e: any): void {
    if (this._isDrawing == false) {
      this._lastMoveEvent = null;
      
      const event = this.GetInternalEvent(e);
      e.preventDefault();
      document.body.style.overscrollBehavior = "contain";

      this.SetupBrush();

      // Calculate the coords
      const coords = this.GetEventPosition(event);

      if (this._drawType === DrawType.Line) {
        this._shapeDrawDownCoords = {x: coords.x, y: coords.y};
      } else {
        this._context.lineWidth = this._brushSize;
        this._context.strokeStyle = this._brushColor;
        this._context.beginPath();
        this._context.moveTo(coords.x, coords.y);
        // Initialise the current path command
        this.StartCurrentPath(coords.x, coords.y);
      }
      // Set the current is drawing value
      this._isDrawing = true;
    }
  }

  private SetupBrush() {
    if (this._drawType === DrawType.Eraser) {
      this._brushSize = this._erasorBrushSize;
      this._brushColor = this._erasorColor;
    } else {
      this._brushSize = this._drawSize;
      this._brushColor = this._drawColor;
    }
  }

  private _lastMoveEvent: any;

  private DrawMovement(e: any): void {
    if (this._isDrawing == false || !e) return;
    
    const event = this.GetInternalEvent(e);
    e.preventDefault();
    this._lastMoveEvent = event;
     // Calculate the coords
    const coords = this.GetEventPosition(event);

    if (this._drawType === DrawType.Line) {
      this._drawPreviewContext.clearRect(0, 0, this._drawPreviewCanvas.width, this._drawPreviewCanvas.height);
      this._drawPreviewContext.lineWidth = this._brushSize;
      this._drawPreviewContext.strokeStyle = this._brushColor;
      this._drawPreviewContext.beginPath();
      this._drawPreviewContext.moveTo(this._shapeDrawDownCoords.x, this._shapeDrawDownCoords.y);
      this._drawPreviewContext.lineTo(coords.x, coords.y);
      this._drawPreviewContext.stroke();
    } else {
      this._context.lineTo(coords.x, coords.y);
      this._context.stroke();
  
      if (!!this._currentCanvasPath) {
        this._currentCanvasPath.points.push( { x: coords.x, y: coords.y } );
      }
    }
  }

  private EndDraw(e: any): void {
    if (this._isDrawing == false) return;
    
    if (this._drawType === DrawType.Line) {
      const coords = this.GetEventPosition(this._lastMoveEvent);
      this.StartCurrentPath(this._shapeDrawDownCoords.x, this._shapeDrawDownCoords.y);
      this._currentCanvasPath.points.push( { x: coords.x, y: coords.y } );
      this.EndCurrentPath();
      this._drawPreviewContext.clearRect(0, 0, this._drawPreviewCanvas.width, this._drawPreviewCanvas.height);
      this._isDrawing = false;
      this.Refresh();
    } else {
      this._isDrawing = false;
      this.EndCurrentPath();
    }
    document.body.style.overscrollBehavior = this._originalOverscrollBehaviour;
  }

  private StartCurrentPath(x: number, y: number): void {
    const id: string = Date.now().toString(18) + Math.random().toString(36).substring(2);
    const path: ICanvasPath = { brushColor: this._brushColor, brushSize: this._brushSize, created: Date.now(), display: true, id: id, points: [], type: this._drawType };
    path.points.push( { x: x, y : y } );
    this._currentCanvasPath = path;
  }

  private EndCurrentPath(): void {
    if (!!this._currentCanvasPath) {
      this._commands.push(this._currentCanvasPath);
      this._currentCanvasPath = null;
      
      this.NotifyBlazorCommands();
    }
  }

  private DrawPathFromCommand(path: ICanvasPath) {
    if (!!path && path.display === true && path.points.length > 0) {
      this._context.lineWidth = path.brushSize;
      this._context.strokeStyle = path.brushColor;

      this._context.beginPath();
      this._context.moveTo(path.points[0].x, path.points[0].y);

      for (let iPoint:number = 0; iPoint < path.points.length; iPoint++) {
        this._context.lineTo(path.points[iPoint].x, path.points[iPoint].y);
      }

      this._context.stroke();
    }
  }

  private NotifyBlazorCommands(): void {
    if (!!this._callbackRef) {
      // const commandJson = JSON.stringify(this._commands);
      // this._callbackRef.invokeMethodAsync("OnCanvasUpdated", commandJson);
      console.log(`Emulating Callback`);
    }
  }

  private ResizeComponent(): void  {
    if (!!this._resizeElement) {
      this._canvas.width = this._resizeElement.clientWidth;
      this._canvas.height = this._resizeElement.clientHeight;

      if (!!this._drawPreviewCanvas) {
        this._drawPreviewCanvas.width = this._resizeElement.clientWidth;
        this._drawPreviewCanvas.height = this._resizeElement.clientHeight;
      }
    }
  }

  private DrawGridLayout(): void {
    let svgData = "";
    if (this._gridType === GridType.Grid) {
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
    } else if (this._gridType === GridType.Point) {
      svgData = `<svg width="100%" height="100%" xmlns="http://www.w3.org/2000/svg"> 
                    <defs> 
                        <pattern id="smallGrid" width="${this._gridSize}" height="${this._gridSize}" patternUnits="userSpaceOnUse"> 
                          <circle cx='${this._gridSize}' cy='${this._gridSize}' r='1' stroke-width="1" stroke='${this._gridColor}' />
                        </pattern> 
                    </defs> 
                    <rect width="100%" height="100%" fill="url(#smallGrid)" /> 
                </svg>`;
    } else {
      return;
    }
    var DOMURL = window.URL || window.webkitURL || window;

    var img = new Image();
    var svg = new Blob([svgData], {type: 'image/svg+xml;charset=utf-8'});
    // @ts-ignore
    var url = DOMURL.createObjectURL(svg);
    const that = this;
    img.onload = function () {
      that._context.drawImage(img, 0, 0);
      // @ts-ignore
      DOMURL.revokeObjectURL(url);
    }
    img.src = url;
  }

  private GetInternalEvent(e): any {
    if (!!e && 'touches' in e) { return e.touches[0]; }
    
    return e;
  }
}

export let _doodleCanvas: DoodleCanvas;

export function InitialiseCanvas(renderElement: HTMLElement, resizeElement: HTMLElement, callbackRef: any, 
                                 initColor: string, initSize: number, gridSize: number, gridColor: string,
                                 gridType: GridType, drawType: DrawType, eraserColor: string): void { _doodleCanvas = new DoodleCanvas(<HTMLCanvasElement>renderElement, resizeElement, callbackRef, initColor, initSize, gridSize, gridColor, gridType, drawType, eraserColor); } 
export function SetBrushColor(color: string): void { _doodleCanvas.SetBrushColor(color); }
export function SetBrushSize(size: number): void { _doodleCanvas.SetBrushSize(size); }
export function Destroy(): void { _doodleCanvas.Destroy() }
export function Clear(): void { _doodleCanvas.Clear(); }
export function Refresh(): void { _doodleCanvas.Refresh(); }
export function Restore(commandJson: string): void { _doodleCanvas.Restore(commandJson); }
export function SetGridSize(size: number): void { _doodleCanvas.SetGridSize(size); }
export function SetGridColor(color: string): void { _doodleCanvas.SetGridColor(color); }
export function SetGridType(gridType: GridType): void { _doodleCanvas.SetGridType(gridType); }
export function SetDrawType(drawType: DrawType): void { _doodleCanvas.SetDrawType(drawType); }
export function SetEraserSize(size: number): void { _doodleCanvas.SetEraserSize(size); }
export function SetEraserColor(color: string): void { _doodleCanvas.SetEraserColor(color); }

