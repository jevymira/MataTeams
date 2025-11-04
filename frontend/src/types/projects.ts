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
    roleID: string
    roleName: string
    positionCount: string
    skillIDs: string[]
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
    roleID: '',
    roleName: '',
    positionCount: '0',
    skillIDs: []
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