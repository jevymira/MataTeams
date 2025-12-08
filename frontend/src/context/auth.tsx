import React, { createContext, useState } from 'react'
import { useLocalStorage } from '../hooks/useLocalStorage'
import { UserContextType, Skill } from '../types'

export const UserContext = createContext<UserContextType | null>(null)

const UserContextProvider = ({ children }: React.PropsWithChildren<unknown>) => {
    const [token, setToken] = useLocalStorage("token", "")
    const [userID, setUserID] = useLocalStorage("userID", "")
    const [username, setUsername] = useLocalStorage("username", "")
    const [firstName, setFirst] = useLocalStorage("firstName", "")
    const [lastName, setLast] = useLocalStorage("lastName", "")
    const [skills, setSkills] = useState<Array<Skill>>([])

    return <UserContext.Provider value={{
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
    </UserContext.Provider>
}

export default UserContextProvider