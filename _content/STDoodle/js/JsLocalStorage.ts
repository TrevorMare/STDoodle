export function SetLocalStorageItem(key: string, value: string): void {
    localStorage.setItem(key, value);
}

export function ClearLocalStorageItem(key: string): void {
    localStorage.setItem(key, null);
}

export function RemoveLocalStorageItem(key: string): void {
    localStorage.removeItem(key);
}

export function ReadLocalStorageItem(key: string): string {
    return localStorage.getItem(key);
}