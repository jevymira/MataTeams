// libraries
import { useContext, useEffect, useState, Dispatch } from 'react'
import { useNavigate } from 'react-router'
import { Container, Editable, Text, Flex, Badge, Wrap, IconButton, Card } from '@chakra-ui/react'
import { LuCheck, LuClock, LuPencil, LuPlus } from 'react-icons/lu'

// context
import { UserContext } from '../../context/auth'

// hooks
import { useGetPendingRequests, useGetUserRoles } from '../../hooks/teams'
import { useUpdateUser } from '../../hooks/profile'

// components
import SkillsDropdown from '../../components/skillsDropdown/SkillsDropdown'

// types
import { UserContextType, Skill } from '../../types'

function Profile() {
    const { firstName, lastName, skills, token, setSkills } = useContext(UserContext) as UserContextType
    const navigate = useNavigate()
    const [ pendingRequests, getRequests ] = useGetPendingRequests(token)
    const [ userRoles, getUserRoles ] = useGetUserRoles(token)
    const [ editFirstName, setEditFirstName ] = useState(firstName)
    const [ editLastName, setEditLastName ] = useState(lastName)
    const [updateUser] = useUpdateUser(editFirstName, editLastName)
    const [isEditingSkills, setIsEditingSkills]= useState(false)

    useEffect(() => {
        getRequests()
        getUserRoles()
    }, [])

    const routeToNewProject = () => {
        navigate('/new')
    }
    

    return (
        <Flex paddingTop={'20px'} flexDirection="column" alignItems={'center'} justifyContent={'center'}>
            <Text fontSize={'26px'} marginBottom={'40px'}>Welcome back, {firstName}!</Text>
            
            <Flex flexDirection='row' justifyContent={'space-between'} alignItems={'center'} width={'500px'}>
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
                <IconButton variant={'subtle'} onClick={() => {
                    setIsEditingSkills(true)
                }}>
                    <LuPencil />
                </IconButton>
            </Flex>
            <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'}>
            {isEditingSkills ? (
                <SkillsDropdown setFormSkills={setSkills} labelText="Select skills"/>
            ) : (
                    <Wrap>
                    {skills?.length > 0 ? (
                        skills.map((skill, index) => (
                            <Badge key={index} mt={2}>{skill.name}</Badge>))
                        ) : (
                            <Text mt={2} color="gray">
                        No skills added yet
                        </Text>)
                        }
                </Wrap>
            )}

            </Flex>

            <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'25px'}>
                <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>Pending Requests</Text>
                <Wrap>
                    {pendingRequests.map(request => {
                        return (
                        <Card.Root  width="240px" variant='outline'>
                        <Card.Body>
                            <Card.Title >
                                <Flex flexDirection={'row'} alignItems={'center'} justifyContent={'space-between'}>{request.projectName}
                                <LuClock /> </Flex>
                            </Card.Title>
                            <Card.Description>
                            {/* <Text>{request.teamName}</Text> */}
                            <Text>{request.projectRoleName}{" developer"}</Text>
                            </Card.Description>
                        </Card.Body>
                        <Card.Footer justifyContent="flex-end">
                            <IconButton padding={'5px'} variant="surface" colorPalette='gray'>Cancel</IconButton>
                        </Card.Footer>
                        </Card.Root>)
                    })}
                </Wrap>
            </Flex>
            
            {(userRoles && userRoles.length > 0) ? (<Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'25px'}>
                <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>My Projects</Text>
                <Wrap>
                    {userRoles.map(role => {
                        return (
                        <Card.Root  width="250px" variant='outline'>
                        <Card.Body>
                            <Card.Title>
                                {role.projectName}
                            </Card.Title>
                            <Card.Description>
                                <Text>{role.roleName}</Text>
                                <Text>{"Pending member requests: 0"}</Text>
                            </Card.Description>
                        </Card.Body>
                        <Card.Footer justifyContent="flex-end">
                            <IconButton colorPalette='gray' variant={'subtle'} padding={'10px'}>
                                <LuPencil />
                                Edit Project
                                </IconButton>
                        </Card.Footer>
                        </Card.Root>)
                    })}
                </Wrap>
            </Flex>): (
                <Flex width='500px' flexDirection={'column'} alignItems={'flex-start'} marginTop={'25px'} >
                    <Text fontWeight={600} fontSize={'20px'} marginBottom={'10px'}>No projects yet!</Text>
                    <IconButton variant={'subtle'} colorPalette={'green'} padding='20px' onClick={routeToNewProject}>
                        <LuPlus />
                        Create a new project
                    </IconButton>
                </Flex>)}
        </Flex>
    )
  }
  
  export default Profile
  