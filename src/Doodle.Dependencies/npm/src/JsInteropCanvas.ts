
import './vendor.min.js';
import { JsStreamManager } from './JsStreamManager'

export const _jsStreamManager: JsStreamManager = new JsStreamManager();

export function RenderCanvasToImage(renderElement: HTMLElement): string {

  var bufferId: string = "88888";
  // @ts-ignore
  html2canvas(renderElement).then(canvas => {
    var renderedData = canvas.toDataURL("image/png");
    _jsStreamManager.AddBuffer(bufferId, renderedData);
  });
  return bufferId;
} 

export function ClearBufferedImage(bufferId: string): void {
  _jsStreamManager.RemoveBuffer(bufferId)
}

export function ReadBufferedImage(bufferId: string, fromIndex: number): Uint8Array {
  return _jsStreamManager.ReadBuffer(bufferId, fromIndex)
} 

export function BufferExists(id: string): boolean {
  return _jsStreamManager.BufferExists(id);
}

export function BufferLength(id: string): number {
  return _jsStreamManager.BufferLength(id);
}