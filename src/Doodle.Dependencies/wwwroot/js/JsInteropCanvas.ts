
import './vendor.min.js';

export function RenderCanvasToImage(renderElement: HTMLElement): string {
  // Generate the buffer Id
  const bufferId: string = Date.now().toString(18) + Math.random().toString(36).substring(2);;
  // @ts-ignore
  html2canvas(renderElement, { allowTaint: true, useCORS: true }).then(canvas => {
    // Load the data Url
    let renderedData: string = canvas.toDataURL("image/png");
    // Remove the data:image/png;base64, part
    renderedData = renderedData.substring(renderedData.indexOf(",") + 1);
    // Add the buffer stream
    // @ts-ignore
    SetLocalStorageItem(bufferId, renderedData);
  });
  return bufferId;
} 
