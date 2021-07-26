import './vendor.min.js';
export function ScrollToElement(elementSelector) {
    const element = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
        element.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
        const x = document.querySelector('xxx');
    }
}
export function RenderCanvasToImage(canvasElement) {
    html2canvas(canvasElement, {
        onrendered: function (canvas) {
            var data = canvas.toDataURL();
            var img = document.createElement('img');
            img.setAttribute('download', 'myImage.png');
            img.src = 'data:image/png;base64,' + data;
            document.body.appendChild(img);
        },
        width: 300,
        height: 300
    });
}
