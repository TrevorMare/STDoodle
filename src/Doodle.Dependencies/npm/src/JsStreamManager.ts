import { JsStreamBuffer } from './JsStreamBuffer'

export class JsStreamManager {

    private _buffers: JsStreamBuffer[] = new Array();

    public AddBuffer(id: string, base64: string) : string {
        const buffer = new JsStreamBuffer(id, base64);
        this._buffers.push(buffer);
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

    public ReadBuffer(id: string, index: number): Uint8Array {
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

    public BufferExists(id: string): boolean {
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

    public BufferLength(id: string): number {
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