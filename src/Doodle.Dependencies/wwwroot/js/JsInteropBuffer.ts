// @ts-ignore
export let _jsStreamManager: JsStreamManager;

export function InitStreamManager(): void {
    if (!_jsStreamManager) {
        // @ts-ignore
        _jsStreamManager = new JsStreamManager();
        console.log(`Stream manager has been initialised.`)
    }
}

export function LoadBuffer(bufferId: string): void {
    InitStreamManager();
    _jsStreamManager.LoadBuffer(bufferId)
}

export function ClearBuffer(bufferId: string): void {
    InitStreamManager();
    _jsStreamManager.RemoveBuffer(bufferId)
}

export function ReadBuffer(bufferId: string, fromIndex: number): string {
    InitStreamManager();
    return _jsStreamManager.ReadBuffer(bufferId, fromIndex)
} 

export function BufferHasData(bufferId: string): boolean {
    InitStreamManager();
    return _jsStreamManager.BufferHasData(bufferId);
}

export function BufferExists(bufferId: string): boolean {
    InitStreamManager();
    return _jsStreamManager.BufferExists(bufferId);
}

export function BufferLength(bufferId: string): number {
    InitStreamManager();
    return _jsStreamManager.BufferLength(bufferId);
}