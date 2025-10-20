// libraries
import { useState } from 'react'
import { Container, Field, Text, Input, Button } from '@chakra-ui/react'

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
            <Button>

            </Button>
        </Container>

    )
  }
  
export default CreateProjectForm