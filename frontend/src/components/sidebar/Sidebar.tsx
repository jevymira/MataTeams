// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'
import { Box, Checkbox, CheckboxGroup, Field, ScrollArea, Separator, Stack } from '@chakra-ui/react'
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
          <Field.Root paddingTop={'25px'} paddingBottom={'15px'}>
              <CheckboxGroup>
                  <Field.Label fontFamily={'"Outfit"; sans-serif;'} textAlign={'left'}>
                      {"Show only projects with:"}
                  </Field.Label>
                      <Checkbox.Root key={"showOpen"} value={"showOpen"} colorPalette={'gray'} variant='subtle'>
                      <Checkbox.HiddenInput />
                      <Checkbox.Control >
                      <Checkbox.Indicator />
                      </Checkbox.Control>
                      <Checkbox.Label>{"Relevant open roles"}</Checkbox.Label>
                  </Checkbox.Root>
              </CheckboxGroup>
            </Field.Root>
          <Separator width={'210px'} />
          <FilterSection items={projectStatusItems} sectionLabel='Filter By Project Status'/>
          <Separator width={'210px'} />
          <FilterSection items={skillItems} sectionLabel='Filter By Skills'/>
          <Separator width={'210px'} />
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