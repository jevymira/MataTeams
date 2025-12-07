import { useState, useEffect } from "react";

export const getLocalValue = (key: string, initValue: string | Function) => {
    if (typeof window === 'undefined') return initValue;

    const itemRaw : string | null = localStorage.getItem(key)

    if (itemRaw !== null && itemRaw !== undefined) {
        return itemRaw
    }

    if (initValue instanceof Function) return initValue();

    return initValue;
}

export const useLocalStorage = (key: string, initValue: string) => {
    const [value, setValue] = useState(() => {
        return getLocalValue(key, initValue);
    });

    useEffect(() => {
        localStorage.setItem(key, value);
    }, [key, value])

    return [value, setValue];
}