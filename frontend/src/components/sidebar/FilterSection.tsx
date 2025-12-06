import { Checkbox, Field, Stack, Text } from "@chakra-ui/react"

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
        <Stack paddingTop={'15px'}>
            <Field.Root>
                <Field.Label paddingLeft={'15px'}>{sectionLabel}</Field.Label>
                {items.map((item) => (
                    <Checkbox.Root key={item.value} value={item.value}>
                    <Checkbox.HiddenInput />
                    <Checkbox.Control>
                    <Checkbox.Indicator />
                    </Checkbox.Control>
                    <Checkbox.Label>{item.label}</Checkbox.Label>
                </Checkbox.Root>
                ))}
            </Field.Root>
        </Stack>
    )
}

export default FilterSection