export interface Dimensions {
    top: number,
    left: number,
    width: number,
    height: number,
    minWidth?: number,
    minHeight?: number
}

export interface Coords {
    x: number,
    y: number
}

export enum E_OperationType {
    Move = 1,
    ResizeTL = 2,
    ResizeTR = 3,
    ResizeBL = 4,
    ResizeBR = 5
}

export interface IDoodleResizeOperation {
    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void,
    ReCalculate(event: any): void,
    EndOperation(event: any): Dimensions,
    CancelOperation(): void,
    get OperationType(): E_OperationType
}

export class DoodleResizeOperation implements IDoodleResizeOperation {
    protected _element: HTMLElement;
    protected _elementDimensions: Dimensions;
    protected _startCoords: Coords;

    protected CalculateDelta(event: any): Dimensions {
        throw new Error("Method not implemented.");
    }

    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void {
        this._element = element;
        this._elementDimensions = elementDimensions;
        this._startCoords = { x: event.clientX, y: event.clientY }
    }
    ReCalculate(event: any): void {
        event.preventDefault();
        const updatedData = this.CalculateDelta(event);
        if (updatedData !== null) {
            this._element.style.left = `${updatedData.left}px`;
            this._element.style.top = `${updatedData.top}px`;
            this._element.style.width = `${updatedData.width}px`;
            this._element.style.height = `${updatedData.height}px`;
        }
    }
    EndOperation(event: any): Dimensions {
        const updatedData = this.CalculateDelta(event);
        return updatedData;
    }
    CancelOperation(): void {
        this._element.style.left = `${this._elementDimensions.left}px`;
        this._element.style.top = `${this._elementDimensions.top}px`;
        this._element.style.width = `${this._elementDimensions.width}px`;
        this._element.style.height = `${this._elementDimensions.height}px`;
    }
    get OperationType(): E_OperationType {
        throw new Error("Method not implemented.");
    }
}

export class DoodleResizeOperationMove extends DoodleResizeOperation {
    
    protected CalculateDelta(event: any): Dimensions {
        const dimensions: Dimensions = {
            height: this._elementDimensions.height,
            width: this._elementDimensions.width,
            left: (event.clientX - this._startCoords.x) + this._elementDimensions.left,
            top: (event.clientY - this._startCoords.y) + this._elementDimensions.top
        };

        return dimensions;
    }
    get OperationType() { return E_OperationType.Move };
}

export class DoodleResizeOperationResizeBR extends DoodleResizeOperation {
   
    protected CalculateDelta(event: any): Dimensions {
        const dimensions: Dimensions = {
            height: Math.max((event.clientY - this._startCoords.y) + this._elementDimensions.height, this._elementDimensions.minHeight || 0),
            width: Math.max((event.clientX - this._startCoords.x) + this._elementDimensions.width, this._elementDimensions.minWidth || 0),
            left: this._elementDimensions.left,
            top: this._elementDimensions.top
        };

        return dimensions;
    }

    get OperationType() { return E_OperationType.ResizeBR };
}

export class DoodleResizeOperationResizeTR extends DoodleResizeOperation {

    protected CalculateDelta(event: any): Dimensions {

        const heightDelta = Math.max((this._startCoords.y - event.clientY) + this._elementDimensions.height, this._elementDimensions.minHeight || 0);
        const dimensions: Dimensions = {
            height: heightDelta,
            width: Math.max((event.clientX - this._startCoords.x) + this._elementDimensions.width, this._elementDimensions.minWidth || 0),
            left: this._elementDimensions.left,
            top: this._elementDimensions.top - (heightDelta - this._elementDimensions.height)
        };

        return dimensions;
    }

    get OperationType() { return E_OperationType.ResizeTR };
}

export class DoodleResizeOperationResizeBL extends DoodleResizeOperation {
   
    protected CalculateDelta(event: any): Dimensions {

        const widthDelta = Math.max((this._startCoords.x - event.clientX) + this._elementDimensions.width, this._elementDimensions.minWidth || 0);
        const dimensions: Dimensions = {
            height: Math.max((event.clientY - this._startCoords.y) + this._elementDimensions.height, this._elementDimensions.minHeight || 0),
            width: widthDelta,
            left: this._elementDimensions.left - (widthDelta - this._elementDimensions.width),
            top: this._elementDimensions.top
        };
        return dimensions;
    }

    get OperationType() { return E_OperationType.ResizeBL };
}

export class DoodleResizeOperationResizeTL extends DoodleResizeOperation {
   
    protected CalculateDelta(event: any): Dimensions {
        const heightDelta = Math.max((this._startCoords.y - event.clientY) + this._elementDimensions.height, this._elementDimensions.minHeight || 0);
        const widthDelta = Math.max((this._startCoords.x - event.clientX) + this._elementDimensions.width, this._elementDimensions.minWidth || 0);
        const dimensions: Dimensions = {
            height: heightDelta,
            width: widthDelta,
            left: this._elementDimensions.left - (widthDelta - this._elementDimensions.width),
            top: this._elementDimensions.top - (heightDelta - this._elementDimensions.height)
        };
        return dimensions;
    }

    get OperationType() { return E_OperationType.ResizeTL };
}

export class DoodleResize {
    private _resizeElement: HTMLElement;
    private _currentDimensions: Dimensions;
    private _elementActivated: boolean;
    private _callbackRef: any;
    private _elementId: string;

    private _rsAdornerTL: HTMLElement;
    private _rsAdornerBL: HTMLElement;
    private _rsAdornerTR: HTMLElement;
    private _rsAdornerBR: HTMLElement;

    private _canResize: boolean = true;
    private _canMove: boolean = true;
    private _currentOperation: IDoodleResizeOperation = null;

    public get ElementId(): string { return this._elementId; }

    constructor(resizeElement: HTMLElement, elementId: string, callbackRef: any, initDimensions: Dimensions, 
                elementActive: boolean = false, allowResize: boolean = true, allowMove: boolean = true) {
        this._resizeElement = resizeElement;
        this._currentDimensions = initDimensions;
        this._elementActivated = elementActive;
        this._callbackRef = callbackRef;
        this._canResize = allowResize;
        this._canMove = allowMove;
        this._elementId = elementId;

        this.SetupAdornerElements();
    }

    public SetAllowResize(value: boolean): void {
        this._canResize = value;
    }

    public SetAllowMove(value: boolean): void {
        this._canMove = value;
    }

    public ActivateElement(): void {
        this._elementActivated = true;
        this.UpdateResizeElementClass();
        this.AttachHandlers();
    }

    public DeActivateElement(): void {
        this._elementActivated = false;
        this.UpdateResizeElementClass();
        this.RemoveHandlers();
    }

    private UpdateResizeElementClass() {
        if (this._elementActivated) {
            this._resizeElement.classList.add("resizeActive");
        } else {
            this._resizeElement.classList.remove("resizeActive");
        }
    }

    private StartMove(event: any): void {
        if (this._canMove === true && this._currentOperation === null) {
            this._currentOperation = new DoodleResizeOperationMove();
            this._currentOperation.StartOperation(this._resizeElement, event, this._currentDimensions);
        }
    }

    private StartResizeBR(event: any): void {
        if (this._canResize === true && this._currentOperation === null) {
            this._currentOperation = new DoodleResizeOperationResizeBR();
            this._currentOperation.StartOperation(this._resizeElement, event, this._currentDimensions);
        }
    }

    private StartResizeTR(event: any): void {
        if (this._canResize === true && this._currentOperation === null) {
            this._currentOperation = new DoodleResizeOperationResizeTR();
            this._currentOperation.StartOperation(this._resizeElement, event, this._currentDimensions);
        }
    }

    private StartResizeBL(event: any): void {
        if (this._canResize === true && this._currentOperation === null) {
            this._currentOperation = new DoodleResizeOperationResizeBL();
            this._currentOperation.StartOperation(this._resizeElement, event, this._currentDimensions);
        }
    }

    private StartResizeTL(event: any): void {
        if (this._canResize === true && this._currentOperation === null) {
            this._currentOperation = new DoodleResizeOperationResizeTL();
            this._currentOperation.StartOperation(this._resizeElement, event, this._currentDimensions);
        }
    }

    private AttachHandlers(): void {


        this._resizeElement.addEventListener('touchstart', (e) => { this.StartMove(e.touches[0]); }, false);
        this._resizeElement.addEventListener('mousedown', (e) => { this.StartMove(e); }, false);

        this._rsAdornerBR.addEventListener('touchstart', (e) => { this.StartResizeBR(e.touches[0]); }, false);
        this._rsAdornerBR.addEventListener('mousedown', (e) => { this.StartResizeBR(e); }, false);

        this._rsAdornerTR.addEventListener('touchstart', (e) => { this.StartResizeTR(e.touches[0]); }, false);
        this._rsAdornerTR.addEventListener('mousedown', (e) => { this.StartResizeTR(e); }, false);

        this._rsAdornerBL.addEventListener('touchstart', (e) => { this.StartResizeBL(e.touches[0]); }, false);
        this._rsAdornerBL.addEventListener('mousedown', (e) => { this.StartResizeBL(e); }, false);

        this._rsAdornerTL.addEventListener('touchstart', (e) => { this.StartResizeTL(e.touches[0]); }, false);
        this._rsAdornerTL.addEventListener('mousedown', (e) => { this.StartResizeTL(e); }, false);
     
        document.addEventListener('touchmove', (e) => { this.MouseMove(e.touches[0]); e.preventDefault(); }, false);
        document.addEventListener('mousemove', (e) => { this.MouseMove(e); }, false);
        document.addEventListener('mouseup', (e) => { this.EndAll(e); }, false);
        document.addEventListener('touchend', (e) => { this.EndAll(e.touches[0]); }, false);
    }

    private RemoveHandlers(): void {

        this._resizeElement.removeEventListener('touchstart', (e) => { this.StartMove(e.touches[0]); });
        this._resizeElement.removeEventListener('mousedown', (e) => { this.StartMove(e); });
        
        this._rsAdornerBR.removeEventListener('touchstart', (e) => { this.StartResizeBR(e.touches[0]); });
        this._rsAdornerBR.removeEventListener('mousedown', (e) => { this.StartResizeBR(e); });
        
        this._rsAdornerTR.removeEventListener('touchstart', (e) => { this.StartResizeTR(e.touches[0]); });
        this._rsAdornerTR.removeEventListener('mousedown', (e) => { this.StartResizeTR(e); });
        
        this._rsAdornerBL.removeEventListener('touchstart', (e) => { this.StartResizeBL(e.touches[0]); });
        this._rsAdornerBL.removeEventListener('mousedown', (e) => { this.StartResizeBL(e); });

        this._rsAdornerTL.removeEventListener('touchstart', (e) => { this.StartResizeTL(e.touches[0]); });
        this._rsAdornerTL.removeEventListener('mousedown', (e) => { this.StartResizeTL(e); });

        document.removeEventListener('touchmove', (e) => { this.MouseMove(e.touches[0]); e.preventDefault(); });
        document.removeEventListener('mousemove', (e) => { this.MouseMove(e); });
        document.removeEventListener('mouseup', (e) => { this.EndAll(e); });
        document.removeEventListener('touchend', (e) => { this.EndAll(e.touches[0]); });

        this.EndAll(null);
    }

    private MouseMove(event: any): void {
        if (this._currentOperation !== null) {
            this._currentOperation.ReCalculate(event);
        }
    }

    private EndAll(event: any): void {
        if (this._currentOperation !== null && !!event) {
            this._currentDimensions = this._currentOperation.EndOperation(event);

            if (this._currentOperation.OperationType === E_OperationType.Move) {
                this.NotifyBlazorPositionChanged();
            } else {
                this.NotifyBlazorSizeChanged();
            }
            this.NotifyBlazorElementUpdated();
            this._currentOperation = null;
        }
    }

    private SetupAdornerElements(): void {
        this._rsAdornerTL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tl]');
        this._rsAdornerBL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-bl]');
        this._rsAdornerTR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tr]');
        this._rsAdornerBR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-br]');
    }

    private NotifyBlazorPositionChanged(): void {
        if (!!this._callbackRef) {
          this._callbackRef.invokeMethodAsync("ElementMoved", this._resizeElement, this._currentDimensions);
        }
    }

    private NotifyBlazorSizeChanged(): void {
        if (!!this._callbackRef) {
          this._callbackRef.invokeMethodAsync("ElementResized", this._resizeElement, this._currentDimensions);
        }
    }

    private NotifyBlazorElementUpdated(): void {
        if (!!this._callbackRef) {
          this._callbackRef.invokeMethodAsync("ElementUpdated", this._resizeElement, this._currentDimensions);
        }
    }
}

export let _doodleResizeComponents: DoodleResize[] = new Array();
export function GetDoodleResize(id: string): DoodleResize {
    const ix = _doodleResizeComponents.findIndex(d => d.ElementId === id);
    if (ix >= 0) {
        return _doodleResizeComponents[ix];
    }
    return null;
}

export function InitialiseResizable(resizeElement: HTMLElement, resizeElementId: string, callbackRef: any, initDimensions: string, 
                                    elementActive: boolean, allowResize: boolean = true, allowMove: boolean = true): void { 

    let dimensions: Dimensions = { height: 20, width: 100, left: 0, top: 0, minHeight: 0, minWidth: 0 };   
    if (!!initDimensions && initDimensions != '') {
        dimensions = JSON.parse(initDimensions);
    }
    const resizeInstance = new DoodleResize(resizeElement, resizeElementId, callbackRef, dimensions, elementActive, allowResize, allowMove); 
    _doodleResizeComponents.push(resizeInstance)
}
export function ActivateElement(resizeElementId: string): void { GetDoodleResize(resizeElementId).ActivateElement(); }
export function DeActivateElement(resizeElementId: string): void { GetDoodleResize(resizeElementId).DeActivateElement(); }
export function SetAllowResize(resizeElementId: string, value: boolean): void { GetDoodleResize(resizeElementId).SetAllowResize(value); }
export function SetAllowMove(resizeElementId: string, value: boolean): void { GetDoodleResize(resizeElementId).SetAllowMove(value); }