//libraries
import { useState } from "react"
import { Container, createListCollection, ListCollection, Select } from "@chakra-ui/react"

type RolesDropdownProps = {
    projectType: string
}

const ProjectStatusDropdown = ({projectType}: RolesDropdownProps) => {
    const [status, setStatus] = useState<string[]>([])
    
    const projectStatusList = (): ListCollection  => {
        switch (projectType) {
            case 'Film':
                return createListCollection({
                    items: [
                        { label: 'Development', value: 'Development'},
                        { label: 'PreProduction', value: 'PreProduction'},
                        { label: 'Production', value: 'Production'},
                        { label: 'PostProduction', value: 'PostProduction'},
                    ]
                })
            case 'ARCS':
            case 'Faculty':
            default:
                return createListCollection({
                    items: [
                        { label: "Draft", value: "Draft"},
                        { label: "Planning", value: "Planning" },
                        { label: "Active", value: "Active" },
                    ],
                })
        } 
    }

    return (
        <Select.Root
            collection={projectStatusList()}
            value={status}
            onValueChange={(e) => {
                setStatus(e.value)
            }}>
            <Select.Control backgroundColor={'white'}>
                <Select.Trigger>
                <Select.ValueText placeholder="Project status" />
                </Select.Trigger>
                <Select.IndicatorGroup>
                <Select.Indicator />
                </Select.IndicatorGroup>
            </Select.Control>
            <Select.Positioner>
                <Select.Content>
                    {projectStatusList().items.map((status) => (
                        <Select.Item item={status} key={status.value}>
                            {status.label}
                        </Select.Item>
                    ))}
                </Select.Content>
            </Select.Positioner>
        </Select.Root>
    )
}

export default ProjectStatusDropdown