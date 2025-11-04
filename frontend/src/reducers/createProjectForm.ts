import { CreateProject, DefaultProjectRole, DefaultRole, ProjectFormAction, ProjectRole } from '../types'

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
                roles: [...state.roles, DefaultProjectRole]
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
            console.log(state)
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload.index) {
                        // var newRole: ProjectRole = r
                        return {...r, skillIDs: action.payload.skills.map(skill => skill.id)}
                    } else {
                        return r
                    }
                })
            }
        }
        case 'UPDATE_ROLE_POSITION_COUNT': {
            console.log('update role pos count')
            return {
                ...state,
                roles: state.roles.map((r, i) => {
                    if (i == action.payload.index) {
                        var newRole: ProjectRole = r
                        newRole.positionCount = action.payload.posititionCount
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
    type: '',
    status: '',
    roles: []
}