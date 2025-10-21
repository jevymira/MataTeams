// libraries
import { useEffect, useState } from 'react'
import { Portal, Select, createListCollection } from '@chakra-ui/react'
// types
import { Skill } from '../../types'
import { useGetSkills } from '../../hooks/projects'

function SkillsDropdown() {
    const [skills, getSkills] = useGetSkills()
    const [userSkill, selectUserSkill] = useState<string[]>([])

    useEffect(() => {
        getSkills()
    }, [])

    const skillsCollection = createListCollection<Skill>({
        items: skills ?? [],
        itemToString: (skill) => skill.name,
        itemToValue: (skill) => skill.name,
    })

    return (
        <Select.Root collection={skillsCollection} 
            size="sm" 
            value={userSkill}
            onValueChange={(e) => selectUserSkill(e.value)} >
            <Select.Control>
                <Select.Trigger>
                <Select.ValueText placeholder="Select skill" />
                </Select.Trigger>
                <Select.IndicatorGroup>
                <Select.Indicator />
                </Select.IndicatorGroup>
            </Select.Control>

            <Portal>
                <Select.Positioner>
                    <Select.Content>
                        {skillsCollection.items.map((skill) => (
                            <Select.Item item={skill} key={skill.id}>
                            {skill.name}
                            <Select.ItemIndicator />
                        </Select.Item>
                        ))}
                    </Select.Content>
                </Select.Positioner>
            </Portal>
        </Select.Root>
    )
}

export default SkillsDropdown