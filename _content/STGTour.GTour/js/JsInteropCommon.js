export function ScrollToElement(elementSelector) {
    const element = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
        element.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
    }
}
export function AddClassToElement(elementSelector, className) {
    const element = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
        element.classList.add(className);
    }
}
export function RemoveClassFromElement(elementSelector, className) {
    const element = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
        element.classList.remove(className);
    }
}
