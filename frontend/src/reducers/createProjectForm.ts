import { CreateProject, DefaultProjectRoleCreate, ProjectFormAction, ProjectRoleCreate } from '../types'

export const createProjectFormReducer = (state: CreateProject, action: ProjectFormAction) => {
    switch (action.type) {
        case 'SET_PROJECT_NAME': {
            return {...state, name: action.payload}
        }
        case 'SET_PROJECT_DESCRIPTION': {
            return {...state, description: action.payload}
        }
        case 'SET_PROJECT_TYPE': {
            return {...state, projectType: action.payload}
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
                    } else {
                        return r
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
                        return {...r, positionCount: action.payload.posititionCount}
                    } else {
                        return r
                    }
                })
            }
        }
        case 'SET_LEADER_ROLE': {
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload) {
                        var newRole: ProjectRoleCreate = r
                        newRole.isLeaderRole = true
                        return newRole
                    } else {
                        var newRole: ProjectRoleCreate = r
                        newRole.isLeaderRole = false
                        return newRole
                    }
                })
            }
        }
    }
}

export const defaultCreateProject: CreateProject = {
    name: '',
    description: '',
    projectType: '',
    status: 'Planning',
    roles: []
}