// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'
import { Box, Checkbox, ScrollArea, Stack } from '@chakra-ui/react'
import FilterSection from './FilterSection'
import { InputItem } from '../../types'

  type SidebarProps = {
    skillItems: InputItem[]
    domainItems: InputItem[]
    projectStatusItems: InputItem[]
  }

export const Sidebar = ({skillItems, projectStatusItems, domainItems}: SidebarProps) => {
  return(
    <ScrollArea.Root className='sidebarContainer' height="80vh" maxWidth={'250px'} paddingLeft={'2vw'}>
      <ScrollArea.Viewport>
        <ScrollArea.Content>
          <FilterSection items={projectStatusItems} sectionLabel='Filter By Project Status'/>
          <FilterSection items={skillItems} sectionLabel='Filter By Skills'/>
          <FilterSection items={domainItems} sectionLabel='Filter By Domain'/>
        </ScrollArea.Content>
      </ScrollArea.Viewport>
      <ScrollArea.Scrollbar>
        <ScrollArea.Thumb />
      </ScrollArea.Scrollbar>
      <ScrollArea.Corner />
    </ScrollArea.Root>
  )
}