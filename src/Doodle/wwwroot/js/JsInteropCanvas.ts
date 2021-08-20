
//import './vendor.min.js';

export interface ICanvasPathPoint {
  X: number;
  Y: number;
}

export interface ICanvasPath {
  BrushSize: number;
  BrushColor: string;
  Id: string;
  Display: boolean;
  Points: ICanvasPathPoint[];
  Created: number;
}

export enum GridType {
  None = 0,
  Grid = 1,
  Point = 2
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

  private _currentCanvasPath: ICanvasPath;
  private _brushSize: number = 1;
  private _brushColor: string = "#000000";

  constructor(canvas: HTMLCanvasElement, resizeElement: HTMLElement, callbackRef: any, initColor: string, initSize: number, gridSize: number, gridColor: string, gridType: GridType) {
    this._canvas = canvas;
    this._context = this._canvas.getContext('2d');
    this._callbackRef = callbackRef;
    this._resizeElement = resizeElement;
    this._gridSize = gridSize;
    this._gridColor = gridColor;
    this._gridType = gridType;

    this.SetupHandlers();

    if (!!initColor && initColor !== '') {
      this.SetBrushColor(initColor);
      console.log(`Setting initial color to ${initColor}`)
    }

    if (!!initSize && initSize > 0) {
      this.SetBrushSize(initSize);
      console.log(`Setting initial size to ${initSize}`)
    }

    this.ResizeComponent();
    this.DrawGridLayout();
  }

  public SetBrushColor(color: string): void {
    this._brushColor = color;
  }

  public SetBrushSize(size: number): void {
    this._brushSize = size;
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

  public Clear(clearCommands: boolean): void {
    if (clearCommands === true) {
      this._commands = [];
    }
    this._context.clearRect(0, 0, this._canvas.width, this._canvas.height);

    this.DrawGridLayout();
    this.NotifyBlazorCommands();
  }

  public Refresh(): void {
    this.Clear(false);

    if (!!this._commands && this._commands.length > 0) {
      this._commands.forEach(command => {
        this.DrawPathFromCommand(command);
      });
    }
  }

  public Restore(commandJson: string): void {
    this.Clear(true);
    if (!!commandJson && commandJson !== '') {
      this._commands = JSON.parse(commandJson);
    }
    this.Refresh();
  }

  public Undo(): boolean {
    let result: boolean = false;
    if (this.CanUndo()) {

      for (let iCommand: number = this._commands.length - 1; iCommand >= 0; iCommand--) {
        if (this._commands[iCommand].Display === true) {
          this._commands[iCommand].Display = false;
          result = true;
          break;
        }
      }

      this.Refresh();
      this.NotifyBlazorCommands();
    }
    return result;
  }

  public Redo(): boolean {
    let result: boolean = false;
    if (this.CanRedo()) {

      for (let iCommand: number = 0; iCommand < this._commands.length; iCommand++) {
        if (this._commands[iCommand].Display === false) {
          this._commands[iCommand].Display = true;
          result = true;
          break;
        }
      }

      this.Refresh();
      this.NotifyBlazorCommands();
    }
    return result;
  }

  public CanUndo(): boolean {
    return (this._commands.findIndex(c => c.Display === true) >= 0);
  }

  public CanRedo(): boolean {
    return (this._commands.findIndex(c => c.Display === false) >= 0);
  }

  private SetupHandlers(): void {
    // Attach the event handlers
    this._canvas.addEventListener('touchstart', (e) => { this.StartDraw(e.touches[0]); }, false);
    this._canvas.addEventListener('mousedown', (e) => { this.StartDraw(e); }, false);

    this._canvas.addEventListener('touchend', (e) => { this.EndDraw(e.touches[0]); }, false);
    this._canvas.addEventListener('mouseup', (e) => { this.EndDraw(e); }, false);

    this._canvas.addEventListener('touchmove', (e) => { this.DrawMovement(e.touches[0]);  e.preventDefault(); }, false);
    this._canvas.addEventListener('mousemove', (e) => { this.DrawMovement(e); }, false);

    if (!!this._resizeElement) {
      this._resizeElement.addEventListener('resize', (e) => { this.ResizeComponent(); })
    }
  }

  private DestroyHandlers(): void {
    // Remove the event handlers
    this._canvas.removeEventListener('touchstart', (e) => {});
    this._canvas.removeEventListener('mousedown', (e) => {});

    this._canvas.removeEventListener('touchend', (e) => {});
    this._canvas.removeEventListener('mouseup', (e) => {});

    this._canvas.removeEventListener('touchmove', (e) => {});
    this._canvas.removeEventListener('mousemove', (e) => {});

    if (!!this._resizeElement) {
      this._resizeElement.removeEventListener('resize', (e) => { this.ResizeComponent(); })
    }
  }

  private GetEventPosition(event: any) : any {
    var rect = event.target.getBoundingClientRect();
    var x = event.clientX - rect.left; 
    var y = event.clientY - rect.top;  
    return { x: x, y: y };
  }

  private StartDraw(event): void {
    if (this._isDrawing == false) {
      // Calculate the coords
      const coords = this.GetEventPosition(event);

      this._context.lineWidth = this._brushSize;
      this._context.strokeStyle = this._brushColor;

      this._context.beginPath();
      this._context.moveTo(coords.x, coords.y);

      // Initialise the current path command
      this.StartCurrentPath(coords.x, coords.y);

      // Set the current is drawing value
      this._isDrawing = true;
    }
  }

  private DrawMovement(event): void {
    if (this._isDrawing == false) return;

     // Calculate the coords
    const coords = this.GetEventPosition(event);
    this._context.lineTo(coords.x, coords.y);
    this._context.stroke();

    if (!!this._currentCanvasPath) {
      this._currentCanvasPath.Points.push( { X: coords.x, Y: coords.y } );
    }
  }

  private EndDraw(event): void {
    if (this._isDrawing == false) return;
    this.DrawMovement(event);
    this._isDrawing = false;

    this.EndCurrentPath();
  }

  private StartCurrentPath(x: number, y: number): void {
    const id: string = Date.now().toString(18) + Math.random().toString(36).substring(2);
    const path: ICanvasPath = { BrushColor: this._brushColor, BrushSize: this._brushSize, Created: Date.now(), Display: true, Id: id, Points: [] };
    path.Points.push( { X: x, Y : y } );
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
    if (!!path && path.Display === true && path.Points.length > 0) {
      this._context.lineWidth = path.BrushSize;
      this._context.strokeStyle = path.BrushColor;

      this._context.beginPath();
      this._context.moveTo(path.Points[0].X, path.Points[0].Y);

      for (let iPoint:number = 0; iPoint < path.Points.length; iPoint++) {
        this._context.lineTo(path.Points[iPoint].X, path.Points[iPoint].Y);
      }

      this._context.stroke();
    }
  }

  private NotifyBlazorCommands(): void {
    if (!!this._callbackRef) {
      const commandJson = JSON.stringify(this._commands);
      this._callbackRef.invokeMethodAsync("OnCanvasUpdated", commandJson);
    }
  }

  private ResizeComponent(): void  {
    if (!!this._resizeElement) {
      this._canvas.width = this._resizeElement.clientWidth;
      this._canvas.height = this._resizeElement.clientHeight;
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
}

export let _doodleCanvas: DoodleCanvas;

export function InitialiseCanvas(renderElement: HTMLElement, resizeElement: HTMLElement, callbackRef: any, 
                                 initColor: string, initSize: number, gridSize: number, gridColor: string,
                                 gridType: GridType): void { _doodleCanvas = new DoodleCanvas(<HTMLCanvasElement>renderElement, resizeElement, callbackRef, initColor, initSize, gridSize, gridColor, gridType); } 
export function SetBrushColor(color: string): void { _doodleCanvas.SetBrushColor(color); }
export function SetBrushSize(size: number): void { _doodleCanvas.SetBrushSize(size); }
export function Destroy(): void { _doodleCanvas.Destroy() }
export function Clear(clearCommands: boolean): void { _doodleCanvas.Clear(clearCommands); }
export function Refresh(): void { _doodleCanvas.Refresh(); }
export function Restore(commandJson: string): void { _doodleCanvas.Restore(commandJson); }
export function Undo(): boolean { return _doodleCanvas.Undo(); }
export function Redo(): boolean { return _doodleCanvas.Redo(); }
export function CanUndo(): boolean { return _doodleCanvas.CanUndo(); }
export function CanRedo(): boolean { return _doodleCanvas.CanRedo(); }
export function SetGridSize(size: number): void { _doodleCanvas.SetGridSize(size); }
export function SetGridColor(color: string): void { _doodleCanvas.SetGridColor(color); }
export function SetGridType(gridType: GridType): void { _doodleCanvas.SetGridType(gridType); }

