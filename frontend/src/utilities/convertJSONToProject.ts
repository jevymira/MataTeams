import { CreateProject, Project, ProjectRoleCreate, ProjectRoleForm } from "../types"

export const convertJSONToProject = (json: any): Project => {
    const { id, teamName, ownerId, description, status, type, roles, teams, canCopy, copyOf } = json
    
    const project: Project = {
        id, name: teamName, ownerId, description, status, type, roles, teams, canCopy, copyOf
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
        }),
        canCopy: project.canCopy,
        copyOf: project.copyOf ?? null
    }
    return JSON.stringify(newJsonObject)
}