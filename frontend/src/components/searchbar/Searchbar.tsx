// libraries
import { Combobox, createListCollection, Flex, Input, InputGroup, Portal, ScrollArea, Select } from "@chakra-ui/react"
import { LuSearch } from "react-icons/lu"

// style
import './Searchbar.css'
import { Dispatch, SetStateAction, useState } from "react"
import { InputItem } from "../../types"

type SearchbarProps = {
    sortBy: string[]
    setSortBy: Dispatch<SetStateAction<string[]>>
    skillItems: InputItem[]
    domainItems: InputItem[]
    projectStatusItems: InputItem[]
}

function Searchbar({sortBy, setSortBy, skillItems, domainItems}: SearchbarProps) {
    const sortOptions = createListCollection({
        items: [
            { label: "Sort by recommended", value: "rec" },
            { label: "Sort by name (asc.)", value: "name_a" },
            { label: "Sort by name (desc.)", value: "name_d" },
            { label: "Sort by most recent", value: "recent" },
        ],
    })

    return (
        <Flex flexDirection={'column'} alignItems={'flex-end'}>
            <div style={{width: '600px', marginBottom:'10px'}}>
                <InputGroup backgroundColor={'white'} startElement={<LuSearch />}>
                <Input placeholder="What kind of project do you want to work on?" />
                </InputGroup>
            </div>
            <Flex>
                <Select.Root collection={createListCollection({items: skillItems})} 
                    size="sm"
                    width="180px"
                    backgroundColor={'white'}
                    >
                    <Select.Control>
                        <Select.Trigger>
                        <Select.ValueText placeholder="Filter by skills" />
                        </Select.Trigger>
                        <Select.IndicatorGroup>
                        <Select.Indicator />
                        </Select.IndicatorGroup>
                    </Select.Control>
                    <Portal>
                        <Select.Positioner>
                            <Select.Content backgroundColor={'white'}>
                                {skillItems.map((skill) => (
                                    <Select.Item item={skill} key={skill.value} >
                                    {skill.label}
                                    <Select.ItemIndicator />
                                </Select.Item>
                                ))}
                            </Select.Content>
                        </Select.Positioner>
                    </Portal>
                </Select.Root>
                    <Select.Root collection={createListCollection({items: domainItems})} 
                        size="sm"
                        width="180px"
                        className="showSmall"
                        backgroundColor={'white'}
                        >
                        <Select.Control>
                            <Select.Trigger>
                            <Select.ValueText placeholder="Filter by domain" />
                            </Select.Trigger>
                            <Select.IndicatorGroup>
                            <Select.Indicator />
                            </Select.IndicatorGroup>
                        </Select.Control>
                        <Portal>
                            <Select.Positioner>
                            <Select.Content backgroundColor={'white'}>
                                {skillItems.map((skill) => (
                                    <Select.Item item={skill} key={skill.value} >
                                    {skill.label}
                                    <Select.ItemIndicator />
                                </Select.Item>
                                ))}
                            </Select.Content>
                            </Select.Positioner>
                        </Portal>
                    </Select.Root>
                </Flex>
                <Select.Root collection={sortOptions} 
                    size="sm"
                    width="220px"
                    backgroundColor={'white'}
                    value={sortBy}
                    onValueChange={(e) => setSortBy(e.value)}
                    >
                    <Select.Control>
                        <Select.Trigger>
                        <Select.ValueText placeholder="Sort by recommended" />
                        </Select.Trigger>
                        <Select.IndicatorGroup>
                        <Select.Indicator />
                        </Select.IndicatorGroup>
                    </Select.Control>
                    <Portal>
                        <Select.Positioner>
                        <Select.Content backgroundColor={'white'}>
                            {sortOptions.items.map((sortBy) => (
                                <Select.Item item={sortBy} key={sortBy.value} >
                                {sortBy.label}
                                <Select.ItemIndicator />
                            </Select.Item>
                            ))}
                        </Select.Content>
                        </Select.Positioner>
                    </Portal>
                </Select.Root>
        </Flex>
    )
}

export default Searchbar