// libraries
import { useContext, useEffect, useState, Dispatch } from 'react'
import { Link } from 'react-router'
import { useNavigate } from 'react-router'
import { ScrollArea, Editable, Text, Flex, Badge, Wrap, IconButton, Card, Container } from '@chakra-ui/react'
import { LuCheck, LuClock, LuPencil, LuPlus } from 'react-icons/lu'

// context
import { UserContext } from '../../context/auth'

// hooks
import { useGetPendingRequests, useGetUserRoles } from '../../hooks/teams'
import { useGetProjectsForUser } from '../../hooks/projects'
import { useUpdateUser } from '../../hooks/profile'

// components
import SkillsDropdown from '../../components/skillsDropdown/SkillsDropdown'

// types
import { UserContextType, Skill } from '../../types'

function Profile() {
    const { firstName, lastName, skills, token, userID, setSkills } = useContext(UserContext) as UserContextType
    const navigate = useNavigate()
    const [ teamRequests, getRequests ] = useGetPendingRequests(token)
    const [ userProjects, getProjectsForUser] = useGetProjectsForUser(token, userID)
    const [ userRoles, getUserRoles ] = useGetUserRoles(token)
    const [ editFirstName, setEditFirstName ] = useState(firstName)
    const [ editLastName, setEditLastName ] = useState(lastName)
    const [skillsToUpdate, setSkillsToUpdate] = useState<Skill[]>([])
    const [updateUser] = useUpdateUser(editFirstName, editLastName, skillsToUpdate)
    const [isEditingSkills, setIsEditingSkills]= useState(false)

    useEffect(() => {
        getRequests()
        getProjectsForUser()
        getUserRoles()
    }, [])

    const routeToNewProject = () => {
        navigate('/new')
    }
    
    const setProfileSkills = (skills: Skill[]) => {
        setSkillsToUpdate(skills)
        //updateUser()
    }

    return (
        <Flex paddingTop={'20px'} flexDirection="column" alignItems={'center'} justifyContent={'center'}>
            <ScrollArea.Root maxWidth={700} paddingTop={'10px'} height={'80vh'}>
                <ScrollArea.Viewport>
                    <ScrollArea.Content padding={'15px'}>
                        <Text fontSize={'26px'} marginBottom={'40px'}>Welcome back, {firstName}!</Text>

                        <Flex flexDirection='row' justifyContent={'space-between'} alignItems={'center'} width={'600px'}>
                            <Text fontSize={'20px'} fontWeight={600}>Profile Details</Text>
                        </Flex>
                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} paddingBottom={'20px'}>
                            <Editable.Root textAlign="start" value={editFirstName}
                                onValueChange={(e) => {
                                    setEditFirstName(e.value)
                                    updateUser()
                                }}>
                                <Editable.Preview />
                                <Editable.Input />
                            </Editable.Root>
                            {/* <IconButton onClick={() => {
                                updateUser()
                            }}>Save</IconButton> */}
                        </Flex>

                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} paddingBottom={'20px'}>
                            <Editable.Root textAlign="start" value={editLastName}
                                onValueChange={(e) => {
                                    setEditLastName(e.value)
                                    updateUser()
                                }}>
                                <Editable.Preview />
                                <Editable.Input />
                            </Editable.Root>
                        </Flex>
                        
                        <Flex flexDirection='row' justifyContent={'space-between'} alignItems={'center'} width={'500px'}>
                            <Text fontSize={'20px'} fontWeight={600}>Skills</Text>
                            {isEditingSkills ? 
                            <IconButton onClick={() => {
                                updateUser()
                                setIsEditingSkills(false)
                            }}>
                                <LuCheck />
                            </IconButton>
                            :
                                <IconButton variant={'subtle'} onClick={() => {
                                    setIsEditingSkills(true)
                                }}>
                                    <LuPencil />
                                </IconButton>
                            }
                        </Flex>
                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'}>
                        {isEditingSkills ? (
                            <SkillsDropdown setFormSkills={setProfileSkills} labelText="Select skills" defaultSelectedSkills={skills}/>
                        ) : (
                                <Wrap>
                                {skills && skills.length > 0 ? (
                                    skills.map((skill, index) => {
                                        return (<Badge key={index} mt={2}>{skill.name}</Badge>)})
                                    ) : (
                                        <Text mt={2} color="gray">
                                    No skills added yet
                                    </Text>)
                                    }
                            </Wrap>
                        )}

                        </Flex>

                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'35px'}>
                            <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>My Projects</Text>
                            <Wrap>
                                {userProjects.map(project => {
                                    return (
                                    <Card.Root  width="240px" variant='outline'>
                                    <Card.Body>
                                        <Card.Title >
                                            <Flex flexDirection={'row'} alignItems={'center'} justifyContent={'space-between'}>
                                                <Link to={`/project/${project.id}`}> {project.name} </Link> </Flex>
                                        </Card.Title>
                                        <Card.Description>
                                        {/* <Text>{request.teamName}</Text> */}
                                        {/* <Text>{project.projectRoleName}{" developer"}</Text> */}
                                        </Card.Description>
                                    </Card.Body>
                                    <Card.Footer justifyContent="flex-end">
                                        <IconButton padding={'5px'} variant="surface" colorPalette='gray' onClick={() => {
                                            navigate(`/project/${project.id}`)
                                        }}>Manage Project</IconButton>
                                    </Card.Footer>
                                    </Card.Root>)
                                })}
                            </Wrap>
                                <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'25px'} >
                                <IconButton variant={'subtle'} colorPalette={'green'} padding='20px' onClick={routeToNewProject}>
                                    <LuPlus />
                                    Create a new project
                                </IconButton>
                            </Flex>
                        </Flex>

                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'35px'}>
                            <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>My Team Roles</Text>
                            <Wrap>
                                {teamRequests.map(request => {
                                    return request.status != 'Pending' && (
                                    <Card.Root  width="240px" variant='outline'>
                                    <Card.Body>
                                        <Card.Title >
                                            <Flex flexDirection={'row'} alignItems={'center'} justifyContent={'space-between'}>
                                                <Link to={`/project/${request.projectId}`}> {request.projectRoleName} developer </Link>
                                             </Flex>
                                        </Card.Title>
                                        <Card.Description>
                                        {/* <Text>{request.teamName}</Text> */}
                                        <Text>{request.projectName}</Text>
                                        </Card.Description>
                                    </Card.Body>
                                    </Card.Root>)
                                })}
                            </Wrap>
                        </Flex>


                        <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'35px'}>
                            <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>Pending Requests</Text>
                            <Wrap>
                                {teamRequests.map(request => {
                                    return request.status == 'Pending' && (
                                    <Card.Root  width="240px" variant='outline'>
                                    <Card.Body>
                                        <Card.Title >
                                            <Flex flexDirection={'row'} alignItems={'center'} justifyContent={'space-between'}>
                                                <Link to={`/project/${request.projectId}`}> {request.projectName} </Link>
                                             </Flex>
                                        </Card.Title>
                                        <Card.Description>
                                        {/* <Text>{request.teamName}</Text> */}
                                        <Text>{request.projectRoleName}{" developer"}</Text>
                                        
                                        </Card.Description>
                                    </Card.Body>
                                    <Card.Footer justifyContent="flex-end">
                                        {request.status == 'Pending' ? <>{"Pending "}<LuClock /></> : <></>} 
                                        <IconButton padding={'5px'} variant="surface" colorPalette='gray'>Cancel</IconButton>
                                    </Card.Footer>
                                    </Card.Root>)
                                })}
                            </Wrap>
                        </Flex>

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
  
  export default Profile
  