export class JsStreamManager {
    constructor() {
        this._buffers = new Array();
    }
    AddBuffer(id, base64) {
        const buffer = new JsStreamBuffer(id, base64);
        this._buffers.push(buffer);
        return id;
    }
    RemoveBuffer(id) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == id);
            if (ix >= 0) {
                this._buffers.splice(ix, 1);
            }
        }
        catch (ex) {
            console.log(`Unable to remove buffer: ${ex}`);
        }
    }
    ReadBuffer(id, index) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == id);
            if (ix >= 0) {
                return this._buffers[ix].ReadBuffer(index);
            }
        }
        catch (ex) {
            console.log(`Unable to remove buffer: ${ex}`);
        }
        return null;
    }
    BufferExists(id) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == id);
            if (ix >= 0) {
                return true;
            }
        }
        catch (ex) {
        }
        return false;
    }
    BufferLength(id) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == id);
            if (ix >= 0) {
                return this._buffers[ix].BufferLength();
            }
        }
        catch (ex) {
        }
        return 0;
    }
}
