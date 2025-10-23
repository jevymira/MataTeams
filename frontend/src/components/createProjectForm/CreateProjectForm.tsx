// libraries
import { useState, Dispatch, useReducer } from 'react'
import { Container, Field, Text, Input, Button, IconButton } from '@chakra-ui/react'
import { LuUserPlus } from "react-icons/lu"

// components
import SkillsDropdown from '../skillsDropdown/SkillsDropdown'

// types
import { Role, DefaultRole, CreateProject, ProjectFormAction } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'
import { createProjectFormReducer, defaultCreateProject } from '../../reducers/createProjectForm'

function CreateProjectForm() {
    const [projectName, setName] = useState("")
    const [description, setDescription] = useState("")
    const [formState, dispatch] = useReducer(createProjectFormReducer, defaultCreateProject)
 
    return (
        <Container maxWidth={500}>
            <Field.Root>
                <Field.Label>
                    <Field.RequiredIndicator />
                    <Text>Project Name</Text>
                </Field.Label>
                <Input size='md' onChange={e => {
                    setName(e.target.value)
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
                <Input size='md' onChange={e => {
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
            <Button>
                <Text>Submit</Text>
            </Button>
        </Container>

    )
  }
  
export default CreateProjectForm