// libraries
import { useContext, useEffect, useState } from 'react'
import { Flex, Box, Text, Button, Spinner, Card, Grid, GridItem } from '@chakra-ui/react'

// context
import { ProjectsContext } from '../../context/project'
import { UserContext } from '../../context/auth'

// types
import { UserContextType, Project, ProjectsContextType} from '../../types'

// hooks
import { useGetProjectByID } from '../../hooks/projects'

// style 
import './ProjectView.css'
import RoleCard from './RoleCard'


function ProjectView() {
    const { viewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    const { token } = useContext(UserContext) as UserContextType
    const [project, getProject] = useGetProjectByID(viewProjectId, token)
    const [requestedRole, setRequestedRole ] = useState(false)

    useEffect(() => {
        getProject()
    }, [])

    return (<Flex width='100%' justifyContent={'center'} flexDirection={'row'}>
    {project ? (
        <Box textAlign={'left'} backgroundColor={'white'} marginTop={'25px'} padding={'20px'} borderRadius={'20px'}>
            <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontWeight={750} fontSize={'26px'} paddingTop={'20px'} >
                {project.name}
            </Text>
            <Flex>
                <Box>
                    <Text fontWeight={650} fontSize={'18px'}>About this project</Text>
                    <Text width={"600px"}>{project.description}</Text>
                    <Text marginTop={'50px'} fontWeight={650} fontSize={'18px'}>{project.teams.length > 0 ? project.teams[0].name : ''}</Text>
                    <Grid templateColumns="repeat(2, 1fr)" gap="6">

                        {project.roles.map(r => {
                            return (<GridItem> <RoleCard role={r} /> </GridItem>)
                        })}
                    </Grid>
                </Box>
                <Box>
                    <p>{project.type}</p>
                    <p>{project.status}</p>
                </Box>
            </Flex>
        </Box>
    ) : <Spinner />}
    </Flex>)
  }
  
  export default ProjectView
  