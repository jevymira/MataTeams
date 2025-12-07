
// libraries
import { Link } from 'react-router'
import { Box, Text, Badge, Flex } from '@chakra-ui/react'

// types
import {Project, ProjectsContextType} from '../../types'

// utilities
import { truncateText } from '../../utilities/truncateText'

// style
import './ProjectCard.css'
import { ProjectsContext } from '../../context/project'
import { useContext } from 'react'
import { GoStarFill } from "react-icons/go";

type ProjectProps = {
    project: Project
    isGoodMatch: boolean
}

function ProjectCard({project, isGoodMatch} : ProjectProps) {
    const { setViewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    return (
        <Box className='projectContainer'>
            <Link to={`/project/view`} className='projectLink' onClick={() => {
                setViewProjectId(project.id)
            }}>{project.name} </Link>
            <p>{truncateText(project.description)}</p>
            {isGoodMatch && (
                <Flex flexDirection={'row'} alignItems={'center'}>
                <GoStarFill color='gold'/>
                <Text fontSize={'16px'}>{"Recommended for you"}</Text>
                </Flex>
                )}
        </Box>
    )
  }
  
  export default ProjectCard
  