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
    StartOperation(element: HTMLElement, event: any, minWidth?: number, minHeight?: number): void,
    ReCalculate(event: any): void,
    EndOperation(event: any): Dimensions,
    CancelOperation(): void,
    get OperationType(): E_OperationType
}

export class DoodleResizeOperation implements IDoodleResizeOperation {
    protected _element: HTMLElement;
    protected _elementDimensions: Dimensions;
    protected _startCoords: Coords;
    protected _minWidth?: number;
    protected _minHeight: number;

    protected CalculateDelta(event: any): Dimensions {
        throw new Error("Method not implemented.");
    }

    private GetElementDimensions(): Dimensions {
        const result: Dimensions = {
            top: Number(this._element.style.top.replace('px', '')),
            left: Number(this._element.style.left.replace('px', '')),
            width: Number(this._element.style.width.replace('px', '')),
            height: Number(this._element.style.height.replace('px', '')),
            minWidth: this._minWidth,
            minHeight: this._minHeight
        }
        return result;
    }

    StartOperation(element: HTMLElement, event: any, minWidth?: number, minHeight?: number): void {
        this._element = element;
        this._minHeight = minHeight;
        this._minWidth = minWidth;
        this._elementDimensions = this.GetElementDimensions();
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
    private _elementActivated: boolean;
    private _callbackRef: any;
    private _elementId: string;

    private _rsAdornerTL: HTMLElement;
    private _rsAdornerBL: HTMLElement;
    private _rsAdornerTR: HTMLElement;
    private _rsAdornerBR: HTMLElement;
    private _mvAdorner: HTMLElement;

    private _resizeElementDownRef: any;
    private _resizeElementUpRef: any;
    private _resizeElementClickRef: any;
    private _documentDownRef: any;
    private _documentUpRef: any;
    private _documentMoveRef: any;
    
    private _adornerDownTLRef: any;
    private _adornerDownTRRef: any;
    private _adornerDownBLRef: any;
    private _adornerDownBRRef: any;
    private _adornerDownMoveRef: any;

    private _canResize: boolean = true;
    private _canMove: boolean = true;
    private _currentOperation: IDoodleResizeOperation = null;
    private _autoHandleEvents: boolean = false;

    private _minWidth?: number;
    private _minHeight?: number;

    public get ElementId(): string { return this._elementId; }

    constructor(resizeElement: HTMLElement, elementId: string, callbackRef: any, 
                autoHandleEvents: boolean, elementActive: boolean = false, allowResize: boolean = true, allowMove: boolean = true,
                minWidth?: number, minHeight?: number) {
        this._resizeElement = resizeElement;
        
        this._elementActivated = elementActive;
        this._callbackRef = callbackRef;
        this._canResize = allowResize;
        this._canMove = allowMove;
        this._elementId = elementId;
        this._minHeight = minHeight;
        this._minWidth = minWidth;
        this._autoHandleEvents = autoHandleEvents;
        this.SetupAdornerElements();
        this.AttachEventHandlers();
    }

    public SetAllowResize(value: boolean): void {
        this._canResize = value;
    }

    public SetAllowMove(value: boolean): void {
        this._canMove = value;
    }

    public SetMinWidth(value?: number) {
        this._minWidth = value;
    }

    public SetMinHeight(value?: number) {
        this._minHeight = value;
    }

    public SetAutoHandleEvents(value: boolean) {
        this._autoHandleEvents = value;
    }

    public SetElementIsActive(value: boolean) {
        console.log(`Blazor=> Setting Element Active ${value} `);
        this._elementActivated = value;
    }

    private StartMove(e: any) {
        if (this._elementActivated === true && this._canMove && this._currentOperation === null) {
            
            const event = this.GetInternalEvent(e);
            this._currentOperation = new DoodleResizeOperationMove();
            this._currentOperation.StartOperation(this._resizeElement, event, this._minWidth, this._minHeight);
        } 
    }

    private StartResizeBR(e: any): void {
        if (this._canResize === true && this._currentOperation === null && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            this._currentOperation = new DoodleResizeOperationResizeBR();
            this._currentOperation.StartOperation(this._resizeElement, event, this._minWidth, this._minHeight);
        }
    }

    private StartResizeTR(e: any): void {
        if (this._canResize === true && this._currentOperation === null && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            this._currentOperation = new DoodleResizeOperationResizeTR();
            this._currentOperation.StartOperation(this._resizeElement, event, this._minWidth, this._minHeight);
        }
    }

    private StartResizeBL(e: any): void {
        if (this._canResize === true && this._currentOperation === null && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            this._currentOperation = new DoodleResizeOperationResizeBL();
            this._currentOperation.StartOperation(this._resizeElement, event, this._minWidth, this._minHeight);
        }
    }

    private StartResizeTL(e: any): void {
        if (this._canResize === true && this._currentOperation === null && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            this._currentOperation = new DoodleResizeOperationResizeTL();
            this._currentOperation.StartOperation(this._resizeElement, event, this._minWidth, this._minHeight);
        }
    }

    private SetupAdornerElements(): void {
        this._rsAdornerTL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tl]');
        this._rsAdornerBL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-bl]');
        this._rsAdornerTR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tr]');
        this._rsAdornerBR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-br]');
        this._mvAdorner = this._resizeElement.querySelector('[data-doodle-move-adorner]');
    }

    private NotifyBlazorPositionChanged(operationResult: Dimensions): void {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementMoved", JSON.stringify(operationResult));
        }
    }

    private NotifyBlazorSizeChanged(operationResult: Dimensions): void {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementResized", JSON.stringify(operationResult));
        }
    }

    private NotifyBlazorElementUpdated(operationResult: Dimensions): void {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementUpdated", JSON.stringify(operationResult));
        }
    }

    private NotifyBlazorSetIsActive(value: boolean): void {
        if (!!this._callbackRef) {
            if (this._elementActivated !== value) {
                console.log(`JS => SetIsActive ${value}`);
                this._callbackRef.invokeMethodAsync("SetIsActivate", value);
            }
        }
    }

    private AttachEventHandlers(): void {
        console.log(`Attaching event handlers`);

        this._documentMoveRef = this.DocumentMoveEvent.bind(this); 
        this._documentUpRef = this.DocumentUpEvent.bind(this);
        this._documentDownRef = this.DocumentDownEvent.bind(this);
        this._resizeElementDownRef = this.ResizeDownEvent.bind(this);
        this._resizeElementUpRef = this.ResizeUpEvent.bind(this);
        this._adornerDownTLRef = this.StartResizeTL.bind(this);
        this._adornerDownTRRef = this.StartResizeTR.bind(this);
        this._adornerDownBLRef = this.StartResizeBL.bind(this);
        this._adornerDownBRRef = this.StartResizeBR.bind(this);
        this._adornerDownMoveRef = this.StartMove.bind(this);
        this._resizeElementClickRef = this.ResizeClickEvent.bind(this);
        

        document.addEventListener('touchmove', this._documentMoveRef, false);
        document.addEventListener('mousemove', this._documentMoveRef, false);

        document.addEventListener('mousedown', this._documentDownRef, false);
        document.addEventListener('touchstart', this._documentDownRef, false);

        document.addEventListener('mouseup', this._documentUpRef, false);
        document.addEventListener('touchend', this._documentUpRef, false);

        this._resizeElement.addEventListener('click', this._resizeElementClickRef, false);

        this._resizeElement.addEventListener('touchstart', this._resizeElementDownRef, false);
        this._resizeElement.addEventListener('mousedown', this._resizeElementDownRef, false);
        
        this._resizeElement.addEventListener('touchend', this._resizeElementUpRef, false);
        this._resizeElement.addEventListener('mouseup', this._resizeElementUpRef, false);

        this._rsAdornerBR.addEventListener('touchstart', this._adornerDownBRRef, false);
        this._rsAdornerBR.addEventListener('mousedown', this._adornerDownBRRef, false);

        this._rsAdornerTR.addEventListener('touchstart', this._adornerDownTRRef, false);
        this._rsAdornerTR.addEventListener('mousedown', this._adornerDownTRRef, false);

        this._rsAdornerBL.addEventListener('touchstart', this._adornerDownBLRef, false);
        this._rsAdornerBL.addEventListener('mousedown', this._adornerDownBLRef, false);

        this._rsAdornerTL.addEventListener('touchstart', this._adornerDownTLRef, false);
        this._rsAdornerTL.addEventListener('mousedown', this._adornerDownTLRef, false);

        this._mvAdorner.addEventListener('touchstart', this._adornerDownMoveRef, false);
        this._mvAdorner.addEventListener('mousedown', this._adornerDownMoveRef, false);
        
    }

    private DetachEventHandlers(): void {
        document.removeEventListener('touchmove', this._documentMoveRef);
        document.removeEventListener('mousemove', this._documentMoveRef);
        
        document.removeEventListener('mousedown', this._documentDownRef);
        document.removeEventListener('touchstart', this._documentDownRef);
        
        document.removeEventListener('mouseup', this._documentUpRef);
        document.removeEventListener('touchend', this._documentUpRef);

        this._resizeElement.removeEventListener('click', this._resizeElementClickRef);

        this._resizeElement.removeEventListener('touchstart', this._resizeElementDownRef);
        this._resizeElement.removeEventListener('mousedown', this._resizeElementDownRef);
        
        this._resizeElement.removeEventListener('touchend', this._resizeElementUpRef);
        this._resizeElement.removeEventListener('mouseup', this._resizeElementUpRef);

        this._rsAdornerBR.removeEventListener('touchstart', this._adornerDownBRRef);
        this._rsAdornerBR.removeEventListener('mousedown', this._adornerDownBRRef);

        this._rsAdornerTR.removeEventListener('touchstart', this._adornerDownTRRef);
        this._rsAdornerTR.removeEventListener('mousedown', this._adornerDownTRRef);

        this._rsAdornerBL.removeEventListener('touchstart', this._adornerDownBLRef);
        this._rsAdornerBL.removeEventListener('mousedown', this._adornerDownBLRef);

        this._rsAdornerTL.removeEventListener('touchstart', this._adornerDownTLRef);
        this._rsAdornerTL.removeEventListener('mousedown', this._adornerDownTLRef);

        this._mvAdorner.removeEventListener('touchstart', this._adornerDownMoveRef);
        this._mvAdorner.removeEventListener('mousedown', this._adornerDownMoveRef);

        this._documentMoveRef = null;
        this._documentDownRef = null;
        this._documentUpRef = null;
        this._resizeElementDownRef = null;
        this._resizeElementUpRef = null;

        this._adornerDownBRRef = null;
        this._adornerDownTRRef = null;
        this._adornerDownBLRef = null;
        this._adornerDownTLRef = null;
        this._adornerDownMoveRef = null;
    }

    private ResizeDownEvent(e: any) {
        if (this._currentOperation === null) {
            const event = this.GetInternalEvent(e);
            event.stopPropagation();
        }
    }

    private ResizeUpEvent(e: any) {
        if (this._currentOperation === null) {
            const event = this.GetInternalEvent(e);
            event.stopPropagation();
        }
    }

    private ResizeClickEvent(e: any) { 

        if (this._elementActivated === false && this._autoHandleEvents === true) {
            const event = this.GetInternalEvent(e);
            event.preventDefault();
            event.stopPropagation();
            this.NotifyBlazorSetIsActive(true);
        }
    }

    private DocumentDownEvent(e: any): void { }

    private DocumentUpEvent(e: any): void {
        const event = this.GetInternalEvent(e);
        
        if (this._elementActivated) {

            if (this._currentOperation !== null && !!e) {
                event.preventDefault();    
    
                const operationResult = this._currentOperation.EndOperation(event);
    
                if (this._currentOperation.OperationType === E_OperationType.Move) {
                    this.NotifyBlazorPositionChanged(operationResult);
                } else {
                    this.NotifyBlazorSizeChanged(operationResult);
                }

                this.NotifyBlazorElementUpdated(operationResult);
                this._currentOperation = null;

                console.log(`Document Element Up: Finalising Operation`);

            } else if (this._currentOperation === null && this._autoHandleEvents === true) {
                event.preventDefault();
                console.log(`Document Element Up: Setting active to false`);
                this.NotifyBlazorSetIsActive(false);
            }
        } else {
            if (this._autoHandleEvents) {
                event.preventDefault();

                this.NotifyBlazorSetIsActive(false);
            }
        }
    }

    private DocumentMoveEvent(e: any): void { 
        if (!!this._currentOperation && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            this._currentOperation.ReCalculate(event);
        }
    }

    private GetInternalEvent(e): any {
        if ('touches' in e) { return e.touches[0]; }
        return e;
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

export function InitialiseResizable(resizeElement: HTMLElement, resizeElementId: string, callbackRef: any, 
                                    autoHandleEvents: boolean, elementActive: boolean, allowResize: boolean = true, allowMove: boolean = true,
                                    minWidth: number = null, minHeight: number = null): void { 

    const resizeInstance = new DoodleResize(resizeElement, resizeElementId, callbackRef, autoHandleEvents, elementActive, allowResize, allowMove, minWidth, minHeight); 
    _doodleResizeComponents.push(resizeInstance)
}
export function ActivateElement(resizeElementId: string): void { GetDoodleResize(resizeElementId).SetElementIsActive(true); }
export function DeActivateElement(resizeElementId: string): void { GetDoodleResize(resizeElementId).SetElementIsActive(false); }
export function SetAllowResize(resizeElementId: string, value: boolean): void { GetDoodleResize(resizeElementId).SetAllowResize(value); }
export function SetAllowMove(resizeElementId: string, value: boolean): void { GetDoodleResize(resizeElementId).SetAllowMove(value); }
export function SetMinWidth(resizeElementId: string, value?: number): void { GetDoodleResize(resizeElementId).SetMinWidth(value); }
export function SetMinHeight(resizeElementId: string, value?: number): void { GetDoodleResize(resizeElementId).SetMinHeight(value); }
export function SetAutoHandleEvents(resizeElementId: string, value: boolean): void { GetDoodleResize(resizeElementId).SetAutoHandleEvents(value); }