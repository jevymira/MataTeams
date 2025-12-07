// libraries
import { useEffect, useContext, useState } from 'react'
import { useNavigate } from 'react-router'
import { Link } from 'react-router'
import { Flex, ScrollArea, Spinner, Stack } from '@chakra-ui/react'

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
  const [filterByVacancies, setFilterByVacancies] = useState(false)

  useEffect(() => {
      getProjects()
  }, [])

  

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
      projectStatusItems={projectStatusItems}
      filterByVacancies={filterByVacancies}
      setFilterByVacancies={setFilterByVacancies}
      />

    <Flex direction={'column'} alignItems={'center'}>
      <div className='projectsPageHeader'>
        <Searchbar 
          skillItems={skillItems} 
          domainItems={domainItems} 
          projectStatusItems={projectStatusItems}
          sortBy={sortBy} 
          setSortBy={setSortBy}/>
      </div>
      {!projects || projects.length < 1 ?<Spinner /> : (
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
  },
  {
    value: "B467AB51-444A-442A-9575-08DE3562308B",
    label: "Python"
  },
  {
    value: "688A12E0-A8AB-4343-9576-08DE3562308B",
    label: "C++"
  },
  {
    value: "0995765D-CBFE-4C95-9577-08DE3562308B",
    label: "SQL"
  },
  {
    value: "335193FA-8261-4212-9578-08DE3562308B",
    label: "Angular"
  },
  {
    value: "DE1BAA73-CA56-4A64-9579-08DE3562308B",
    label: "Spring"
  },
  {
    value: "44331C30-4B5E-4427-957A-08DE3562308B",
    label: "Flask"
  },
  {
    value: "3D11F019-7E2E-4EC5-957B-08DE3562308B",
    label: "Docker"
  },
  {
    value: "9D325071-D5CC-4A22-957C-08DE3562308B",
    label: "Kubernetes"
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
      value: "Animation",
      label: "Animation"
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
  
export default Projects;
  