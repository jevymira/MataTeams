import { Checkbox, CheckboxGroup, Field, Fieldset, Stack, Text } from "@chakra-ui/react"

type FilterItem = {
    label: string
    value: string
}

type FilterSectionProps = {
    items: Array<FilterItem>
    sectionLabel: string
}

function FilterSection({items, sectionLabel}: FilterSectionProps) {
    return (
        <Stack paddingTop={'25px'}>
            <Fieldset.Root>
                <CheckboxGroup>
                    <Fieldset.Legend fontFamily={'"Outfit"; sans-serif;'} textAlign={'left'}>
                        {sectionLabel}
                    </Fieldset.Legend>
                    {items.map((item) => (
                        <Checkbox.Root key={item.value} value={item.value}>
                        <Checkbox.HiddenInput />
                        <Checkbox.Control>
                        <Checkbox.Indicator />
                        </Checkbox.Control>
                        <Checkbox.Label>{item.label}</Checkbox.Label>
                    </Checkbox.Root>
                    ))}
                </CheckboxGroup>
                </Fieldset.Root>
        </Stack>
    )
}

export default FilterSection