import { useContext, useState } from 'react'

// context
import { ProjectsContext } from '../context/projects'

// types
import { ProjectsContextType, Project, Skill, Role, CreateProject } from '../types'

// utilities
import { convertJSONToProject } from '../utilities/convertJSONToProject'


export function useCreateProject(createProjectData: CreateProject) {
    const createProject = async () => {
        const options = {
            method: 'POST'
        }
    }
}

export function useGetProjectByID(id: string) {
    const [project, setProject] = useState<Project>();

    const getProjectByID = async () => {
        const options = {
            method: 'GET'
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

export function useGetAllProjects() {
    const { projects, setProjects } = useContext(ProjectsContext) as ProjectsContextType

    const getProjects = async () => {
        const options = {
            method: 'GET'
        }
        
        try {
            var projectsFromServer: Array<Project> = []
            for (let i = 1; i < 6; i++) {
                fetch('https://localhost:7260/api/projects/' + i, options).then(res => {
                    if (res.status !== 200) {
                        console.error('error!')
                        return -1
                    }

                    return res.json()
                }).then(jsonRes => {
                    // TODO: this approach causing flickering, it is a 
                    // workaround until we have a /get all projects endpoint
                    let project: Project = convertJSONToProject(jsonRes)
                    projectsFromServer = [...projectsFromServer, project]
                }).then(() => {
                    setProjects(projectsFromServer)
                })
            }
            
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
                console.log(res)
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
                console.log(res)
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