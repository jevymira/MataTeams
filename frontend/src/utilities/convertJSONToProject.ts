import { CreateProject, Project, ProjectRoleForm } from "../types"

export const convertJSONToProject = (json: any): Project => {
    const { id, name, description, status, type, roles } = json
    
    const project: Project = {
        id, name, description, status, type, roles
    }
    return project
}

export const convertProjectToJSON = (project: CreateProject): string => {
    var newJsonObject = {
        name: project.name,
        description: project.description,
        type: project.type,
        status: project.status,
        roles: project.roles.map(r => {
            var roleForm: ProjectRoleForm = {
                roleId: r.roleId,
                positionCount: parseInt(r.positionCount),
                skillIds: r.skillIds
            }
            return roleForm
        })
    }
    return JSON.stringify(newJsonObject)
}