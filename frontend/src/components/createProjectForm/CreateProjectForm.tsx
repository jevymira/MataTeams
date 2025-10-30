// libraries
import { useState, Dispatch, useReducer, useContext } from 'react'
import { Container, Field, Text, Input, Button, IconButton } from '@chakra-ui/react'
import { LuUserPlus } from "react-icons/lu"

// hooks
import { useCreateProject } from '../../hooks/projects'

// components
import SkillsDropdown from '../skillsDropdown/SkillsDropdown'

// context
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'
import { createProjectFormReducer, defaultCreateProject } from '../../reducers/createProjectForm'

function CreateProjectForm() {
    const [formState, dispatch] = useReducer(createProjectFormReducer, defaultCreateProject)
    const { token } = useContext(AuthContext) as AuthContextType
    const [createProject] = useCreateProject(formState, token)
 
    return (
        <Container maxWidth={500}>
            <Field.Root>
                <Field.Label>
                    <Field.RequiredIndicator />
                    <Text>Project Name</Text>
                </Field.Label>
                <Input size='md' value={formState.name} onChange={e => {
                    dispatch({type: 'SET_PROJECT_NAME', payload: e.target.value})
                }} />
                <Field.ErrorText>
                    <Text>Project name must be longer than one character!</Text>
                </Field.ErrorText>
            </Field.Root>

            <Field.Root>
                <Field.Label>
                    <Field.RequiredIndicator />
                    <Text>Description</Text>
                </Field.Label>
                <Input size='md' value={formState.description} onChange={e => {
                    dispatch({type: 'SET_PROJECT_DESCRIPTION', payload: e.target.value})
                }} />
                <Field.ErrorText>
                    <Text>Project name must be longer than one character!</Text>
                </Field.ErrorText>
            </Field.Root>
            <IconButton onClick={(e) => {
                dispatch({type: 'ADD_ROLE'})
            }}>
                <LuUserPlus aria-label="Add new role"/>
            </IconButton>
            {formState.roles.map((r, i) => {
                return <AddRoleForm index={i} dispatch={dispatch} key={i} role={r} />
            })}
            <Button onClick={createProject}>
                <Text>Submit</Text>
            </Button>
        </Container>

    )
  }
  
export default CreateProjectForm