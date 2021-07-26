export function ScrollToElement(elementSelector) {
    const element = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
        element.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
    }
}
