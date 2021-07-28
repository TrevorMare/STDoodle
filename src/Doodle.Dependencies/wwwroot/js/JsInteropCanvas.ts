
//import './vendor.min.js';
export class DoodleCanvas {

  private _canvas: HTMLCanvasElement;
  private _isDrawing: boolean = false;
  private _context: CanvasRenderingContext2D;

  constructor(canvas: HTMLCanvasElement) {
    this._canvas = canvas;
    this._context = this._canvas.getContext('2d')
    this.SetupHandlers();
  }

  private BeginDraw(event): void {
    if (this._isDrawing == false) {
      this._context.beginPath();

      this._context.moveTo(event.pageX - this._canvas.offsetLeft, event.pageY - this._canvas.offsetTop);

      this._isDrawing = true;
    }
  }

  private DrawLine(event): void {
    if (this._isDrawing == false) return;
    this._context.lineTo(event.pageX - this._canvas.offsetLeft, event.pageY - this._canvas.offsetTop);
    this._context.stroke();
  }

  private EndDraw(event): void {
    if (this._isDrawing == false) return;
    this.DrawLine(event);
    this._isDrawing = false;
  }


  private SetupHandlers(): void {
    // Attach the event handlers
    this._canvas.addEventListener('touchstart', (e) => { this.BeginDraw(e.touches[0]); }, false);
    this._canvas.addEventListener('mousedown', (e) => { this.BeginDraw(e); }, false);

    this._canvas.addEventListener('touchend', (e) => { this.EndDraw(e.touches[0]); }, false);
    this._canvas.addEventListener('mouseup', (e) => { this.EndDraw(e); }, false);

    this._canvas.addEventListener('touchmove', (e) => { this.DrawLine(e.touches[0]);  e.preventDefault(); }, false);
    this._canvas.addEventListener('mousemove', (e) => { this.DrawLine(e); }, false);
  }

  private DestroyHandlers(): void {
    // Remove the event handlers
    this._canvas.removeEventListener('touchstart', (e) => {});
    this._canvas.addEventListener('mousedown', (e) => {});

    this._canvas.addEventListener('touchend', (e) => {});
    this._canvas.addEventListener('mouseup', (e) => {});

    this._canvas.addEventListener('touchmove', (e) => {});
    this._canvas.addEventListener('mousemove', (e) => {});
  }

}


export function InitialiseCanvas(renderElement: HTMLElement): void {

  const doodleCanvas: DoodleCanvas = new DoodleCanvas(<HTMLCanvasElement>renderElement);


} 


