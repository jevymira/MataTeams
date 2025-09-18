import {ProjectObj} from '../../types'
import './Project.css'

type ProjectProps = {
    project: ProjectObj
}

function Project({project} : ProjectProps) {

    return (
        <div className='projectContainer'>
            <h1>{project.title} </h1>
            <p>{project.description}</p>
        </div>
    )
  }
  
  export default Project;
  