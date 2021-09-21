import './vendor.min.js';
export function ConvertElementToImage(renderElement, optionsJson = null) {
    let config = {};
    if (optionsJson !== null && optionsJson !== '') {
        config = JSON.parse(optionsJson);
    }
    const bufferId = Date.now().toString(18) + Math.random().toString(36).substring(2);
    html2canvas(renderElement, config).then(canvas => {
        let renderedData = canvas.toDataURL("image/png");
        renderedData = renderedData.substring(renderedData.indexOf(",") + 1);
        SetLocalStorageItem(bufferId, renderedData);
    });
    return bufferId;
}
