import { CreateProject, DefaultProjectRole, DefaultRole, ProjectFormAction } from '../types'

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
                roles: [...state.roles, DefaultProjectRole]
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
        case 'UPDATE_ROLE_SKILLS': {
            return {
                ...state,
                roles: state.roles.filter((r, i) => {
                    if (i == action.payload.index) {
                        return {...r, skills: action.payload.skills}
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
                    console.log('index')
                    console.log(i)
                    if (i == action.payload.index) {
                        console.log({...r, positionCount: action.payload.posititionCount})
                        return {...r, positionCount: action.payload.posititionCount}
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