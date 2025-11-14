
// libraries
import { Link } from 'react-router'
import { Box, Badge } from '@chakra-ui/react'

// types
import {Project} from '../../types'

// utilities
import { truncateText } from '../../utilities/truncateText'

// style
import './ProjectCard.css'

type ProjectProps = {
    project: Project
}

function ProjectCard({project} : ProjectProps) {
    return (
        <Box 
            bg={'#e3e0de'} 
            padding={'15px'}
            m={'20px'} 
            borderRadius={'5px'} 
            _hover={{bg: '#d1d1d1'}}>
            <Link to={`/project/${project.id}`} className='projectLink'>{project.name} </Link>
            <p>{truncateText(project.description)}</p>
            <p></p>
        </Box>
    )
  }
  
  export default ProjectCard
  