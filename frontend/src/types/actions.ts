import { Role, ProjectRoleFormData } from './projects'

export type ProjectFormAction = 
    { type: 'SET_PROJECT_NAME', payload: string } |
    { type: 'SET_PROJECT_DESCRIPTION', payload: string } |
    { type: 'ADD_ROLE', payload: Role } |
    { type: 'REMOVE_ROLE', payload: number } |
    { type: 'UPDATE_ROLE', payload: ProjectRoleFormData }
