import { Project } from "../types"

export const convertJSONToProject = (json: any): Project => {
    const { id, name, description, status, type } = json
    
    const project: Project = {
        id, name, description, status, type
    }
    return project
}