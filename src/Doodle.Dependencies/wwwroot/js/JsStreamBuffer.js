export class JsStreamBuffer {
    constructor(jsBufferName, base64Buffer) {
        this._jsBufferName = jsBufferName;
        this._base64Data = base64Buffer;
        this.CreateArrayBuffer(base64Buffer);
    }
    get JsBufferName() { return this._jsBufferName; }
    ;
    ReadBufferBase64(fromIndex) {
        try {
            const segmentLength = 5 * 1024;
            const totalLeft = Math.min(segmentLength, this._base64Data.length - fromIndex);
            const result = this._base64Data.substr(fromIndex, totalLeft);
            return result;
        }
        catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer - Error reading buffer. ${ex}`);
            return null;
        }
    }
    ReadBuffer(fromIndex) {
        try {
            const segmentLength = 5 * 1024;
            const totalLeft = Math.min(segmentLength, this._buffer.byteLength - fromIndex);
            const result = new Uint8Array(this._buffer, fromIndex, totalLeft);
            return result.toString();
        }
        catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer - Error reading buffer. ${ex}`);
            return null;
        }
    }
    CreateArrayBuffer(base64Buffer) {
        try {
            const arrayData = Uint8Array.from(atob(base64Buffer), c => c.charCodeAt(0));
            this._buffer = arrayData.buffer;
        }
        catch (ex) {
            console.log(`JsStreamBuffer:CreateArrayBuffer: Error creating ArrayBuffer Object: ${ex}`);
        }
    }
    BufferLength() {
        try {
            return this._buffer.byteLength;
        }
        catch (ex) {
            console.log(`JsStreamBuffer:BufferLength: Error reading ArrayBuffer length: ${ex}`);
            return 0;
        }
    }
}
