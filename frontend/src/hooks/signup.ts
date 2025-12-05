// libraries
import { useContext } from 'react'
import { useNavigate } from 'react-router'

// context
import { AuthContext } from '../context/auth'
import { AuthContextType, Skill } from '../types'

export function useSignup(email: string, password: string, firstName: string, lastName: string, username: string, skills: Array<Skill>) {
    const { setUserID, setToken, setFirst, setLast, setUsername, setSkills } = useContext(AuthContext) as AuthContextType
    const navigate = useNavigate()

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-type': 'application/json'},
        body: JSON.stringify({email, password})
    }

    const signup = async () => {
        try {
            fetch('https://localhost:7190/api/auth/register', requestOptions).then((res) => {
                if (res.status !== 200) {
                    throw new Error("Sign up failed")
                } else {
                    return res.json()
                }
            }).then((resJSON) => {
                const token = resJSON['token']
                setToken(token)
                setFirst(firstName)
                setLast(lastName)
                setUsername(username)
                setSkills(skills)
                navigate("/")     
            }).catch((err) => {
                console.error(err)
            })
            
        } catch (err) {
            console.error(err)
        }
    }

    return [signup] as const
}