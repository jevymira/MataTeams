export type AuthContextType = {
    userID: number
    token: string
    setToken: (token: string) => void
    setUserID: (userID: string) => void
}