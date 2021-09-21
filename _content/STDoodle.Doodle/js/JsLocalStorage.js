export function SetLocalStorageItem(key, value) {
    localStorage.setItem(key, value);
}
export function ClearLocalStorageItem(key) {
    localStorage.setItem(key, null);
}
export function RemoveLocalStorageItem(key) {
    localStorage.removeItem(key);
}
export function ReadLocalStorageItem(key) {
    return localStorage.getItem(key);
}
