export class JsStreamManager {
    // @ts-ignore
    private _buffers: JsStreamBuffer[] = new Array();

    // @ts-ignore
    private GetBufferByName(bufferId: string): JsStreamBuffer {
        try {
            const ix = this._buffers.findIndex(b => b.JsBufferName == bufferId);
            if (ix => 0) {
                return this._buffers[ix];
            }
            return null;
        } catch (ex) {
            return null;    
        }
    }
    
    private GetBufferIndexByName(bufferId: string): number {
        try {
            const bufferIndex = this._buffers.findIndex(b => b.JsBufferName == bufferId);
            return bufferIndex;
        } catch (ex) {
            return -1;    
        }
    }

    public LoadBuffer(bufferId: string) : string {
        // @ts-ignore
        const buffer = new JsStreamBuffer(bufferId);
        this._buffers.push(buffer);
        return bufferId;
    }

    public RemoveBuffer(bufferId: string): void {
        var bufferIndex = this.GetBufferIndexByName(bufferId);
        if (bufferIndex >= 0) {
            console.log(`Buffer with Id ${bufferId} removed.`)
            this._buffers[bufferIndex].ClearBuffer();
            this._buffers.splice(bufferIndex, 1);
        } else {
            console.log(`Unable to clear buffer with Id ${bufferId}. Not found`)
        }
    }

    public BufferExists(bufferId: string): boolean {
        return (this.GetBufferIndexByName(bufferId) >= 0)
    }

    public BufferHasData(bufferId: string): boolean {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.BufferHasData();
        }
        return false;
    }

    public BufferLength(bufferId: string): number {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.BufferLength();
        }
        return 0;
    }

    public ReadBuffer(bufferId: string, index: number): string {
        const buffer = this.GetBufferByName(bufferId);
        if (buffer !== null) {
            return buffer.ReadBuffer(index);
        }
        return null;
    }
}