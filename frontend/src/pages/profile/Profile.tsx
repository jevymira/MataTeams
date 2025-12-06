// libraries
import { useContext, useState } from 'react'
import { Container, Text, Button } from '@chakra-ui/react'

// context
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType } from '../../types'
import CreateProjectForm from '../../components/createProjectForm/CreateProjectForm'

function Profile() {
    const { username, skills } = useContext(AuthContext) as AuthContextType
    const [showCreateProjectForm, setCreateProjectForm] = useState(false)
    
    const toggleCreateProjectForm = () => {
        setCreateProjectForm(!showCreateProjectForm)
    }

    return (
        <Container style={{paddingTop: '20px'}}>
            <Text>Welcome back, {username}!</Text>
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
  