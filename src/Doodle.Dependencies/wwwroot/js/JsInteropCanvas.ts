
import './vendor.min.js';

// @ts-ignore
export let _jsStreamManager: JsStreamManager;

export function RenderCanvasToImage(renderElement: HTMLElement): string {

  if (_jsStreamManager == null) {
    console.log(`Creating new JS Stream Manager Object`)
    // @ts-ignore
    _jsStreamManager = new JsStreamManager();
  }
  // Generate the buffer Id
  const bufferId: string = Date.now().toString(18) + Math.random().toString(36).substring(2);;
  // @ts-ignore
  html2canvas(renderElement, { allowTaint: true, useCORS: true }).then(canvas => {
    // Load the data Url
    let renderedData: string = canvas.toDataURL("image/png");
    // Remove the data:image/png;base64, part
    renderedData = renderedData.substring(renderedData.indexOf(",") + 1);
    // Add the buffer stream
    _jsStreamManager.AddBuffer(bufferId, renderedData);
  });
  return bufferId;
} 

export function ClearBufferedImage(bufferId: string): void {
  _jsStreamManager.RemoveBuffer(bufferId)
}

export function ReadBufferedImage(bufferId: string, fromIndex: number): string {
  return _jsStreamManager.ReadBuffer(bufferId, fromIndex)
} 

export function ReadBufferedImageBase64(bufferId: string, fromIndex: number): string {
  return _jsStreamManager.ReadBufferBase64(bufferId, fromIndex)
} 

export function BufferExists(id: string): boolean {
  return _jsStreamManager.BufferExists(id);
}

export function BufferLength(id: string): number {
  return _jsStreamManager.BufferLength(id);
}