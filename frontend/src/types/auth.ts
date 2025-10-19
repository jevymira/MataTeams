export type AuthContextType = {
    userID: number
    username: string
    token: string
    setToken: (token: string) => void
    setUserID: (userID: string) => void
    setUsername: (username: string) => void
}