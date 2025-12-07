// libraries
import { useEffect, useContext, useState } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Flex, ScrollArea, Stack } from '@chakra-ui/react'

// context
import { UserContext } from '../../context/auth'

//types
import { UserContextType, InputItem } from '../../types'

// components
import ProjectCard from '../../components/project/ProjectCard'
import Searchbar from '../../components/searchbar/Searchbar'
import { Sidebar } from '../../components/sidebar/Sidebar'

// hooks
import { useGetAllProjects, useGetRecommendedProjects } from '../../hooks/projects'

// style
import './Projects.css'
import { LuChevronLeft, LuChevronRight } from 'react-icons/lu'
import { Paginate } from '../../components/pagination/Paginate'
import { sortProjects } from '../../utilities/sortFilterProjects'

function Projects() {
  const { token } = useContext(UserContext) as UserContextType
  const [projects, getProjects] = useGetRecommendedProjects(token)
  const [sortBy, setSortBy] = useState<string[]>(["rec"])

  useEffect(() => {
      getProjects()
  }, [])

  const skillItems: Array<InputItem> = [
  {
    value: "8762D340-5139-4B1F-6B6A-08DE347F2135",
    label: "Java"
  },
  {
    value: "42CC3186-C06A-474E-6B6B-08DE347F2135",
    label: "JavaScript"
  },
  {
    value: "793F7776-5F6B-4828-6B6C-08DE347F2135",
    label: "React"
  },
  {
    value: "5A67A4EF-5B74-4539-6B6D-08DE347F2135",
    label: "Express"
  }
]

const projectStatusItems = [
    {
      value: "Draft",
      label: "Draft"
    },
    {
      value: "Planning",
      label: "Planning"
    },
    {
      value: "Active",
      label: "Active"
    },
    {
      value: "Completed",
      label: "Completed"
    },
    {
      value: "Closed",
      label: "Closed"
    },
  ]

  const domainItems = [
    {
      value: "Computer Science",
      label: "Computer Science"
    },
    {
      value: "Engineering",
      label: "Engineering"
    },
    {
      value: "Machine learning",
      label: "Machine learning"
    },
    {
      value: "Research",
      label: "Research"
    },
    {
      value: "Film",
      label: "Film"
    },
    {
      value: "Business",
      label: "Business"
    },
    {
      value: "Robotics",
      label: "Robotics"
    },
    {
      value: "Humanities",
      label: "Humanities"
    },
  ]

  return (
  <Flex 
    className='projectsWrapper'
    direction={'row'} 
    justifyContent={'center'} 
    maxWidth={'70%'}
    alignItems={'flex-start'}>
    <Sidebar 
      skillItems={skillItems} 
      domainItems={domainItems} 
      projectStatusItems={projectStatusItems}/>

    <Flex direction={'column'} alignItems={'center'}>
      <div className='projectsPageHeader'>
        <Searchbar 
          skillItems={skillItems} 
          domainItems={domainItems} 
          projectStatusItems={projectStatusItems}
          sortBy={sortBy} 
          setSortBy={setSortBy}/>
      </div>
      {!projects || projects.length < 1 ? <div>Loading...</div> : (
        <ScrollArea.Root height="73vh" width={'600px'} marginTop={'10px'}>
          <ScrollArea.Viewport>
            <ScrollArea.Content>
              <Stack>
                {sortProjects(projects, sortBy[0]).map((p, i) => {
                  return (
                    <ProjectCard project={p} isGoodMatch={((i < 2) && sortBy[0]=='rec')}/>
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
  