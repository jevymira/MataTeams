// libraries
import { useEffect, useContext } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Flex, ScrollArea, Stack } from '@chakra-ui/react'

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
import { LuChevronLeft, LuChevronRight } from 'react-icons/lu'
import { Paginate } from '../../components/pagination/Paginate'

function Projects() {
  const { token } = useContext(AuthContext) as AuthContextType
  const [projects, getProjects] = useGetAllProjects(token)
  const navigate = useNavigate()

  useEffect(() => {
      getProjects()
  }, [])

  return (
  <Flex 
    direction={'row'} 
    justifyContent={'flex-start'} 
    alignItems={'flex-start'}>
    <Sidebar />
    <Flex direction={'column'} alignItems={'center'}>
      <div className='projectsPageHeader'>
        <Searchbar />
      </div>
      {!projects || projects.length < 1? <div>Loading...</div> : (
        <ScrollArea.Root height="73vh" width={'600px'} marginTop={'10px'}>
          <ScrollArea.Viewport>
            <ScrollArea.Content>
              <Stack>
                {projects.map((p, i) => {
                  return (
                    <ProjectCard project={p} isGoodMatch={(i < 2)}/>
                  )
                })}
              </Stack>
            </ScrollArea.Content>
          </ScrollArea.Viewport>
          <ScrollArea.Scrollbar>
            <ScrollArea.Thumb />
          </ScrollArea.Scrollbar>
          <ScrollArea.Corner />
        </ScrollArea.Root>
      )}
      <Paginate />
      </Flex>
    </Flex>
  )
}
  
  export default Projects;
  