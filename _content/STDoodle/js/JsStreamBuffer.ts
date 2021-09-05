// This class should rather return an int array instead of a string
// but due to some difficulty this has been skipped for now
export class JsStreamBuffer {
     
    private _jsBufferName: string;
    private _readLength: number = 5 * 1024;
    private _lsValue: string = '';
    private _buffer: ArrayBuffer;
 
    get JsBufferName(): string { return this._jsBufferName }

    public constructor(jsBufferName: string) {
        this._jsBufferName = jsBufferName;
    }

    private RefreshBuffer(): void {
        this._lsValue = '';
        this._buffer = new ArrayBuffer(0);
    }

    private GetBufferData(): string {
        if (this._lsValue == null || this._lsValue == '') {
            // @ts-ignore
            this._lsValue = ReadLocalStorageItem(this._jsBufferName);
            if (this._lsValue !== '') {
                this.BuildBufferArray();
            }
        }
        return (this._lsValue || '');
    }

    private BuildBufferArray(): void {
        if (this._buffer.byteLength === 0) {
            if (this._lsValue == '') {
                this._buffer = new ArrayBuffer(0);
            } else {
                const arrayData = Uint8Array.from(atob(this._lsValue), c => c.charCodeAt(0));
                this._buffer = arrayData.buffer;
            }
        }
    }

    public BufferHasData(): boolean {
        try {
            this.RefreshBuffer();
            return (this.GetBufferData() != '');
        } catch (ex) {
            console.log(`JsStreamBuffer:BufferHasData. Error ${ex}`);
        }
        return false;
    }

    public BufferLength(): number {
        try {
            this.RefreshBuffer();
            return this.GetBufferData().length;
        } catch (ex) {
            console.log(`JsStreamBuffer:BufferLength. Error ${ex}`);
            return 0;
        }
    }

    public ClearBuffer(): void {
        try {
            this._lsValue = null;
            // @ts-ignore
            RemoveLocalStorageItem(this._jsBufferName);
        } catch (ex) {
            console.log(`JsStreamBuffer:ClearBuffer. Error ${ex}`);
        }
    }

    public ReadBuffer(fromIndex: number): string {
        try {
            const bufferData = this.GetBufferData();
            if (this._buffer.byteLength === 0) {
                return '';
            }
            const totalLeft = Math.min(this._readLength, this._buffer.byteLength - fromIndex);
            const result = new Uint8Array(this._buffer, fromIndex, totalLeft);
            return result.toString();
        } catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer. Error ${ex}`);
            return null;
        }
    }
   
}