// libraries
import { useState } from 'react'
import { Container, Field, Text, Input, Button, IconButton } from '@chakra-ui/react'
import SkillsDropdown from '../skillsDropdown/SkillsDropdown'
import { LuUserPlus } from "react-icons/lu"

// types
import { Role, DefaultRole } from '../../types'
import AddRoleForm from '../addRoleForm/AddRoleForm'

function CreateProjectForm() {
    const [projectName, setName] = useState("")
    const [description, setDescription] = useState("")
    const [roles, setRoles] = useState<Role[]>([])

    const addRole = () => {
        if (roles.length < 11) {
            setRoles([...roles, DefaultRole])
        }
    }
 
    return (
        <Container maxWidth={500}>
            <Field.Root>
                <Field.Label>
                    <Field.RequiredIndicator />
                    <Text>Project Name</Text>
                </Field.Label>
                <Input size='md' onChange={e => {
                    setName(e.target.value)
                }} />
                <Field.ErrorText>
                    <Text>Project name must be longer than one character!</Text>
                </Field.ErrorText>
            </Field.Root>

            <Field.Root>
                <Field.Label>
                    <Field.RequiredIndicator />
                    <Text>Description</Text>
                </Field.Label>
                <Input size='md' onChange={e => {
                    setDescription(e.target.value)
                }} />
                <Field.ErrorText>
                    <Text>Project name must be longer than one character!</Text>
                </Field.ErrorText>
            </Field.Root>
            <IconButton onClick={(e) => {
                addRole()
            }}>
                <LuUserPlus aria-label="Add new role"/>
            </IconButton>
            {roles.map(r => {
                return <AddRoleForm />
            })}
            <Button>
                <Text>Submit</Text>
            </Button>
        </Container>

    )
  }
  
export default CreateProjectForm