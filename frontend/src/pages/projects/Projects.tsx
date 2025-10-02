import { useEffect, useState } from 'react'
import ProjectCard from '../../components/project/ProjectCard'
import { useGetAllProjects } from '../../hooks/Projects'

function Projects() {
  const [projects, getProjects] = useGetAllProjects()

  useEffect(() => {
    getProjects()
  }, [])

  return (
    <div className="">
      {projects.map((p) => {
          console.log(p)
          return (
              <ProjectCard project={p}/>
          )
      })}
    </div>
  )
}
  
  export default Projects;
  