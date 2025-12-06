// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'
import { Box, Checkbox, ScrollArea, Stack } from '@chakra-ui/react'


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
  <ScrollArea.Root  height="50vh" maxW="md">
    <ScrollArea.Viewport>
      <ScrollArea.Content>
        <Stack>
          Filter By Project Status
          {projectStatusItems.map((status) => (
            <Checkbox.Root key={status.value} value={status.value}>
              <Checkbox.HiddenInput />
              <Checkbox.Control>
                <Checkbox.Indicator />
              </Checkbox.Control>
              <Checkbox.Label>{status.label}</Checkbox.Label>
            </Checkbox.Root>
          ))}
        </Stack>
        <Stack>
          Filter By Skills
          {skillItems.map((skill) => (
            <Checkbox.Root key={skill.value} value={skill.value}>
              <Checkbox.HiddenInput />
              <Checkbox.Control>
                <Checkbox.Indicator />
              </Checkbox.Control>
              <Checkbox.Label>{skill.label}</Checkbox.Label>
            </Checkbox.Root>
          ))}
        </Stack>
      </ScrollArea.Content>
    </ScrollArea.Viewport>
    <ScrollArea.Scrollbar>
      <ScrollArea.Thumb />
    </ScrollArea.Scrollbar>
    <ScrollArea.Corner />
  </ScrollArea.Root>
)}