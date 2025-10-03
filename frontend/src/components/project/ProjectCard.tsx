
// libraries
import { Link } from 'react-router'

// types
import {Project} from '../../types'

// style
import './ProjectCard.css'

type ProjectProps = {
    project: Project
}

function ProjectCard({project} : ProjectProps) {
    
    return (
        <div className='projectContainer'>
            <Link to={`/project/${project.id}`} className='projectLink'>{project.name} </Link>
            <p>{project.description}</p>
            <p></p>
        </div>
    )
  }
  
  export default ProjectCard
  