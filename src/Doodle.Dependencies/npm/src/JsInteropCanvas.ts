/// <reference types="../node_modules/html2canvas/dist/types" />

import './vendor.min.js';

export function ScrollToElement(elementSelector: string): void {
    const element: HTMLElement = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
      element.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });

      const x: HTMLElement = document.querySelector('xxx');

    }
  }

  export function RenderCanvasToImage(canvasElement: HTMLElement) {
    // @ts-ignore
    html2canvas(canvasElement,{
        onrendered: function (canvas){
            var data = canvas.toDataURL();
            var img  = document.createElement('img');
            img.setAttribute('download','myImage.png');
            img.src  = 'data:image/png;base64,' + data;
            document.body.appendChild(img);
        },
        width:300,
        height:300
    });

  } 