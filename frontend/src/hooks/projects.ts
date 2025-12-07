import { useContext, useState } from 'react'
import { useNavigate } from 'react-router'

// context
import { ProjectsContext } from '../context/project'

// types
import { ProjectsContextType, Project, Skill, Role, CreateProject, UserContextType } from '../types'

// utilities
import { convertJSONToProject, convertProjectToJSON } from '../utilities/convertJSONToProject'
import { UserContext } from '../context/auth'


export function useCreateProject(createProjectData: CreateProject, token: string) {
    const navigate = useNavigate()
    const { setViewProjectId } = useContext(ProjectsContext) as ProjectsContextType

    const createProject = async () => {
        const options = {
            method: 'POST',
            body: convertProjectToJSON(createProjectData),
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }

        const getProjectOptions = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }

        try {
            fetch('https://localhost:7260/api/projects', options).then(res => {
                if (res.status !== 201) {
                    console.error(res)
                    // TODO: set error state
                }
                return res.json()
            }).then(projID => {
                setViewProjectId(projID)
                fetch(`https://localhost:7260/api/projects/${projID}`, getProjectOptions).then(res => {
                    return res.json().then(projectJSON => {
                        // create team
                        let leaderRoleID = ''
                        let leaderProjectRoleId = ''
                        createProjectData.roles.forEach(role => {
                            if (role.isLeaderRole) {
                                leaderRoleID = role.roleId
                            }
                        }) 
    
                        let project: Project = convertJSONToProject(projectJSON)
                        project.roles.forEach(role => {
                            if (role.roleId == leaderRoleID) {
                                leaderProjectRoleId = role.projectRoleId
                            }
                        })
    
                        const createTeamOptions = {
                            method: 'POST',
                            body: JSON.stringify({
                                teamName:'My Team',
                                projectRoleId: leaderProjectRoleId
                            }),
                            headers: {
                                'Authorization': `Bearer ${token}`,
                                'Content-Type': 'application/json',
                            }
                        }

                        fetch(`https://localhost:7260/api/projects/${projID}/teams`, createTeamOptions).then(res => {
                            navigate('/project/view')
                        })
                    })
                })
            })
        } catch (e) {
            console.error(e)
        }
    }

    return [createProject] as const
}

export function useGetProjectByID(id: string, token: string) {
    const [project, setProject] = useState<Project>();

    const getProjectByID = async () => {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }
        try {
            fetch(`https://localhost:7260/api/projects/${id}`).then(res => {
                if (res.status !== 200) {
                    console.error(res.statusText)
                    return -1
                }

                return res.json()
            }).then(projectJSON => {
                setProject(convertJSONToProject(projectJSON))
            })
        } catch (e) {
            console.error(e)
            return e
        }

    }
    return [project, getProjectByID] as const
}

export function useGetRecommendedProjects(token: string) {
    const { projects, setProjects } = useContext(ProjectsContext) as ProjectsContextType
    const { setSkills, setFirst, setLast } = useContext(UserContext) as UserContextType

    const getProjects = async () => {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }

        try {
           fetch('https://localhost:7260/api/users/me', options).then((res) => {
            return res.json().then(json => {
                setSkills(json['skills'])
                setFirst(json['firstName'])
                setLast(json['lastName'])

                fetch('https://localhost:7260/api/users/me/recommendations', options).then(res => {
                    if (res.status !== 200) {
                        console.error('error!')
                        return -1
                    }
                    return res.json()
                }).then(jsonRes => {
                    setProjects(jsonRes['items']?.map((p: Project, i: number) => {
                        p.matchPercentage = i
                        return p
                    }))
                })
            })
           })
        } catch(err) {
            console.error(err)
        }
    }
    return [projects, getProjects] as const
}

export function useGetAllProjects(token: string) {
    const { projects, setProjects } = useContext(ProjectsContext) as ProjectsContextType

    const getProjects = async () => {
        const options = {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            }
        }
        
        try {
           // var projectsFromServer: Array<Project> = []
            fetch('https://localhost:7260/api/projects/', options).then(res => {
                if (res.status !== 200) {
                    console.error('error!')
                    return -1
                }

                return res.json()
            }).then(jsonRes => {
                setProjects(jsonRes['projects'].map((p: Project, i: number) => {
                    p.matchPercentage=i
                }))
            })
        } catch (e) {
            console.error(e)
            return e
        }
    }

    return [projects, getProjects] as const
}

export function useGetSkills() {
    const [skills, setSkills] = useState<Skill[]>([])

    const getSkills = async () => {
        const options = {
            method: 'GET'
        }
        
        try {   
            fetch(`https://localhost:7260/api/skills`).then(res => {
                if (res.status !== 200) {
                    throw new Error(res.statusText)
                }

                return res.json()
            }).then(json => {          
                setSkills(json)
            })
        } catch(e) {
            setSkills([])
            console.error(e)
        }
    }
    return [skills, getSkills] as const
}

export function useGetRoles() {
    const [roles, setRoles] = useState<Role[]>([])

    const getRoles = async () => {
        const options = {
            method: 'GET'
        }
        
        try {   
            fetch(`https://localhost:7260/api/roles`).then(res => {
                if (res.status !== 200) {
                    throw new Error(res.statusText)
                }

                return res.json()
            }).then(json => {          
                setRoles(json)
            })
        } catch(e) {
            setRoles([])
            console.error(e)
        }
    }
    return [roles, getRoles] as const
}