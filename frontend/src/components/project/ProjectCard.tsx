
// libraries
import { Link } from 'react-router'
import { Box, Badge } from '@chakra-ui/react'

// types
import {Project, ProjectsContextType} from '../../types'

// utilities
import { truncateText } from '../../utilities/truncateText'

// style
import './ProjectCard.css'
import { ProjectsContext } from '../../context/project'
import { useContext } from 'react'

type ProjectProps = {
    project: Project
}

function ProjectCard({project} : ProjectProps) {
    const { setViewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    return (
        <Box 
            bg={'#e3e0de'} 
            padding={'15px'}
            m={'20px'} 
            borderRadius={'5px'} 
            _hover={{bg: '#d1d1d1'}}>
            <Link to={`/project/view`} className='projectLink' onClick={() => {
                setViewProjectId(project.id)
            }}>{project.name} </Link>
            <p>{truncateText(project.description)}</p>
            <p></p>
        </Box>
    )
  }
  
  export default ProjectCard
  