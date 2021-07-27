// This class should rather return an int array instead of a string
// but due to some difficulty this has been skipped for now
export class JsStreamBuffer {
     
    private _jsBufferName: string;
    private _buffer: ArrayBuffer;
    private _base64Data: string;

    get JsBufferName(): string { return this._jsBufferName };

    public constructor(jsBufferName: string, base64Buffer: string) {
        this._jsBufferName = jsBufferName;
        this._base64Data = base64Buffer;
        this.CreateArrayBuffer(base64Buffer);
    }

    public ReadBufferBase64(fromIndex: number): string {
        try {
            const segmentLength: number = 5 * 1024;
            const totalLeft = Math.min(segmentLength, this._base64Data.length - fromIndex);
            const result = this._base64Data.substr(fromIndex, totalLeft);
            return result;
        } catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer - Error reading buffer. ${ex}`);
            return null;
        }
    }

    public ReadBuffer(fromIndex: number): string {
        try {
            const segmentLength: number = 5 * 1024;
            const totalLeft = Math.min(segmentLength, this._buffer.byteLength - fromIndex);
            const result = new Uint8Array(this._buffer, fromIndex, totalLeft);
            return result.toString();
        } catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer - Error reading buffer. ${ex}`);
            return null;
        }
    }

    private CreateArrayBuffer(base64Buffer: string): void {
        try {
            const arrayData = Uint8Array.from(atob(base64Buffer), c => c.charCodeAt(0));
            this._buffer = arrayData.buffer;
        } catch (ex) {
            console.log(`JsStreamBuffer:CreateArrayBuffer: Error creating ArrayBuffer Object: ${ex}`);
        }
    }

    public BufferLength(): number {
        try {
            return this._buffer.byteLength;
        } catch (ex) {
            console.log(`JsStreamBuffer:BufferLength: Error reading ArrayBuffer length: ${ex}`);
            return 0;
        }
        
    }
}