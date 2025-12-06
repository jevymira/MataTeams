//libraries
import { useState } from "react"
import { Container, createListCollection, ListCollection, Select } from "@chakra-ui/react"

type RolesDropdownProps = {
    projectType: string
}

const ProjectTypeDropdown = ({projectType}: RolesDropdownProps) => {
    const [status, setStatus] = useState<string[]>([])

    const projectStatusList = (): ListCollection  => {
        switch (projectType) {
            case 'Film':
                return createListCollection({
                    items: [
                        { label: 'Development', value: 'Development'},
                        { label: 'PreProduction', value: 'PreProduction'},
                    ]
                })
            case 'ARCS':
            case 'Facuty':
            default:
                return createListCollection({
                    items: [
                        { label: "ARCS", value: "ARCS" },
                        { label: "Faculty", value: "Faculty" },
                        { label: "Club", value: "Club" },
                        { label: "Class", value: "Class" },
                        { label: "Personal", value: "Personal" },
                        { label: "Film", value: "Film" },
                    ],
                })
        } 
    }

    return (
        <Container>
            <Select.Root
                collection={projectStatusList()}
                value={status}
                onValueChange={(e) => {
                    setStatus(e.value)
                }}>
                <Select.Control>
                    <Select.Trigger>
                    <Select.ValueText placeholder="Select framework" />
                    </Select.Trigger>
                    <Select.IndicatorGroup>
                    <Select.Indicator />
                    </Select.IndicatorGroup>
                </Select.Control>
                <Select.Positioner>
                    <Select.Content>
                        {projectStatusList().items.map((status) => (
                            <Select.Item item={projectType} key={status.value}>
                                {status.label}
                            </Select.Item>
                        ))}
                    </Select.Content>
                </Select.Positioner>
            </Select.Root>
        </Container>
    )
}

export default ProjectTypeDropdown