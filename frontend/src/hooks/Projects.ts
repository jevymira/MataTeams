import { useContext, useState } from 'react'
import { ProjectsContext } from "../context/Projects"
import { ProjectsContextType, Project } from '../types'
import { convertJSONToProject } from '../utilities/convertJSONToProject'


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