
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

export class DoodleCanvas {

  private _canvas: HTMLCanvasElement;
  private _isDrawing: boolean = false;
  private _context: CanvasRenderingContext2D;
  private _commands: ICanvasPath[] = [];
  private _callbackRef: any;
  private _resizeElement: HTMLElement;

  private _currentCanvasPath: ICanvasPath;
  private _brushSize: number = 1;
  private _brushColor: string = "#000000";

  constructor(canvas: HTMLCanvasElement, resizeElement: HTMLElement, callbackRef: any, initColor: string, initSize: number) {
    this._canvas = canvas;
    this._context = this._canvas.getContext('2d');
    this._callbackRef = callbackRef;
    this._resizeElement = resizeElement;
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

  public Clear(clearCommands: boolean): void {
    if (clearCommands === true) {
      this._commands = [];
    }
    this._context.clearRect(0, 0, this._canvas.width, this._canvas.height);
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

  private StartDraw(event): void {
    if (this._isDrawing == false) {

      // Calculate the coords
      const x: number = event.pageX - this._canvas.offsetLeft;
      const y: number = event.pageY - this._canvas.offsetTop;

      this._context.lineWidth = this._brushSize;
      this._context.strokeStyle = this._brushColor;

      this._context.beginPath();
      this._context.moveTo(x, y);

      // Initialise the current path command
      this.StartCurrentPath(x, y);

      // Set the current is drawing value
      this._isDrawing = true;
    }
  }

  private DrawMovement(event): void {
    if (this._isDrawing == false) return;

    const x: number = event.pageX - this._canvas.offsetLeft;
    const y: number = event.pageY - this._canvas.offsetTop;

    this._context.lineTo(x, y);
    this._context.stroke();

    if (!!this._currentCanvasPath) {
      this._currentCanvasPath.Points.push( { X: x, Y: y } );
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

  private ResizeComponent() {
    if (!!this._resizeElement) {
      this._canvas.width = this._resizeElement.clientWidth;
      this._canvas.height = this._resizeElement.clientHeight;
    }
  }
}

export let _doodleCanvas: DoodleCanvas;

export function InitialiseCanvas(renderElement: HTMLElement, resizeElement: HTMLElement, callbackRef: any, initColor: string, initSize: number): void { _doodleCanvas = new DoodleCanvas(<HTMLCanvasElement>renderElement, resizeElement, callbackRef, initColor, initSize); } 
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


