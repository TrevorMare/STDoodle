import './vendor.min.js';
export let _jsStreamManager;
export function RenderCanvasToImage(renderElement) {
    if (_jsStreamManager == null) {
        console.log(`Creating new JS Stream Manager Object`);
        _jsStreamManager = new JsStreamManager();
    }
    const bufferId = Date.now().toString(18) + Math.random().toString(36).substring(2);
    ;
    html2canvas(renderElement, { allowTaint: true, useCORS: true }).then(canvas => {
        let renderedData = canvas.toDataURL("image/png");
        renderedData = renderedData.substring(renderedData.indexOf(",") + 1);
        _jsStreamManager.AddBuffer(bufferId, renderedData);
    });
    return bufferId;
}
export function ClearBufferedImage(bufferId) {
    _jsStreamManager.RemoveBuffer(bufferId);
}
export function ReadBufferedImage(bufferId, fromIndex) {
    return _jsStreamManager.ReadBuffer(bufferId, fromIndex);
}
export function ReadBufferedImageBase64(bufferId, fromIndex) {
    return _jsStreamManager.ReadBufferBase64(bufferId, fromIndex);
}
export function BufferExists(id) {
    return _jsStreamManager.BufferExists(id);
}
export function BufferLength(id) {
    return _jsStreamManager.BufferLength(id);
}
