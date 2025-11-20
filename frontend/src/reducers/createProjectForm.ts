import { CreateProject, DefaultProjectRoleCreate, DefaultRole, ProjectFormAction, ProjectRoleCreate } from '../types'

export const createProjectFormReducer = (state: CreateProject, action: ProjectFormAction) => {
    switch (action.type) {
        case 'SET_PROJECT_NAME': {
            return {...state, name: action.payload}
        }
        case 'SET_PROJECT_DESCRIPTION': {
            return {...state, description: action.payload}
        }
        case 'ADD_ROLE': {
            return {
                ...state,
                roles: [...state.roles, DefaultProjectRoleCreate]
            }
        }
        case 'SET_ROLE_ID': {
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload.index) {
                        return {...r, roleId: action.payload.roleId}
                    }
                })
            }
        }
        case 'REMOVE_ROLE': {
            return {
                ...state,
                roles: state.roles.filter((r, i) => {
                    return (i !== action.payload)
                })
            }
        }
        case 'UPDATE_ROLE_SKILLS': {
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload.index) {
                        // var newRole: ProjectRole = r
                        return {...r, skillIds: action.payload.skills.map(skill => skill.id)}
                    } else {
                        return r
                    }
                })
            }
        }
        case 'UPDATE_ROLE_POSITION_COUNT': {
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload.index) {
                        var newRole: ProjectRoleCreate = r
                        newRole.positionCount = parseInt(action.payload.posititionCount)
                        return newRole
                    } else {
                        return r
                    }
                })
            }
        }
    }
}

export const defaultCreateProject: CreateProject = {
    name: '',
    description: '',
    type: 'ARCS',
    status: 'Planning',
    roles: []
}