export class JsStreamManager {
    constructor() {
        this._buffers = new Array();
    }
    AddBuffer(id, base64) {
        console.log(`Adding stream ${id} with length ${base64.length}`);
        const buffer = new JsStreamBuffer(id, base64);
        this._buffers.push(buffer);
        console.log(`Current buffer count: ${this._buffers.length}`);
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
            console.log(`Unable to read buffer: ${ex}`);
        }
        return null;
    }
    ReadBufferBase64(id, index) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == id);
            if (ix >= 0) {
                return this._buffers[ix].ReadBufferBase64(index);
            }
        }
        catch (ex) {
            console.log(`Unable to read buffer: ${ex}`);
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
            console.log(`BufferExists: ${ex}`);
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
            console.log(`BufferLength: ${ex}`);
        }
        return 0;
    }
}
