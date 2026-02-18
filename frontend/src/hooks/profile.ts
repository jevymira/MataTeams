import { useState } from "react"
import { User } from "../types"

export function useUpdateUserFirstName(id: string, token: string, firstName: string) {
    const [user, setUser] = useState<User>()

    const putUser = async () => {
        const options = {
            method: 'PUT',
            body: JSON.stringify({
                firstName
            }),
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }
        try {
            fetch(`https://localhost:7260/api/users/me`, options).then(res => {
                if (res.status !== 200) {
                    console.error(res.statusText)
                    return -1
                }

                return res.json()
            })
        } catch (e) {
            console.error(e)
            return e
        }

    }
    return [putUser] as const
}