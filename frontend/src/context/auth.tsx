import React, { createContext } from 'react'
import { useLocalStorage } from '../hooks/useLocalStorage'
import { AuthContextType } from '../types'

export const AuthContext = createContext<AuthContextType | null>(null)

const AuthContextProvider = ({ children }: React.PropsWithChildren<unknown>) => {
    const [token, setToken] = useLocalStorage("token", "")
    const [userID, setUserID] = useLocalStorage("userID", "")

    return <AuthContext.Provider value={{
        userID, 
        token,
        setUserID,
        setToken
    }}>
        {children}
    </AuthContext.Provider>
}

export default AuthContextProvider