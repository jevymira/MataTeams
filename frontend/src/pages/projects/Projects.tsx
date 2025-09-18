import Project from '../../components/project/Project'
import {ProjectObj} from '../../types'

type ProjectsProps = {
    projects: ProjectObj[]
}

function Projects({projects} : ProjectsProps) {

    return (
      <div className="">
        {projects.map((p) => {
            return (
                <Project project={p}/>
            )
        })}
      </div>
    );
  }
  
  export default Projects;
  