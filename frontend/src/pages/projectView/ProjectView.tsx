// libraries
import { useContext, useEffect, useState, useRef } from 'react'
import { Flex, Box, Text, Button, Spinner, Card, Grid, GridItem } from '@chakra-ui/react'
import { ToastContainer, toast } from 'react-toastify'


// context
import { ProjectsContext } from '../../context/project'
import { UserContext } from '../../context/auth'

// components
import ProjectAdminView from './ProjectAdminView'

// types
import { UserContextType, Project, ProjectsContextType} from '../../types'

// hooks
import { useGetProjectByID } from '../../hooks/projects'

// style 
import './ProjectView.css'
import RoleCard from './RoleCard'
import { useParams, Link, useNavigate } from 'react-router'
import { LuClipboardList, LuTelescope, LuUser, LuExternalLink, LuArrowRight } from 'react-icons/lu'


function ProjectView() {
    let { id } = useParams()
    let projectRef = useRef(id)
    const { viewProjectId, projectLeaderId, setViewProjectId} = useContext(ProjectsContext) as ProjectsContextType
    const { token, userID } = useContext(UserContext) as UserContextType
    const [project, getProject] = useGetProjectByID(id ? id : '', token)
    const [requestedRole, setRequestedRole ] = useState(false)
    const navigate = useNavigate()

    const routeToNewProject = () => {
        navigate('/new')
    }
    /*
    const { login } = useParams();
    const loginRef = useRef(login);
    ...
    useEffect(() => {
    if(login !== loginRef.current){
        getUser(login);
        getUserRepos(login);
        loginRef.current = login;
    }
    }, [login, loginRef]);
    */

    useEffect(() => {
            getProject()
    }, [])

    useEffect(() => {
        if (id !== projectRef.current) {
            getProject()
            projectRef.current = id;
        }
    }, [id, projectRef])

    const onToast = () => {
        toast("Sent request to join project role!")
    }

    if (!project) {
        return <Spinner />
    }


    if (project.ownerId == userID) {
        return <ProjectAdminView project={project} />
    }

    return (<Flex width='100%' justifyContent={'center'} flexDirection={'row'}>
     <ToastContainer theme={"dark"} closeOnClick={true}/>
    {project ? (
        <Box textAlign={'left'} backgroundColor={'white'} marginTop={'25px'} padding={'25px'} borderRadius={'20px'}>
            <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontWeight={750} fontSize={'26px'} paddingTop={'20px'} >
                {project.name}
            </Text>
            <Flex>
                <Box>
                    <Text fontWeight={650} fontSize={'18px'}>About this project</Text>
                    <Text width={"600px"}>{project.description}</Text>
                    <Text marginTop={'50px'} fontWeight={650} fontSize={'18px'}>{project.teams.length > 0 ? project.teams[0].name : ''}</Text>
                    <Grid templateColumns="repeat(2, 1fr)" gap="6">

                        {project.teams && project.teams.length > 0 && project.roles.map(r => {
                            return (<GridItem> <RoleCard role={r} teamID={project.teams[0].id} onToast={onToast}/> </GridItem>)
                        })}
                    </Grid>
                </Box>
                <Box width={'200px'}>
                    <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuExternalLink />
                        <Link to={`/profile/${projectLeaderId}`}>
                            <Text textDecoration={'underline'} paddingLeft={'10px'}>Project Lead Profile</Text> 
                        </Link>
                    </Flex>
                    { project.copyOf && <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuExternalLink />
                        <Link to={`/project/${project.copyOf.toUpperCase()}`}>
                            <Text textDecoration={'underline'} paddingLeft={'10px'}>Original Project</Text> 
                        </Link> 
                    </Flex>
                    }
                    <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuClipboardList />
                        <Text paddingLeft={'10px'}>Type: {project.type}</Text>
                    </Flex>
                    <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuTelescope />
                        <Text paddingLeft={'10px'}>Status: {project.status}</Text>
                    </Flex>
                    <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuUser />
                        <Text paddingLeft={'10px'}>{project.roles.length} open roles</Text>
                    </Flex>
                    <Flex>
                        {project.canCopy && <Button size="xs" onClick={() => {
                            // first set project ID in context
                            setViewProjectId(project.id)
                            console.log(project.id)
                            console.log(viewProjectId)
                            // then navigate to route
                            navigate('/copy')
                        }}>Copy this Project <LuArrowRight/></Button>}
                    </Flex>
                </Box>
            </Flex>
        </Box>
    ) : <Spinner />}
    </Flex>)
  }
  
  export default ProjectView
  