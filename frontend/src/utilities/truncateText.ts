export const truncateText = (inputText: string): string => {
    if (inputText.length > 50) {
        return `${inputText.substring(0, 49)}...`
    }
    return inputText
}