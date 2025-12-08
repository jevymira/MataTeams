// libraries
import { useEffect, useState, useMemo } from 'react'
import { Combobox, Portal, Select, createListCollection, Wrap, Badge } from '@chakra-ui/react'
// types
import { Skill } from '../../types'
import { useGetSkills } from '../../hooks/projects'

type SkillsDropdownProps = {
    labelText: string
    setFormSkills: (skills: Skill[]) => void
}

const SkillsDropdown = ({labelText, setFormSkills}: SkillsDropdownProps) => {
    const [skills, getSkills] = useGetSkills()
    const [searchValue, setSearchValue] = useState("")
    const [selectedSkills, setSelectedSkills] = useState<string[]>([])

    useEffect(() => {
        getSkills()
    }, [])

    const skillsCollection = createListCollection<Skill>({
        items: skills ?? [],
        itemToString: (skill) => skill.name,
        itemToValue: (skill) => skill.id,
    })

      const filteredItems = useMemo(
        () =>
        skills.filter((skill) =>
            skill.name.toLowerCase().includes(searchValue.toLowerCase()),
        ),
        [searchValue],
    )

    const handleValueChange = (details: Combobox.ValueChangeDetails) => {
        setSelectedSkills(details.value)
        setFormSkills(details.items)
    }

    return (
        <Combobox.Root collection={skillsCollection} 
            multiple
            size="sm" 
            value={selectedSkills}
            onValueChange={handleValueChange}
            onInputValueChange={(details) => setSearchValue(details.inputValue)}>
            <Combobox.Label>{labelText}</Combobox.Label>
            <Wrap gap="1">
                {selectedSkills.map((skill) => (
                <Badge key={skill}>{skills.find(s => s.id == skill)?.name}</Badge>
                ))}
            </Wrap>
            <Combobox.Control>
                <Combobox.Input backgroundColor={'white'} />
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