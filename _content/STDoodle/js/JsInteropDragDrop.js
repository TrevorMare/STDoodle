export var E_OperationType;
(function (E_OperationType) {
    E_OperationType[E_OperationType["Move"] = 1] = "Move";
    E_OperationType[E_OperationType["ResizeTL"] = 2] = "ResizeTL";
    E_OperationType[E_OperationType["ResizeTR"] = 3] = "ResizeTR";
    E_OperationType[E_OperationType["ResizeBL"] = 4] = "ResizeBL";
    E_OperationType[E_OperationType["ResizeBR"] = 5] = "ResizeBR";
})(E_OperationType || (E_OperationType = {}));
export class DoodleResizeOperation {
    CalculateDelta(event) {
        throw new Error("Method not implemented.");
    }
    GetElementDimensions() {
        const result = {
            top: Number(this._element.style.top.replace('px', '')),
            left: Number(this._element.style.left.replace('px', '')),
            width: Number(this._element.style.width.replace('px', '')),
            height: Number(this._element.style.height.replace('px', '')),
            minWidth: this._minWidth,
            minHeight: this._minHeight
        };
        return result;
    }
    StartOperation(element, event, minWidth, minHeight) {
        this._element = element;
        this._minHeight = minHeight;
        this._minWidth = minWidth;
        this._elementDimensions = this.GetElementDimensions();
        this._startCoords = { x: event.clientX, y: event.clientY };
    }
    ReCalculate(event) {
        if (!event)
            return;
        this._newDimensions = this.CalculateDelta(event);
        if (this._newDimensions !== null) {
            this._element.style.left = `${this._newDimensions.left}px`;
            this._element.style.top = `${this._newDimensions.top}px`;
            this._element.style.width = `${this._newDimensions.width}px`;
            this._element.style.height = `${this._newDimensions.height}px`;
        }
    }
    EndOperation(event) {
        if (!event)
            return this._newDimensions;
        const updatedData = this.CalculateDelta(event);
        return updatedData;
    }
    CancelOperation() {
        this._element.style.left = `${this._elementDimensions.left}px`;
        this._element.style.top = `${this._elementDimensions.top}px`;
        this._element.style.width = `${this._elementDimensions.width}px`;
        this._element.style.height = `${this._elementDimensions.height}px`;
    }
    get OperationType() {
        throw new Error("Method not implemented.");
    }
}
export class DoodleResizeOperationMove extends DoodleResizeOperation {
    CalculateDelta(event) {
        const dimensions = {
            height: this._elementDimensions.height,
            width: this._elementDimensions.width,
            left: (event.clientX - this._startCoords.x) + this._elementDimensions.left,
            top: (event.clientY - this._startCoords.y) + this._elementDimensions.top
        };
        return dimensions;
    }
    get OperationType() { return E_OperationType.Move; }
    ;
}
export class DoodleResizeOperationResizeBR extends DoodleResizeOperation {
    CalculateDelta(event) {
        const dimensions = {
            height: Math.max((event.clientY - this._startCoords.y) + this._elementDimensions.height, this._elementDimensions.minHeight || 0),
            width: Math.max((event.clientX - this._startCoords.x) + this._elementDimensions.width, this._elementDimensions.minWidth || 0),
            left: this._elementDimensions.left,
            top: this._elementDimensions.top
        };
        return dimensions;
    }
    get OperationType() { return E_OperationType.ResizeBR; }
    ;
}
export class DoodleResizeOperationResizeTR extends DoodleResizeOperation {
    CalculateDelta(event) {
        const heightDelta = Math.max((this._startCoords.y - event.clientY) + this._elementDimensions.height, this._elementDimensions.minHeight || 0);
        const dimensions = {
            height: heightDelta,
            width: Math.max((event.clientX - this._startCoords.x) + this._elementDimensions.width, this._elementDimensions.minWidth || 0),
            left: this._elementDimensions.left,
            top: this._elementDimensions.top - (heightDelta - this._elementDimensions.height)
        };
        return dimensions;
    }
    get OperationType() { return E_OperationType.ResizeTR; }
    ;
}
export class DoodleResizeOperationResizeBL extends DoodleResizeOperation {
    CalculateDelta(event) {
        const widthDelta = Math.max((this._startCoords.x - event.clientX) + this._elementDimensions.width, this._elementDimensions.minWidth || 0);
        const dimensions = {
            height: Math.max((event.clientY - this._startCoords.y) + this._elementDimensions.height, this._elementDimensions.minHeight || 0),
            width: widthDelta,
            left: this._elementDimensions.left - (widthDelta - this._elementDimensions.width),
            top: this._elementDimensions.top
        };
        return dimensions;
    }
    get OperationType() { return E_OperationType.ResizeBL; }
    ;
}
export class DoodleResizeOperationResizeTL extends DoodleResizeOperation {
    CalculateDelta(event) {
        const heightDelta = Math.max((this._startCoords.y - event.clientY) + this._elementDimensions.height, this._elementDimensions.minHeight || 0);
        const widthDelta = Math.max((this._startCoords.x - event.clientX) + this._elementDimensions.width, this._elementDimensions.minWidth || 0);
        const dimensions = {
            height: heightDelta,
            width: widthDelta,
            left: this._elementDimensions.left - (widthDelta - this._elementDimensions.width),
            top: this._elementDimensions.top - (heightDelta - this._elementDimensions.height)
        };
        return dimensions;
    }
    get OperationType() { return E_OperationType.ResizeTL; }
    ;
}
export class DoodleResize {
    constructor(resizeElement, elementId, callbackRef, autoHandleEvents, elementActive = false, allowResize = true, allowMove = true, minWidth, minHeight) {
        this._canResize = true;
        this._canMove = true;
        this._currentOperation = null;
        this._autoHandleEvents = false;
        this._resizeElement = resizeElement;
        this._elementActivated = elementActive;
        this._callbackRef = callbackRef;
        this._canResize = allowResize;
        this._canMove = allowMove;
        this._elementId = elementId;
        this._minHeight = minHeight;
        this._minWidth = minWidth;
        this._autoHandleEvents = autoHandleEvents;
        this._originalOverscrollBehaviour = document.body.style.overscrollBehavior;
        this.SetupAdornerElements();
        this.AttachEventHandlers();
    }
    get ElementId() { return this._elementId; }
    SetAllowResize(value) {
        this._canResize = value;
    }
    SetAllowMove(value) {
        this._canMove = value;
    }
    SetMinWidth(value) {
        this._minWidth = value;
    }
    SetMinHeight(value) {
        this._minHeight = value;
    }
    SetAutoHandleEvents(value) {
        this._autoHandleEvents = value;
    }
    SetElementIsActive(value) {
        this._elementActivated = value;
    }
    OnMoveAdornerDown(e) {
        if (this.AdornerDownCanResize(e)) {
            this._currentOperation = new DoodleResizeOperationMove();
            this._currentOperation.StartOperation(this._resizeElement, this.GetInternalEvent(e), this._minWidth, this._minHeight);
        }
    }
    OnResizeBRAdornerDown(e) {
        if (this.AdornerDownCanResize(e)) {
            this._currentOperation = new DoodleResizeOperationResizeBR();
            this._currentOperation.StartOperation(this._resizeElement, this.GetInternalEvent(e), this._minWidth, this._minHeight);
        }
    }
    OnResizeTRAdornerDown(e) {
        if (this.AdornerDownCanResize(e)) {
            this._currentOperation = new DoodleResizeOperationResizeTR();
            this._currentOperation.StartOperation(this._resizeElement, this.GetInternalEvent(e), this._minWidth, this._minHeight);
        }
    }
    OnResizeBLAdornerDown(e) {
        if (this.AdornerDownCanResize(e)) {
            this._currentOperation = new DoodleResizeOperationResizeBL();
            this._currentOperation.StartOperation(this._resizeElement, this.GetInternalEvent(e), this._minWidth, this._minHeight);
        }
    }
    OnResizeTLAdornerDown(e) {
        if (this.AdornerDownCanResize(e)) {
            this._currentOperation = new DoodleResizeOperationResizeTL();
            this._currentOperation.StartOperation(this._resizeElement, this.GetInternalEvent(e), this._minWidth, this._minHeight);
        }
    }
    AdornerDownCanResize(downEvent) {
        if (this._elementActivated === true && this._canMove && this._currentOperation === null) {
            downEvent.preventDefault();
            return true;
        }
        return false;
    }
    SetupAdornerElements() {
        this._rsAdornerTL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tl]');
        this._rsAdornerBL = this._resizeElement.querySelector('[data-doodle-resizable-adorner-bl]');
        this._rsAdornerTR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-tr]');
        this._rsAdornerBR = this._resizeElement.querySelector('[data-doodle-resizable-adorner-br]');
        this._mvAdorner = this._resizeElement.querySelector('[data-doodle-move-adorner]');
    }
    NotifyBlazorPositionChanged(operationResult) {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementMoved", JSON.stringify(operationResult));
        }
    }
    NotifyBlazorSizeChanged(operationResult) {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementResized", JSON.stringify(operationResult));
        }
    }
    NotifyBlazorElementUpdated(operationResult) {
        if (!!this._callbackRef) {
            this._callbackRef.invokeMethodAsync("ElementUpdated", JSON.stringify(operationResult));
        }
    }
    NotifyBlazorSetIsActive(value) {
        if (!!this._callbackRef) {
            if (this._elementActivated !== value) {
                this._callbackRef.invokeMethodAsync("SetIsActivate", value);
            }
        }
    }
    AttachEventHandlers() {
        this._documentMoveRef = this.DocumentMoveEvent.bind(this);
        this._documentUpRef = this.DocumentUpEvent.bind(this);
        this._documentDownRef = this.DocumentDownEvent.bind(this);
        this._resizeElementDownRef = this.ResizeDownEvent.bind(this);
        this._resizeElementUpRef = this.ResizeUpEvent.bind(this);
        this._resizeElementClickRef = this.ResizeClickEvent.bind(this);
        this._adornerDownTLRef = this.OnResizeTLAdornerDown.bind(this);
        this._adornerDownTRRef = this.OnResizeTRAdornerDown.bind(this);
        this._adornerDownBLRef = this.OnResizeBLAdornerDown.bind(this);
        this._adornerDownBRRef = this.OnResizeBRAdornerDown.bind(this);
        this._adornerDownMoveRef = this.OnMoveAdornerDown.bind(this);
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
    DetachEventHandlers() {
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
    ResizeDownEvent(e) {
        if (this._currentOperation === null) {
            const event = this.GetInternalEvent(e);
            e.stopPropagation();
        }
    }
    ResizeUpEvent(e) {
        if (this._currentOperation === null && !e) {
            const event = this.GetInternalEvent(e);
            e.stopPropagation();
        }
    }
    ResizeClickEvent(e) {
        if (this._elementActivated === false && this._autoHandleEvents === true) {
            const event = this.GetInternalEvent(e);
            e.preventDefault();
            e.stopPropagation();
            this.NotifyBlazorSetIsActive(true);
        }
    }
    DocumentDownEvent(e) {
        document.body.style.overscrollBehavior = "contain";
    }
    DocumentUpEvent(e) {
        const event = this.GetInternalEvent(e);
        if (this._elementActivated) {
            if (this._currentOperation !== null && !!e) {
                const operationResult = this._currentOperation.EndOperation(event);
                if (this._currentOperation.OperationType === E_OperationType.Move) {
                    this.NotifyBlazorPositionChanged(operationResult);
                }
                else {
                    this.NotifyBlazorSizeChanged(operationResult);
                }
                this.NotifyBlazorElementUpdated(operationResult);
                this._currentOperation = null;
                console.log(`Document Element Up: Finalising Operation`);
            }
            else if (this._currentOperation === null && this._autoHandleEvents === true) {
                console.log(`Document Element Up: Setting active to false`);
                this.NotifyBlazorSetIsActive(false);
            }
        }
        else {
            if (this._autoHandleEvents) {
                this.NotifyBlazorSetIsActive(false);
            }
        }
    }
    DocumentMoveEvent(e) {
        if (!!this._currentOperation && this._elementActivated) {
            const event = this.GetInternalEvent(e);
            if (!event) {
                event.preventDefault();
            }
            this._currentOperation.ReCalculate(event);
        }
    }
    GetInternalEvent(e) {
        if ('touches' in e) {
            return e.touches[0];
        }
        return e;
    }
}
export let _doodleResizeComponents = new Array();
export function GetDoodleResize(id) {
    const ix = _doodleResizeComponents.findIndex(d => d.ElementId === id);
    if (ix >= 0) {
        return _doodleResizeComponents[ix];
    }
    return null;
}
export function InitialiseResizable(resizeElement, resizeElementId, callbackRef, autoHandleEvents, elementActive, allowResize = true, allowMove = true, minWidth = null, minHeight = null) {
    const resizeInstance = new DoodleResize(resizeElement, resizeElementId, callbackRef, autoHandleEvents, elementActive, allowResize, allowMove, minWidth, minHeight);
    _doodleResizeComponents.push(resizeInstance);
}
export function ActivateElement(resizeElementId) { GetDoodleResize(resizeElementId).SetElementIsActive(true); }
export function DeActivateElement(resizeElementId) { GetDoodleResize(resizeElementId).SetElementIsActive(false); }
export function SetAllowResize(resizeElementId, value) { GetDoodleResize(resizeElementId).SetAllowResize(value); }
export function SetAllowMove(resizeElementId, value) { GetDoodleResize(resizeElementId).SetAllowMove(value); }
export function SetMinWidth(resizeElementId, value) { GetDoodleResize(resizeElementId).SetMinWidth(value); }
export function SetMinHeight(resizeElementId, value) { GetDoodleResize(resizeElementId).SetMinHeight(value); }
export function SetAutoHandleEvents(resizeElementId, value) { GetDoodleResize(resizeElementId).SetAutoHandleEvents(value); }
