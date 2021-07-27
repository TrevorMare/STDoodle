export class JsStreamBuffer {
    constructor(jsBufferName, base64Buffer) {
        this._jsBufferName = jsBufferName;
        this.CreateArrayBuffer(base64Buffer);
    }
    get JsBufferName() { return this._jsBufferName; }
    ;
    ReadBuffer(fromIndex) {
        const segmentLength = 5 * 1024;
        const totalLeft = Math.min(segmentLength, this._buffer.byteLength - fromIndex);
        const result = new Uint8Array(this._buffer, fromIndex, totalLeft);
        return result;
    }
    CreateArrayBuffer(base64Buffer) {
        this._buffer = decode(base64Buffer);
    }
    BufferLength() {
        return this._buffer.byteLength;
    }
}
