import { useContext } from 'react'
import { ProjectsContext } from "../context/Projects"
import { ProjectsContextType, Project } from '../types'
import { convertJSONToProject } from '../utilities/convertJSONToProject'

export function useGetAllProjects() {
    const { projects, setProjects } = useContext(ProjectsContext) as ProjectsContextType

    const getProjects = async () => {
        const options = {
            method: 'GET'
        }
        try {
            var projectsFromServer: Array<Project> = []
            for (let i = 1; i < 4; i++) {
                fetch('https://localhost:7260/api/projects/' + i, options).then(res => {
                    if (res.status !== 200) {
                        console.error('error!')
                    }

                    return res.json()
                }).then(jsonRes => {
                    let project: Project = convertJSONToProject(jsonRes)
                    console.log(project)
                    projectsFromServer.push(project)
                })
            }

            setProjects(projectsFromServer)
        } catch (e) {
            console.error(e)
        }
    }

    return [projects, getProjects] as const
}