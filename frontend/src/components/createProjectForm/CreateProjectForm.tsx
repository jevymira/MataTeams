// libraries
import { useState } from 'react'
import { Container, Field, Text, Input, Button, Select } from '@chakra-ui/react'
import SkillsDropdown from '../skillsDropdown/SkillsDropdown'

function CreateProjectForm() {
    const [projectName, setName] = useState("")
    const [description, setDescription] = useState("")

    return (
        <Container>
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
            <SkillsDropdown />
            <Button>
                <Text>Submit</Text>
            </Button>
        </Container>

    )
  }
  
export default CreateProjectForm