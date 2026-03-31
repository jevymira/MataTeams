// libraries
import { useState } from "react"

// types
import { User } from "../types"


export function useGetUserByID(id: string, token: string) {
    const [user, setUser] = useState<User>()

    const getUser = async () => {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        }

        try {   
            fetch(`https://localhost:7260/api/users/${id}`, options).then(res => {
                return res.json()
            }).then(json => {
                setUser(json)
            })
            } catch(err) {
                console.error(err)
            }
    }

    return [user, getUser] as const
}