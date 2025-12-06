// libraries
import { useEffect, useContext } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Button, Flex, Container, ScrollArea, Stack, Pagination, ButtonGroup, IconButton} from '@chakra-ui/react'

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
    <Flex direction={'column'}>
      <div className='projectsPageHeader'>
        <Searchbar />
      </div>
      {!projects || projects.length < 1? <div>Loading...</div> : (
        <ScrollArea.Root paddingLeft={'120px'} height="80vh">
          <ScrollArea.Viewport>
            <ScrollArea.Content>
              <Stack>
                {projects.map((p) => {
                  return (
                    <ProjectCard project={p}/>
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
    <Pagination.Root count={20} pageSize={2} defaultPage={1}>
      <ButtonGroup variant="ghost" size="sm">
        <Pagination.PrevTrigger asChild>
          <IconButton>
            <LuChevronLeft />
          </IconButton>
        </Pagination.PrevTrigger>

        <Pagination.Items
          render={(page) => (
            <IconButton variant={{ base: "ghost", _selected: "outline" }}>
              {page.value}
            </IconButton>
          )}
        />

        <Pagination.NextTrigger asChild>
          <IconButton>
            <LuChevronRight />
          </IconButton>
        </Pagination.NextTrigger>
      </ButtonGroup>
    </Pagination.Root>
      </Flex>
    </Flex>
    )
}
  
  export default Projects;
  