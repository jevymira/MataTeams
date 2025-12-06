// libraries
import { Combobox, createListCollection, Flex, Input, InputGroup, Portal, Select } from "@chakra-ui/react"
import { LuSearch } from "react-icons/lu"

// style
import './Searchbar.css'

function Searchbar() {
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
            <Select.Root collection={sortOptions} 
                size="sm"
                width="220px"
                backgroundColor={'white'}
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