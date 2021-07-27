//import { JsStreamBuffer } from './JsStreamBuffer'

export class JsStreamManager {
    // @ts-ignore
    private _buffers: JsStreamBuffer[] = new Array();

    public AddBuffer(id: string, base64: string) : string {
        console.log(`Adding stream ${id} with length ${base64.length}`)
        // @ts-ignore
        const buffer = new JsStreamBuffer(id, base64);
        this._buffers.push(buffer);
        console.log(`Current buffer count: ${this._buffers.length}`);
        return id;
    }

    public RemoveBuffer(id: string) {
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

    public ReadBuffer(id: string, index: number): string {
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

    public ReadBufferBase64(id: string, index: number): string {
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

    public BufferExists(id: string): boolean {
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

    public BufferLength(id: string): number {
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