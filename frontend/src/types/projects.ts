export type Project = {
    id: string
    description: string
    matchPercentage?: number
    name: string
    type: string
    status: string
    roles: ProjectRole[]
    teams: Team[]
}

export type TeamMember = {
    id: string
    name: string
}

export type Team = {
    name: string
    leader: TeamMember
    projectRoles: ProjectRole[]
    vacantPositionCount: number
    members: TeamMember[]
}

export type CreateProject = {
    name: string
    description: string
    projectType: string
    status: string
    roles: ProjectRoleCreate[]
}

export type ProjectsContextType = {
    projects: Array<Project>
    setProjects: (projects: Array<Project>) => void
    viewProjectId: string
    setViewProjectId: (id: string) => void
}

//**** PROJECT ROLES ****/
export type ProjectRole = {
    projectRoleId: string,
    roleId: string,
    roleName: string,
    positionCount: number,
    vacantPositionCount?: number,
    skills: Skill[] // TODO also has projectRoleSkillId here
}

export type ProjectRoleCreate = {
    roleId: string
    roleName: string
    positionCount: number
    isLeaderRole: boolean
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
    posititionCount: number
    index: number
}

export type ProjectRoleFormId = {
    roleId: string
    index: number
}

export const DefaultProjectRoleCreate : ProjectRoleCreate = {
    roleId: '0199eef1-d1bd-75e9-8c25-39531d023e73',
    roleName: '',
    positionCount: 0,
    isLeaderRole: false,
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