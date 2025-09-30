import { useEffect, useState } from 'react';
import ProjectView from '../../components/project/ProjectView'
import { useGetAllProjects } from '../../hooks/Projects';

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
              <ProjectView project={p}/>
          )
      })}
    </div>
  );
}
  
  export default Projects;
  