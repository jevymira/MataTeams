// libraries
import { useEffect, useContext } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Button, Flex, Container} from '@chakra-ui/react'
import { LuPlus } from "react-icons/lu"

// context
import { AuthContext } from '../../context/auth'

//types
import { AuthContextType } from '../../types'

// components
import ProjectCard from '../../components/project/ProjectCard'
import Searchbar from '../../components/searchbar/Searchbar'
import { Sidebar } from '../../components/sidebar/Sidebar'

// hooks
import { useGetAllProjects } from '../../hooks/projects'

// style
import './Projects.css'

function Projects() {
  const { token } = useContext(AuthContext) as AuthContextType
  const [projects, getProjects] = useGetAllProjects(token)
  const navigate = useNavigate()

  useEffect(() => {
      getProjects()
  }, [])

  const routeToNewProject = () => {
    navigate('/new')
  }

  return (
  <Flex direction={'row'}>
    <Sidebar />
    <Flex direction={'column'}>
      <div className='projectsPageHeader'>
        <Searchbar />
        <Button aria-label="Create new Project" onClick={routeToNewProject}>
          <LuPlus /> New Project
        </Button>
        {/* <Link to="/new">Create new Project </Link> */}
      </div>
      {!projects || projects.length < 1? <div>Loading...</div> : (
        <Container paddingLeft={'120px'}>
          {projects.map((p) => {
            return (
              <ProjectCard project={p}/>
            )
          })}
        </Container>
      )}
    </Flex>
    </Flex>)
}
  
  export default Projects;
  