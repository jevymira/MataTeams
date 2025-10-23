import { Role, ProjectRoleFormSkills, ProjectRoleFormPositionCount } from './projects'

export type ProjectFormAction = 
    { type: 'SET_PROJECT_NAME', payload: string } |
    { type: 'SET_PROJECT_DESCRIPTION', payload: string } |
    { type: 'ADD_ROLE' } |
    { type: 'REMOVE_ROLE', payload: number } |
    { type: 'UPDATE_ROLE_SKILLS', payload: ProjectRoleFormSkills } |
    { type: 'UPDATE_ROLE_POSITION_COUNT', payload: ProjectRoleFormPositionCount }
