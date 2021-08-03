export interface Dimensions {
    top: number,
    left: number,
    width: number,
    height: number,
    minWidth: number,
    minHeight: number
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

export interface DoodleResizeOperation {
    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void,
    ReCalculate(event: any): void,
    EndOperation(event: any): Dimensions,
    CancelOperation(): void,
    get OperationType(): E_OperationType
}

export class DoodleResizeOperationMove implements DoodleResizeOperation {
    private _element: HTMLElement;
    private _elementDimensions: Dimensions;
    private _startCoords: Coords;

    get OperationType() { return E_OperationType.Move };

    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void {
        this._element = element;
        this._elementDimensions = elementDimensions;
        this._startCoords = { x: event.clientX, y: event.clientY }
    }
    
    ReCalculate(event: any): void {
        event.preventDefault();
        const updatedPosition: Coords = {
            x: (event.clientX - this._startCoords.x) + this._elementDimensions.left,
            y: (event.clientY - this._startCoords.y) + this._elementDimensions.top
        }
        this._element.style.left = `${updatedPosition.x}px`;
        this._element.style.top = `${updatedPosition.y}px`;
    }
    
    EndOperation(event: any): Dimensions {
        const updatedPosition: Coords = {
            x: (event.clientX - this._startCoords.x) + this._elementDimensions.left,
            y: (event.clientY - this._startCoords.y) + this._elementDimensions.top
        }
        // ... operator not working
        const result: Dimensions = { 
            left: this._elementDimensions.left,
            top: this._elementDimensions.top,
            width: this._elementDimensions.width,
            height: this._elementDimensions.height,
            minHeight: this._elementDimensions.minHeight,
            minWidth: this._elementDimensions.minWidth
        };

        result.left = updatedPosition.x;
        result.top = updatedPosition.y;

        return result;
    }

    CancelOperation(): void {
        this._element.style.left = `${this._elementDimensions.left}px`;
        this._element.style.top = `${this._elementDimensions.top}px`;
    }
}

export class DoodleResizeOperationResizeBR implements DoodleResizeOperation {
    private _element: HTMLElement;
    private _elementDimensions: Dimensions;
    private _startCoords: Coords;

    get OperationType() { return E_OperationType.ResizeBR };

    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void {
        event.preventDefault();
        this._element = element;
        this._elementDimensions = elementDimensions;
        this._startCoords = { x: event.clientX, y: event.clientY }
    }
    
    ReCalculate(event: any): void {
        event.preventDefault();
        const updatedSize: Coords = {
            x: Math.max((event.clientX - this._startCoords.x), this._elementDimensions.minWidth || 0) + this._elementDimensions.width, 
            y: Math.max((event.clientY - this._startCoords.y), this._elementDimensions.minHeight || 0) + this._elementDimensions.height
        }
        this._element.style.width = `${updatedSize.x}px`;
        this._element.style.height = `${updatedSize.y}px`;
    }
    
    EndOperation(event: any): Dimensions {
        const updatedSize: Coords = {
            x: Math.max((event.clientX - this._startCoords.x), this._elementDimensions.minWidth || 0) + this._elementDimensions.width, 
            y: Math.max((event.clientY - this._startCoords.y), this._elementDimensions.minHeight || 0) + this._elementDimensions.height
        }
        // ... operator not working
        const result: Dimensions = { 
            left: this._elementDimensions.left,
            top: this._elementDimensions.top,
            width: this._elementDimensions.width,
            height: this._elementDimensions.height,
            minHeight: this._elementDimensions.minHeight,
            minWidth: this._elementDimensions.minWidth
        };

        result.width = updatedSize.x;
        result.height = updatedSize.y;
        return result;
    }

    CancelOperation(): void {
        this._element.style.height = `${this._elementDimensions.height}px`;
        this._element.style.width = `${this._elementDimensions.width}px`;
    }
}

export class DoodleResizeOperationResizeTR implements DoodleResizeOperation {
    private _element: HTMLElement;
    private _elementDimensions: Dimensions;
    private _startCoords: Coords;

    get OperationType() { return E_OperationType.ResizeTR };

    StartOperation(element: HTMLElement, event: any, elementDimensions: Dimensions): void {
        event.preventDefault();
        this._element = element;
        this._elementDimensions = elementDimensions;
        this._startCoords = { x: event.clientX, y: event.clientY }
    }
    
    ReCalculate(event: any): void {
        event.preventDefault();
        
        const updatedPosition: Coords = {
            x: this._elementDimensions.left,
            y: (event.clientY - this._startCoords.y) + this._elementDimensions.top
        }
        
        const updatedSize: Coords = {
            x: Math.max((event.clientX - this._startCoords.x), this._elementDimensions.minWidth || 0) + this._elementDimensions.width, 
            y: Math.max((event.clientY - this._startCoords.y), this._elementDimensions.minHeight || 0) + this._elementDimensions.height
        }

        this._element.style.top = `${updatedPosition.x}px`;
        this._element.style.height = `${updatedSize.y}px`;
        this._element.style.width = `${updatedSize.x}px`;
    }
    
    EndOperation(event: any): Dimensions {
        const updatedSize: Coords = {
            x: Math.max((event.clientX - this._startCoords.x), this._elementDimensions.minWidth || 0) + this._elementDimensions.width, 
            y: Math.max((event.clientY - this._startCoords.y), this._elementDimensions.minHeight || 0) + this._elementDimensions.height
        }
        // ... operator not working
        const result: Dimensions = { 
            left: this._elementDimensions.left,
            top: this._elementDimensions.top,
            width: this._elementDimensions.width,
            height: this._elementDimensions.height,
            minHeight: this._elementDimensions.minHeight,
            minWidth: this._elementDimensions.minWidth
        };

        result.width = updatedSize.x;
        result.height = updatedSize.y;
        return result;
    }

    CancelOperation(): void {
        this._element.style.top = `${this._elementDimensions.top}px`;
        this._element.style.height = `${this._elementDimensions.height}px`;
        this._element.style.width = `${this._elementDimensions.width}px`;
    }
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
    private _currentOperation: DoodleResizeOperation = null;

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

    private AttachHandlers(): void {


        this._resizeElement.addEventListener('touchstart', (e) => { this.StartMove(e.touches[0]); }, false);
        this._resizeElement.addEventListener('mousedown', (e) => { this.StartMove(e); }, false);

        this._rsAdornerBR.addEventListener('touchstart', (e) => { this.StartResizeBR(e.touches[0]); }, false);
        this._rsAdornerBR.addEventListener('mousedown', (e) => { this.StartResizeBR(e); }, false);
     
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
        let shouldNotifyOfChanges: boolean = false;
        
        if (this._currentOperation !== null && !!event) {
            this._currentDimensions = this._currentOperation.EndOperation(event);

            if (this._currentOperation.OperationType === E_OperationType.Move) {
                //this.NotifyBlazorPositionChanged();
            } else {
                //this.NotifyBlazorSizeChanged();
            }

            //this.NotifyBlazorElementUpdated();

            this._currentOperation = null;
        }
    }

    private SetupAdornerElements(): void {
        this._rsAdornerTL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tl]');
        this._rsAdornerBL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-bl]');
        this._rsAdornerTR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tr]');
        this._rsAdornerBR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-br]');

        console.log(`Bottom right adorner: ${this._rsAdornerBR}`);

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