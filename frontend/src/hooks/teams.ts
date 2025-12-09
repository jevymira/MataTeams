import { useState } from "react"
import { PendingRequest, ProjectRoleResponse, UserRole } from "../types"

export function useGetUserRoles(token: string) {
    const [userRoles, setUserRoles] = useState<UserRole[]>([])

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

export function useGetPendingRequests(token: string) {
    const [pendingRequests, setPendingRequests] = useState<PendingRequest[]>([])
    
        const getRequests = async () => {
            const options = {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
            }
            
            try {   
                fetch('https://localhost:7260/api/users/me/requests', options).then(res => {
                    if (res.status !== 200) {
                        throw new Error(res.statusText)
                    }
    
                    return res.json()
                }).then(json => {          
                    setPendingRequests(json.items)
                })
            } catch(e) {
                setPendingRequests([])
                console.error(e)
            }
        }
        return [pendingRequests, getRequests] as const
}

export function useRequestRole(projectRoleId: string, teamId: string, token: string) {
    const [roleRequest, setRoleRequest] = useState<ProjectRoleResponse>()

        const requestRole = async () => {
            console.log(projectRoleId)
            const options = {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({projectRoleId: projectRoleId}),
            }
            
            try {   
                fetch(`https://localhost:7260/api/teams/${teamId}/requests`, options).then(res => {
                    if (res.status !== 201) {
                        throw new Error(res.statusText)
                    }

                    return res.json()
                }).then(json => {       
                    console.log(json)   
                    setRoleRequest(json)
                })
            } catch(e) {
                console.error(e)
            }
        }

    return [roleRequest, requestRole] as const
}