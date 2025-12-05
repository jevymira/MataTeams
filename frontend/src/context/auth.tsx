import React, { createContext, useState } from 'react'
import { useLocalStorage } from '../hooks/useLocalStorage'
import { AuthContextType, Skill } from '../types'

export const AuthContext = createContext<AuthContextType | null>(null)

const AuthContextProvider = ({ children }: React.PropsWithChildren<unknown>) => {
    const [token, setToken] = useLocalStorage("token", "")
    const [userID, setUserID] = useLocalStorage("userID", "")
    const [username, setUsername] = useLocalStorage("username", "")
    const [firstName, setFirst] = useLocalStorage("firstName", "")
    const [lastName, setLast] = useLocalStorage("lastName", "")
    const [skills, setSkills] = useState<Array<Skill>>([])

    return <AuthContext.Provider value={{
        userID, 
        username,
        firstName,
        lastName,
        skills,
        token,
        setUserID,
        setToken,
        setFirst,
        setLast,
        setUsername,
        setSkills
    }}>
        {children}
    </AuthContext.Provider>
}

export default AuthContextProvider