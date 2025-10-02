// libraries
import { useParams } from 'react-router'

// types
import {Project} from '../../types'

// components
import { Sidebar } from '../../components/sidebar/Sidebar'

// hooks
import { useEffect } from 'react'
import { useGetProjectByID } from '../../hooks/Projects'

// style 
import './ProjectView.css'


function ProjectView() {
    const { id } = useParams()
    console.log(id)
    const [project, getProject] = useGetProjectByID(id as string)

    useEffect(() => {
        getProject()
        console.log("got project??")
        console.log(project)
    }, [])

    console.log("got project??")
    console.log(project)

    return (<div className='projectViewWrapper'>
    <Sidebar />
    {project ? (
        <div className=''>
            <h1>{project.name} </h1>
            <p>{project.description}</p>
            <p></p>
        </div>
    ) : <div>Loading...</div>}
    </div>)
  }
  
  export default ProjectView
  