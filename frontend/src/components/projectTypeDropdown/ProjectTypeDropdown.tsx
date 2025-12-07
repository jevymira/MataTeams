//libraries
import { useState } from "react"
import { Container, createListCollection, Select } from "@chakra-ui/react"

type RolesDropdownProps = {
    setFormProjectType: (projectType: string) => void
}

const ProjectTypeDropdown = ({setFormProjectType}: RolesDropdownProps) => {
    const [type, setType] = useState<string[]>([])

    const projectTypes = createListCollection({
        items: [
            { label: "ARCS", value: "ARCS" },
            { label: "Faculty", value: "Faculty" },
            { label: "Club", value: "Club" },
            { label: "Class", value: "Class" },
            { label: "Personal", value: "Personal" },
            { label: "Film", value: "Film" },
        ],
    })

    return (
        <Select.Root
            collection={projectTypes}
            value={type}
            backgroundColor={'white'}
            onValueChange={(e) => {
                setType(e.value)
                setFormProjectType(e.value[0])
            }}>
            <Select.Control>
                <Select.Trigger>
                <Select.ValueText placeholder="What kind of project will this be?" />
                </Select.Trigger>
                <Select.IndicatorGroup>
                <Select.Indicator />
                </Select.IndicatorGroup>
            </Select.Control>
            <Select.Positioner>
                <Select.Content>
                    {projectTypes.items.map((projectType) => (
                        <Select.Item item={projectType} key={projectType.value}>
                            {projectType.label}
                        </Select.Item>
                    ))}
                </Select.Content>
            </Select.Positioner>
        </Select.Root>
    )
}

export default ProjectTypeDropdown