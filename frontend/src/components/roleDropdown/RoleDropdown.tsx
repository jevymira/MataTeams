// libraries
import { useEffect, useState, useMemo } from 'react'
import { Combobox, Portal, Select, createListCollection, Wrap, Badge } from '@chakra-ui/react'
// types
import { Role } from '../../types'
import { useGetRoles } from '../../hooks/projects'

type RolesDropdownProps = {
    labelText: string
    setRoleId: (roleId: string) => void
}

const RolesDropdown = ({labelText, setRoleId}: RolesDropdownProps) => {
    const [roles, getRoles] = useGetRoles()
    const [selectedRole, setSelectedRole] = useState<string[]>([])

    useEffect(() => {
        getRoles()
    }, [])

    const rolesCollection = createListCollection<Role>({
        items: roles ?? [],
        itemToString: (role) => role.name,
        itemToValue: (role) => role.id,
    })

    return (
    <Select.Root collection={rolesCollection} 
      value={selectedRole} 
      onValueChange={(e) => {
        setSelectedRole(e.value)
        setRoleId(e.value[0])
      }}>
      <Select.HiddenSelect />
      <Select.Label>{labelText}</Select.Label>
      <Select.Control>
        <Select.Trigger>
          <Select.ValueText placeholder="Select role type" />
        </Select.Trigger>
      </Select.Control>
      <Portal>
        <Select.Positioner>
          <Select.Content>
            {rolesCollection.items.map((role) => (
              <Select.Item item={role} key={role.name}>
                {role.name}
                <Select.ItemIndicator />
              </Select.Item>
            ))}
          </Select.Content>
        </Select.Positioner>
      </Portal>
    </Select.Root>
  )
}

export default RolesDropdown