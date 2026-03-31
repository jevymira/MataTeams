import { CreateProject, Project, ProjectRoleCreate, ProjectRoleForm } from "../types"

export const convertJSONToProject = (json: any): Project => {
    const { id, teamName, description, status, type, roles, teams } = json
    
    const project: Project = {
        id, name: teamName, description, status, type, roles, teams
    }
    return project
}

export const convertProjectToJSON = (project: CreateProject): string => {
    var newJsonObject = {
        name: project.name,
        description: project.description,
        type: project.projectType,
        status: project.status,
        roles: project.roles.map((r: ProjectRoleCreate) => {
            var createRole: ProjectRoleForm = {
                roleId: r.roleId,
                positionCount: r.positionCount,
                skillIds: r.skillIds
            }
            return createRole
        })
    }
    return JSON.stringify(newJsonObject)
}