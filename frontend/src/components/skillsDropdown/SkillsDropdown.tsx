// libraries
import { useEffect, useState, useMemo } from 'react'
import { Combobox, Portal, Select, createListCollection, Wrap, Badge } from '@chakra-ui/react'
// types
import { Skill } from '../../types'
import { useGetSkills } from '../../hooks/projects'

function SkillsDropdown() {
    const [skills, getSkills] = useGetSkills()
    const [searchValue, setSearchValue] = useState("")
    const [selectedSkills, setSelectedSkills] = useState<string[]>([])

    useEffect(() => {
        getSkills()
    }, [])

    const skillsCollection = createListCollection<Skill>({
        items: skills ?? [],
        itemToString: (skill) => skill.name,
        itemToValue: (skill) => skill.name,
    })

      const filteredItems = useMemo(
        () =>
        skills.filter((item) =>
            item.name.toLowerCase().includes(searchValue.toLowerCase()),
        ),
        [searchValue],
    )

    const handleValueChange = (details: Combobox.ValueChangeDetails) => {
        setSelectedSkills(details.value)
    }

    return (
        <Combobox.Root collection={skillsCollection} 
            multiple
            size="sm" 
            value={selectedSkills}
            onValueChange={handleValueChange}
            onInputValueChange={(details) => setSearchValue(details.inputValue)} >
            <Wrap gap="2">
                {selectedSkills.map((skill) => (
                <Badge key={skill}>{skill}</Badge>
                ))}
            </Wrap>
            <Combobox.Label>Select skills for your role in the project</Combobox.Label>
            
            <Combobox.Control>
                <Combobox.Input />
                <Combobox.IndicatorGroup>
                <Combobox.Trigger />
                </Combobox.IndicatorGroup>
            </Combobox.Control>

            <Portal>
                <Combobox.Positioner>
                    <Combobox.Content>
                        <Combobox.ItemGroup>
                            <Combobox.ItemGroupLabel>Skills</Combobox.ItemGroupLabel>
                        {filteredItems.map((skill) => (
                            <Combobox.Item item={skill} key={skill.id}>
                            {skill.name}
                            <Combobox.ItemIndicator />
                        </Combobox.Item>
                        ))}
                        <Combobox.Empty>No skills found</Combobox.Empty>
                        </Combobox.ItemGroup>
                    </Combobox.Content>
                </Combobox.Positioner>
            </Portal>
        </Combobox.Root>
    )
}

export default SkillsDropdown