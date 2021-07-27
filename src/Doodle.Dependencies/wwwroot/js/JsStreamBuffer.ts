
export class JsStreamBuffer {
     
    private _jsBufferName: string;
    private _buffer: ArrayBuffer;

    get JsBufferName(): string { return this._jsBufferName };

    public constructor(jsBufferName: string, base64Buffer: string) {
        this._jsBufferName = jsBufferName;
        this.CreateArrayBuffer(base64Buffer);
    }

    public ReadBuffer(fromIndex: number): Uint8Array {
        const segmentLength: number = 5 * 1024;
        const totalLeft = Math.min(segmentLength, this._buffer.byteLength - fromIndex);
        const result = new Uint8Array(this._buffer, fromIndex, totalLeft);

        return result;
    }

    private CreateArrayBuffer(base64Buffer: string): void {
        // @ts-ignore
        this._buffer = decode(base64Buffer);
    }

    public BufferLength(): number {
        return this._buffer.byteLength;
    }
}