export const truncateText = (inputText: string): string => {
    if (inputText.length > 45) {
        return `${inputText.substring(0, 43)}...`
    }
    return inputText
}