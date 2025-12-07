// libraries
import { useContext, useState } from 'react'
import { Container, Text, Button } from '@chakra-ui/react'

// context
import { UserContext } from '../../context/auth'

// types
import { UserContextType } from '../../types'
import CreateProjectForm from '../../components/createProjectForm/CreateProjectForm'

function Profile() {
    const { firstName, skills } = useContext(UserContext) as UserContextType

    return (
        <Container style={{paddingTop: '20px'}}>
            <Text>Welcome back, {firstName}!</Text>
            {skills?.length > 0 ? (
                skills.map((skill, index) => (
                <Text key={index} mt={2}>{skill.name}</Text>))
                ) : (
                <Text mt={2} color="gray">
                No skills added yet
                </Text>
            )}
        </Container>
    )
  }
  
  export default Profile
  