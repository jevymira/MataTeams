// libraries
import { useState, useContext } from "react"

// types
import { User } from "../types"

// context
import { UserContext } from '../context/auth'
import { UserContextType } from '../types'

export function useUpdateUser(firstName: string, lastName: string) {
    const { token, setSkills, setFirst, setLast, skills } = useContext(UserContext) as UserContextType
    const skillIds = skills.map(s => {
        return s.id
    })

    const putUser = async () => {
        const options = {
            method: 'PUT',
            body: JSON.stringify({
                firstName,
                lastName, 
                skillIds
            }),
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }
        try {
            fetch(`https://localhost:7260/api/users/me`, options).then(res => {
                console.log(res.status)
                if (res.status !== 200) {
                    console.error(res.statusText)
                    return -1
                }
                return res.json()
            }).then(json => {
                console.log(json)
                setFirst(firstName)
                setLast(lastName)
                setSkills(json['skillIds'])
            })
        } catch (e) {
            console.error(e)
            return e
        }

    }

    return [putUser] as const
}