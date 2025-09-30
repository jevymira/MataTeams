import {Project} from '../../types'
import './Project.css'

type ProjectProps = {
    project: Project
}

function ProjectView({project} : ProjectProps) {

    return (
        <div className='projectContainer'>
            <h1>{project.name} </h1>
            <p>{project.description}</p>
        </div>
    )
  }
  
  export default ProjectView
  