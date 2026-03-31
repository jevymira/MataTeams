// libraries
import { useState } from "react"

// types
import { User } from "../types"


export function useGetUser(token: string) {
    const [userRoles, setUserRoles] = useState<User[]>([])

    const getRoles = async () => {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        }

        try {   
            fetch('https://localhost:7260/api/users/me/roles', options).then(res => {
                return res.json()
            }).then(json => {
                setUserRoles(json)
            })
            } catch(err) {
                console.error(err)
            }
    }

    return [userRoles, getRoles] as const
}