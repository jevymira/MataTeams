// libraries
import { useContext } from 'react'
import { useNavigate } from 'react-router'

// context
import { AuthContext } from '../context/auth'
import { AuthContextType } from '../types'

export function useLogin(username: string, password: string) {
    const { setUserID, setToken } = useContext(AuthContext) as AuthContextType
    const navigate = useNavigate()

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-type': 'application/json'},
        body: JSON.stringify({username, password})
    }

    const login = async () => {
        try {
            fetch('https://localhost:7190/login', requestOptions).then((res) => {
                if (res.status !== 200) {
                    throw new Error("Could not login")
                } else {
                    return res.json()
                }
            }).then((resJSON) => {
                const token = resJSON['token']
                setToken(token)
                navigate("/")     
            }).catch((err) => {
                console.error(err)
            })
            
        } catch (err) {
            console.error(err)
        }
    }

    return [login] as const
}