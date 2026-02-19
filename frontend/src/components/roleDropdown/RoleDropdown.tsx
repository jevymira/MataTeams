// libraries
import { useEffect, useState, useMemo } from 'react'
import { Combobox, Portal, Select, createListCollection, Wrap, Badge } from '@chakra-ui/react'
// types
import { Role } from '../../types'
import { useGetRoles } from '../../hooks/projects'

type RolesDropdownProps = {
    labelText: string
    projectType: string
    setRoleId: (roleId: string) => void
}

const gameDesignRoles = ['3D Modeler', 'Texture artist', 'Programmer', 'Mechanics Designer', 'Level Designer']
const programmingRoles = ['Backend', 'Frontend', 'Fullstack', 'Machine Learning']

const RolesDropdown = ({labelText, setRoleId, projectType}: RolesDropdownProps) => {
    const [roles, getRoles] = useGetRoles()
    const [selectedRole, setSelectedRole] = useState<string[]>([])

    useEffect(() => {
        getRoles()
    }, [])

        const filterRolesForProjectType = (rolesToFilter: Role[]): Role[] => {
      return rolesToFilter.filter(r => {
        if (projectType == 'Game Design' && gameDesignRoles.includes(r.name)) {
          return r
        } 
        else if (projectType != 'Game Design' && projectType != 'Business' && programmingRoles.includes(r.name)) {
          return r
        }
      })  
    }

    const rolesCollection = createListCollection<Role>({
        items: roles? filterRolesForProjectType(roles) : [],
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
      <Select.Control backgroundColor={'white'}>
        <Select.Trigger>
          <Select.ValueText placeholder="Role type..." />
        </Select.Trigger>
      </Select.Control>
      <Portal>
        <Select.Positioner>
          <Select.Content >
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