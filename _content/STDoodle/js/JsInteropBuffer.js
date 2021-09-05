export let _jsStreamManager;
export function InitStreamManager() {
    if (!_jsStreamManager) {
        _jsStreamManager = new JsStreamManager();
        console.log(`Stream manager has been initialised.`);
    }
}
export function LoadBuffer(bufferId) {
    InitStreamManager();
    _jsStreamManager.LoadBuffer(bufferId);
}
export function ClearBuffer(bufferId) {
    InitStreamManager();
    _jsStreamManager.RemoveBuffer(bufferId);
}
export function ReadBuffer(bufferId, fromIndex) {
    InitStreamManager();
    return _jsStreamManager.ReadBuffer(bufferId, fromIndex);
}
export function BufferHasData(bufferId) {
    InitStreamManager();
    return _jsStreamManager.BufferHasData(bufferId);
}
export function BufferExists(bufferId) {
    InitStreamManager();
    return _jsStreamManager.BufferExists(bufferId);
}
export function BufferLength(bufferId) {
    InitStreamManager();
    return _jsStreamManager.BufferLength(bufferId);
}
