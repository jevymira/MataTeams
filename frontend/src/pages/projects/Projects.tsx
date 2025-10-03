import { useEffect, useState } from 'react'
import ProjectCard from '../../components/project/ProjectCard'
import { useGetAllProjects } from '../../hooks/Projects'
import { Sidebar } from '../../components/sidebar/Sidebar'
import './Projects.css'

function Projects() {
  const [projects, getProjects] = useGetAllProjects()

  useEffect(() => {
      getProjects()
  }, [])

  return (
  <div className='projectsWrapper'>
    <Sidebar />
    {projects === null || projects.length < 1? <div>Loading...</div> : (
      <div className="">
        {projects.map((p) => {
          return (
            <ProjectCard project={p}/>
          )
        })}
      </div>
    )}
    </div>)
}
  
  export default Projects;
  