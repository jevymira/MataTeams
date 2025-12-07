import { Role, ProjectRoleFormSkills, ProjectRoleFormPositionCount, ProjectRoleFormId } from './projects'

export type ProjectFormAction = 
    { type: 'SET_PROJECT_NAME', payload: string } |
    { type: 'SET_PROJECT_DESCRIPTION', payload: string } |
    { type: 'SET_PROJECT_TYPE', payload: string } |
    { type: 'ADD_ROLE' } |
    { type: 'SET_ROLE_ID', payload: ProjectRoleFormId} |
    { type: 'REMOVE_ROLE', payload: number } |
    { type: 'UPDATE_ROLE_SKILLS', payload: ProjectRoleFormSkills } |
    { type: 'UPDATE_ROLE_POSITION_COUNT', payload: ProjectRoleFormPositionCount } |
    { type: 'SET_LEADER_ROLE', payload: number }
