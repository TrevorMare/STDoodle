export class JsStreamManager {
    constructor() {
        this._buffers = new Array();
    }
    GetBufferByName(bufferId) {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == bufferId);
            if (ix => 0) {
                return this._buffers[ix];
            }
            return null;
        }
        catch (ex) {
            return null;
        }
    }
    GetBufferIndexByName(bufferId) {
        try {
            const bufferIndex = this._buffers.findIndex(b => b.JsBufferName == bufferId);
            return bufferIndex;
        }
        catch (ex) {
            return -1;
        }
    }
    LoadBuffer(bufferId) {
        const buffer = new JsStreamBuffer(bufferId);
        this._buffers.push(buffer);
        return bufferId;
    }
    RemoveBuffer(bufferId) {
        var bufferIndex = this.GetBufferIndexByName(bufferId);
        if (bufferIndex >= 0) {
            console.log(`Buffer with Id ${bufferId} removed.`);
            this._buffers[bufferIndex].ClearBuffer();
            this._buffers.splice(bufferIndex, 1);
        }
        else {
            console.log(`Unable to clear buffer with Id ${bufferId}. Not found`);
        }
    }
    BufferExists(bufferId) {
        return (this.GetBufferIndexByName(bufferId) >= 0);
    }
    BufferHasData(bufferId) {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.BufferHasData();
        }
        return false;
    }
    BufferLength(bufferId) {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.BufferLength();
        }
        return 0;
    }
    ReadBuffer(bufferId, index) {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.ReadBuffer(index);
        }
        return null;
    }
}
