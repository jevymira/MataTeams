import { CreateProject, DefaultRole, ProjectFormAction } from '../types'

export const createProjectFormReducer = (state: CreateProject, action: ProjectFormAction) => {
    switch (action.type) {
        case 'SET_PROJECT_NAME': {
            return {...state, description: action.payload}
        }
        case 'SET_PROJECT_DESCRIPTION': {
            return {...state, description: action.payload}
        }
        case 'ADD_ROLE': {
            return {
                ...state,
                role: [...state.roles, DefaultRole]
            }
        }
        case 'REMOVE_ROLE': {
            return {
                ...state,
                roles: state.roles.filter((r, i) => {
                    if (i !== action.payload) {
                        return r
                    }
                })
            }
        }
        case 'UPDATE_ROLE': {
            return {
                ...state,
                roles: state.roles.filter((r, i) => {
                    if (i == action.payload.index) {
                        return action.payload.role
                    } else {
                        return r
                    }
                })
            }
        }
    }
}