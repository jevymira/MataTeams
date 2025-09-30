export type Project = {
    id: number
    name: string
    description: string
    type: string
    status: string
}

export type ProjectsContextType = {
    projects: Array<Project>
    setProjects: (projects: Array<Project>) => void
}