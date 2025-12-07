// libraries
import { useState, Dispatch, useReducer, useContext } from 'react'
import { Container, Field, Text, Input, Button, IconButton, Flex, ScrollArea } from '@chakra-ui/react'
import { LuUserPlus } from "react-icons/lu"

// hooks
import { useCreateProject } from '../../hooks/projects'

// context
import { UserContext } from '../../context/auth'

// components
import ProjectTypeDropdown from '../projectTypeDropdown/ProjectTypeDropdown'

// types
import { UserContextType } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'
import { createProjectFormReducer, defaultCreateProject } from '../../reducers/createProjectForm'

// style
import './CreateProjectForm.css'
import ProjectStatusDropdown from '../projectStatusDropdown/ProjectStatusDropdown'

function CreateProjectForm() {
    const [formState, dispatch] = useReducer(createProjectFormReducer, defaultCreateProject)
    const { token } = useContext(UserContext) as UserContextType
    const [createProject] = useCreateProject(formState, token)
 
    return (
        <Flex alignItems={'center'} flexDirection={'column'}>
            <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontSize={'26px'} padding={'20px'} textAlign={'center'}>
                Create a new project
            </Text>
            <ScrollArea.Root maxWidth={500} paddingTop={'10px'} height={'80vh'}>
                <ScrollArea.Viewport>
                    <ScrollArea.Content padding={'15px'}>
                        <Field.Root style={{paddingBottom: '25px'}} >
                            <Field.Label>
                                <Field.RequiredIndicator />
                                <Text>Project Name</Text>
                            </Field.Label>
                            <Input backgroundColor={'white'} size='md' value={formState.name} onChange={e => {
                                dispatch({type: 'SET_PROJECT_NAME', payload: e.target.value})
                            }} />
                            <Field.ErrorText>
                                <Text>Project name must be longer than one character!</Text>
                            </Field.ErrorText>
                        </Field.Root>

                        <Field.Root style={{paddingBottom: '25px'}} >
                            <Field.Label>
                                <Field.RequiredIndicator />
                                <Text>Description</Text>
                            </Field.Label>
                            <Input backgroundColor={'white'} size='md' value={formState.description} onChange={e => {
                                dispatch({type: 'SET_PROJECT_DESCRIPTION', payload: e.target.value})
                            }} />
                            <Field.ErrorText>
                                <Text>Project name must be longer than one character!</Text>
                            </Field.ErrorText>
                        </Field.Root>

                        <Field.Root style={{paddingBottom: '25px'}}>
                            <Field.Label>
                                <Field.RequiredIndicator />
                                <Text>Project type</Text>
                            </Field.Label>
                            <ProjectTypeDropdown setFormProjectType={(projectType: string) => {
                                dispatch({type: 'SET_PROJECT_TYPE', payload: projectType})
                            }}/>
                        </Field.Root>

                        {formState.projectType !== "" && (
                            <Field.Root style={{paddingBottom: '25px'}}>
                                <Field.Label>
                                    <Field.RequiredIndicator />
                                    <Text>Project status</Text>
                                </Field.Label>
                                <ProjectStatusDropdown projectType={formState.projectType}/>
                            </Field.Root>
                        )}
                        <Flex paddingBottom={'10px'} flexDirection={'row'} justifyContent={'center'} alignItems={'center'}>
                            <Text fontSize={'18px'} fontWeight={'500'} paddingRight={'10px'}>Add team roles for this project</Text>
                            <IconButton onClick={(e) => {
                                dispatch({type: 'ADD_ROLE'})
                            }}>
                                    <LuUserPlus aria-label="Add new role"/>
                            </IconButton>
                        </Flex>
                        {formState.roles.map((r, i) => {
                            return <AddRoleForm index={i} dispatch={dispatch} key={i} role={r} />
                        })}
                        <div className='formButtons'>
                            <Button style={{marginLeft: '5px'}} onClick={createProject}>
                                <Text>Submit</Text>
                            </Button>
                        </div>
                    </ScrollArea.Content>
                </ScrollArea.Viewport>
                <ScrollArea.Scrollbar>
                <ScrollArea.Thumb />
                </ScrollArea.Scrollbar>
                <ScrollArea.Corner />
            </ScrollArea.Root>
        </Flex>

    )
  }
  
export default CreateProjectForm