import './vendor.min.js';
import { JsStreamManager } from './JsStreamManager';
export const _jsStreamManager = new JsStreamManager();
export function RenderCanvasToImage(renderElement) {
    var bufferId = "88888";
    html2canvas(renderElement).then(canvas => {
        var renderedData = canvas.toDataURL("image/png");
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
export function BufferExists(id) {
    return _jsStreamManager.BufferExists(id);
}
export function BufferLength(id) {
    return _jsStreamManager.BufferLength(id);
}
