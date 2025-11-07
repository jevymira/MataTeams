// libraries 
import { useState } from 'react'
import { Input, Container, Button, Stack, Checkbox, CheckboxGroup, Fieldset } from '@chakra-ui/react'
import { PasswordInput } from '../../components/ui/password-input'
import SkillsDropdown from '../../components/skillsDropdown/SkillsDropdown'

// hooks

// style
import './signup.css'

const skills = [
    {label: "Skill 1", value: 1},
    {label: "Skill 2", value: 2},
    {label: "Skill 3", value: 3}
]

export const Signup = () => {

    return (
        <div className='background'>
            <Container maxW='2xl' className='Form'>
                <p>Welcome!</p>
                <p>Let's get you set up!</p>
                <p>Please enter your first and last name:</p>
                <Input placeholder='First Name' size='md'/>
                <Input placeholder='Last Name' size='md'/>
                <p>Create a Username and Password</p>
                <Input placeholder='Username' size='md'/>
                <Input placeholder='Password' size='md'/>
                <p>Are you a Student or Faculty member?</p>
                <Input placeholder='Student/Faculty' size='md'/>
                <p>Enter your major:</p>
                <Input placeholder='Major' size='md'/>
                <SkillsDropdown />
                <Button>Submit</Button>
            </Container>
        </div>
    )
  }