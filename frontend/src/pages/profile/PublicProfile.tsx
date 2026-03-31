// libraries
import { useContext, useEffect, useState } from 'react'
import { useParams, useNavigate } from "react-router"

// context
import { UserContext } from '../../context/auth'

// types
import { UserContextType } from '../../types'

// hooks
import { useGetUserByID } from '../../hooks/users'

function PublicProfile() {
    let { id } = useParams()
    const navigate = useNavigate()
    const { token } = useContext(UserContext) as UserContextType

    const [user, getUser] = useGetUserByID(id ? id : '', token)

    useEffect(() => {
        if (!id || id == '') {
            navigate('/')
        } else {
            getUser()
        }
    }, [])

    return (
        <div>
            <p>{user ? user.firstName + " " + user.lastName: ''}</p>
            <div>
                {user && user.skills && user.skills.map(s => {
                    return <div>{s.name}</div>
                })}
            </div>
        </div>
    )
}

export default PublicProfile