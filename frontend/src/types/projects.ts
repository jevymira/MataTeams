export type Project = {
    id: string
    name: string
    description: string
    type: string
    status: string
    roles: ProjectRole[]
}

export type CreateProject = Omit<Project, "id">

export type ProjectsContextType = {
    projects: Array<Project>
    setProjects: (projects: Array<Project>) => void
}

//**** PROJECT ROLES ****/
export type ProjectRole = {
    roleId: string
    roleName: string
    positionCount: string
    skillIds: string[]
}

export type ProjectRoleForm = {
    roleId: string
    positionCount: number
    skillIds: string[]
}

export type ProjectRoleFormSkills = {
    skills: Skill[]
    index: number
}

export type ProjectRoleFormPositionCount = {
    posititionCount: string
    index: number
}

export const DefaultProjectRole : ProjectRole = {
    roleId: '0199eef1-d1bd-75e9-8c25-39531d023e73',
    roleName: '',
    positionCount: '0',
    skillIds: []
}


//**** ROLES ****/
export const DefaultRole : Role = {
    id: '',
    name: '',
}

export type Role = {
    id: string
    name: string
}

//**** SKILLS ****/
export type Skill = {
    id: string
    name: string
}