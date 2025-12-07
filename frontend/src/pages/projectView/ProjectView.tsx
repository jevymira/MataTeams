// libraries
import { useContext, useEffect } from 'react'
import { Flex, Box, Text } from '@chakra-ui/react'

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

    return (<Flex width='100%' justifyContent={'center'}>
    {project ? (
        <Box>
            <Text>{project.name} </Text>
            <p>{project.description}</p>
            <p>{project.type}</p>
            <p>{project.status}</p>
            <p>{project.teams.length > 0 ? project.teams[0].teamName : ''}</p>
        </Box>
    ) : <div>Loading...</div>}
    </Flex>)
  }
  
  export default ProjectView
  