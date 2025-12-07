// libraries
import { useContext, useEffect } from 'react'
import { Flex, Box, Text, Button } from '@chakra-ui/react'

// context
import { ProjectsContext } from '../../context/project'
import { UserContext } from '../../context/auth'

// types
import { UserContextType, Project, ProjectsContextType} from '../../types'

// hooks
import { useGetProjectByID } from '../../hooks/projects'

// style 
import './ProjectView.css'


function ProjectView() {
    const { viewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    const { token } = useContext(UserContext) as UserContextType
    const [project, getProject] = useGetProjectByID(viewProjectId, token)

    useEffect(() => {
        getProject()
    }, [])

    return (<Flex width='100%' justifyContent={'center'} flexDirection={'row'}>
    {project ? (
        <Box textAlign={'left'}>
            <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontWeight={750} fontSize={'26px'} paddingTop={'20px'} >
                {project.name}
            </Text>
            <Text  fontWeight={650} fontSize={'18px'}>About this project</Text>
            <Text width={"800px"}>{project.description}</Text>
            <p>{project.type}</p>
            <p>{project.status}</p>
            <Text></Text>
            <p>{project.teams.length > 0 ? project.teams[0].name : ''}</p>
        </Box>
    ) : <div>Loading...</div>}
    <Box borderRadius={'20px'} borderWidth={'1px'} borderColor={'gray'}>
        <Button>Request to Join</Button>
    </Box>
    </Flex>)
  }
  
  export default ProjectView
  