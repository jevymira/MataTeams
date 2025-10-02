import { useParams } from 'react-router'
import {Project} from '../../types'
import { useEffect } from 'react'
import { useGetProjectByID } from '../../hooks/Projects'


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

    return project ? (
        <div className=''>
            <h1>{project.name} </h1>
            <p>{project.description}</p>
            <p></p>
        </div>
    ) : <div>Loading...</div>
  }
  
  export default ProjectView
  