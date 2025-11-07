// libraries
import { useEffect } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Button } from '@chakra-ui/react'
import { LuPlus } from "react-icons/lu"

// components
import ProjectCard from '../../components/project/ProjectCard'
import Searchbar from '../../components/searchbar/Searchbar'
import { Sidebar } from '../../components/sidebar/Sidebar'

// hooks
import { useGetAllProjects } from '../../hooks/projects'

// style
import './Projects.css'

function Projects() {
  const [projects, getProjects] = useGetAllProjects()
  const navigate = useNavigate()

  useEffect(() => {
      getProjects()
  }, [])

  const routeToNewProject = () => {
    navigate('/new')
  }

  return (
  <div className='projectsWrapper'>
    <Sidebar />
    <div className='projectsPageHeader'>
      <Searchbar />
      <Button aria-label="Create new Project" onClick={routeToNewProject}>
        <LuPlus /> New Project
      </Button>
      {/* <Link to="/new">Create new Project </Link> */}
    </div>
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
  