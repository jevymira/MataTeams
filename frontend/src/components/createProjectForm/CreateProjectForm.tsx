// libraries
import { useState, Dispatch, useReducer, useContext } from 'react'
import { Container, Field, Text, Input, Button, IconButton } from '@chakra-ui/react'
import { LuUserPlus } from "react-icons/lu"

// hooks
import { useCreateProject } from '../../hooks/projects'

// context
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'
import { createProjectFormReducer, defaultCreateProject } from '../../reducers/createProjectForm'

// style
import './CreateProjectForm.css'

function CreateProjectForm() {
    const [formState, dispatch] = useReducer(createProjectFormReducer, defaultCreateProject)
    const { token } = useContext(AuthContext) as AuthContextType
    const [createProject] = useCreateProject(formState, token)
 
    return (
        <Container maxWidth={500} style={{paddingTop: '20px'}}>
            <Field.Root style={{paddingBottom: '25px'}}>
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

            <Field.Root style={{paddingBottom: '25px'}}>
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
            {formState.roles.map((r, i) => {
                return <AddRoleForm index={i} dispatch={dispatch} key={i} role={r} />
            })}
            <div className='formButtons'>
                <IconButton onClick={(e) => {
                    dispatch({type: 'ADD_ROLE'})
                }}>
                    <LuUserPlus aria-label="Add new role"/>
                </IconButton>
                <Button style={{marginLeft: '5px'}} onClick={createProject}>
                    <Text>Submit</Text>
                </Button>
            </div>
        </Container>

    )
  }
  
export default CreateProjectForm