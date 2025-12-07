// libraries
import { useContext, useEffect } from 'react'

// context
import { ProjectsContext } from '../../context/project'
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType, Project, ProjectsContextType} from '../../types'

// components
import { Sidebar } from '../../components/sidebar/Sidebar'

// hooks
import { useGetProjectByID } from '../../hooks/projects'

// style 
import './ProjectView.css'


function ProjectView() {
    const { viewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    const { token } = useContext(AuthContext) as AuthContextType
    const [project, getProject] = useGetProjectByID(viewProjectId, token)

    useEffect(() => {
        getProject()
    }, [])

    return (<div className='projectViewWrapper'>
    {project ? (
        <div className=''>
            <h1>{project.name} </h1>
            <p>{project.description}</p>
            <p>{project.type}</p>
            <p>{project.status}</p>
        </div>
    ) : <div>Loading...</div>}
    </div>)
  }
  
  export default ProjectView
  