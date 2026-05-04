// libraries
import { useState, useEffect, Dispatch, useReducer, useContext } from 'react'
import { Container, Field, Text, Input, Button, IconButton, Flex, ScrollArea, CheckboxCard, Center, Spinner } from '@chakra-ui/react'
import { LuUserPlus } from "react-icons/lu"
import { useNavigate } from 'react-router'

// hooks
import { useCreateProject, useGetProjectByID } from '../../hooks/projects'

// context
import { UserContext } from '../../context/auth'
import { ProjectsContext } from '../../context/project'

// components
import ProjectTypeDropdown from '../projectTypeDropdown/ProjectTypeDropdown'
import ProjectStatusDropdown from '../projectStatusDropdown/ProjectStatusDropdown'

// types
import { CreateProject, UserContextType, ProjectsContextType } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'
import { createProjectFormReducer, defaultCreateProject } from '../../reducers/createProjectForm'


function CopyProjectForm() {
    const navigate = useNavigate()
    const { token } = useContext(UserContext) as UserContextType
    const { viewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    const [project, getProject] = useGetProjectByID(viewProjectId, token)
    const [formState, dispatch] = useReducer(createProjectFormReducer, defaultCreateProject)
    const [createProject] = useCreateProject(formState, token)
    const [changedName, setChangedName] = useState(false)
    const [changedDescription, setChangedDescription] = useState(false)

     useEffect(() => {
        if (!viewProjectId) {
            navigate('/')
        } else {
            getProject()
        }

        getProject()
    }, [])
 
    return (
        <Flex alignItems={'center'} flexDirection={'column'}>
            {project ? (
            <>
            <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontSize={'26px'} padding={'20px'} textAlign={'center'}>
                Copy Project
            </Text>
            <ScrollArea.Root maxWidth={500} paddingTop={'10px'} height={'80vh'}>
                <ScrollArea.Viewport>
                    <ScrollArea.Content padding={'15px'}>
                        <Field.Root style={{paddingBottom: '25px'}} >
                            <Field.Label>
                                <Field.RequiredIndicator />
                                <Text>Project Name</Text>
                            </Field.Label>
                            <Input backgroundColor={'white'} size='md' value={changedName ? formState.name : project.name} onChange={e => {
                                setChangedName(true)
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
                            <Input backgroundColor={'white'} size='md' value={changedDescription ? formState.description : project.description} onChange={e => {
                                setChangedDescription(true)
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

                        <CheckboxCard.Root variant='outline' colorPalette='green' className='checkboxcard' 
                                        size='lg' border='none' boxShadow='none' alignItems='center'
                                        onCheckedChange={e => {
                                            dispatch({type: 'CAN_COPY', payload: e.checked === true})}}>
                            <CheckboxCard.HiddenInput />
                            <CheckboxCard.Control>
                                <CheckboxCard.Label fontSize={'18px'} fontWeight={'500'}>
                                    Allow this project to be copied?
                                </CheckboxCard.Label>
                                <CheckboxCard.Indicator />
                            </CheckboxCard.Control>
                        </CheckboxCard.Root>

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
                            return <AddRoleForm index={i} dispatch={dispatch} key={i} role={r} projectType={formState.projectType} />
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
            </>
            ) : <Spinner />}
            
        </Flex>

    )
  }
  
export default CopyProjectForm