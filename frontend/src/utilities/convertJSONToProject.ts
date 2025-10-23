import { Project } from "../types"

export const convertJSONToProject = (json: any): Project => {
    const { id, name, description, status, type, roles } = json
    
    const project: Project = {
        id, name, description, status, type, roles
    }
    return project
}