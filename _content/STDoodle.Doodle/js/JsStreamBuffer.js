export class JsStreamBuffer {
    constructor(jsBufferName) {
        this._readLength = 5 * 1024;
        this._lsValue = '';
        this._jsBufferName = jsBufferName;
    }
    get JsBufferName() { return this._jsBufferName; }
    RefreshBuffer() {
        this._lsValue = '';
        this._buffer = new ArrayBuffer(0);
    }
    GetBufferData() {
        if (this._lsValue == null || this._lsValue == '') {
            this._lsValue = ReadLocalStorageItem(this._jsBufferName);
            if (this._lsValue !== '') {
                this.BuildBufferArray();
            }
        }
        return (this._lsValue || '');
    }
    BuildBufferArray() {
        if (this._buffer.byteLength === 0) {
            if (this._lsValue == '') {
                this._buffer = new ArrayBuffer(0);
            }
            else {
                const arrayData = Uint8Array.from(atob(this._lsValue), c => c.charCodeAt(0));
                this._buffer = arrayData.buffer;
            }
        }
    }
    BufferHasData() {
        try {
            this.RefreshBuffer();
            return (this.GetBufferData() != '');
        }
        catch (ex) {
            console.log(`JsStreamBuffer:BufferHasData. Error ${ex}`);
        }
        return false;
    }
    BufferLength() {
        try {
            this.RefreshBuffer();
            return this.GetBufferData().length;
        }
        catch (ex) {
            console.log(`JsStreamBuffer:BufferLength. Error ${ex}`);
            return 0;
        }
    }
    ClearBuffer() {
        try {
            this._lsValue = null;
            RemoveLocalStorageItem(this._jsBufferName);
        }
        catch (ex) {
            console.log(`JsStreamBuffer:ClearBuffer. Error ${ex}`);
        }
    }
    ReadBuffer(fromIndex) {
        try {
            const bufferData = this.GetBufferData();
            if (this._buffer.byteLength === 0) {
                return '';
            }
            const totalLeft = Math.min(this._readLength, this._buffer.byteLength - fromIndex);
            const result = new Uint8Array(this._buffer, fromIndex, totalLeft);
            return result.toString();
        }
        catch (ex) {
            console.log(`JsStreamBuffer:ReadBuffer. Error ${ex}`);
            return null;
        }
    }
}
