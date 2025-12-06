// libraries
import { useContext } from 'react'
import { useNavigate } from 'react-router'

// context
import { AuthContext } from '../context/auth'
import { AuthContextType, Skill } from '../types'

export function useSignup(
    email: string, 
    password: string, 
    firstName: string, 
    lastName: string, 
    username: string, 
    isFacultyOrStaff: boolean,
    skills: Array<Skill>) {
    const { setUserID, setToken, setFirst, setLast, setUsername, setSkills } = useContext(AuthContext) as AuthContextType
    const navigate = useNavigate()

    const signUpReqOptions = {
        method: 'POST',
        headers: { 'Content-type': 'application/json'},
        body: JSON.stringify({email, password})
    }

    const profileReqOptions = {
        method: 'POST',
        body: JSON.stringify({
            firstName, lastName, isFacultyOrStaff, programs: ["Computer Science"]
        })
    }

    const signup = async () => {
        try {
            fetch('https://localhost:7190/api/auth/register', signUpReqOptions).then((res) => {
                if (res.status !== 200) {
                    throw new Error("Sign up failed")
                } else {
                    return res.json()
                }
            }).then((resJSON) => {
                const token = resJSON['token']
                setToken(token)
                
                return fetch('https://localhost:7190/api/users', profileReqOptions)
            }).then((profileRes) => {
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