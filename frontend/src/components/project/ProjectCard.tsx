import {Project} from '../../types'
import './Project.css'

type ProjectProps = {
    project: Project
}

function ProjectCard({project} : ProjectProps) {

    return (
        <div className='projectContainer'>
            <h1>{project.name} </h1>
            <p>{project.description}</p>
            <p></p>
        </div>
    )
  }
  
  export default ProjectCard
  