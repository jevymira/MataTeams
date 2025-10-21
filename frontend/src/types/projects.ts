export type Project = {
    id: string
    name: string
    description: string
    type: string
    status: string
}

export type CreateProjectForm = {
    name: string
    description: string
    type: string
    status: string
    roles: Role[]
}

export type Role = {
    id: string
    name: string
}

export type ProjectRole = {
    roleID: string
    positionCount: string
    skills: Skill[]
}

export type Skill = {
    id: string
    name: string
}

export type ProjectsContextType = {
    projects: Array<Project>
    setProjects: (projects: Array<Project>) => void
}