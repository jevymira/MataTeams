// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'
import { Box, Checkbox, ScrollArea, Stack } from '@chakra-ui/react'
import FilterSection from './FilterSection'


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

const skillItems = [
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
export const Sidebar = () => {
  return(
  <ScrollArea.Root height="80vh" maxW="md" paddingLeft={'5vw'}>
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
)}