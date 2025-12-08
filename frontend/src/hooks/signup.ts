// libraries
import { useContext } from 'react'
import { useNavigate } from 'react-router'

// context
import { UserContext } from '../context/auth'
import { UserContextType, Skill } from '../types'

export function useSignup(
    email: string, 
    password: string, 
    firstName: string, 
    lastName: string, 
    username: string, 
    isFacultyOrStaff: boolean,
    skills: Array<Skill>) {
    const { setUserID, setToken, setFirst, setLast, setUsername, setSkills } = useContext(UserContext) as UserContextType
    const navigate = useNavigate()

    const signUpReqOptions = {
        method: 'POST',
        headers: { 'Content-type': 'application/json'},
        body: JSON.stringify({email, password})
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
                
                const profileReqOptions = {
                    method: 'POST',
                    headers: { 
                        'Authorization': `Bearer ${token}`,
                        'Content-type': 'application/json'
                    },
                    body: JSON.stringify({
                        firstName,
                        lastName,
                        isFacultyOrStaff,
                        programs: ["Computer Science"],
                        skillIds: skills.map(skill => skill.id)
                    })
                }
                return fetch('https://localhost:7260/api/users', profileReqOptions)
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