export function ScrollToElement(elementSelector: string): void {
    const element: HTMLElement = document.querySelector(elementSelector);
    if (element !== undefined && element != null) {
      element.scrollIntoView({ behavior: "smooth", block: "center", inline: "center" });
    }
  }