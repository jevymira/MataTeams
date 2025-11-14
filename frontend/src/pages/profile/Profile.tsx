// libraries
import { useContext, useState } from 'react'
import { Container, Text, Button } from '@chakra-ui/react'

// context
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType } from '../../types'
import CreateProjectForm from '../../components/createProjectForm/CreateProjectForm'

function Profile() {
    const { username } = useContext(AuthContext) as AuthContextType
    const [showCreateProjectForm, setCreateProjectForm] = useState(false)
    
    const toggleCreateProjectForm = () => {
        setCreateProjectForm(!showCreateProjectForm)
    }

    return (
        <Container style={{paddingTop: '20px'}}>
            <Text>Welcome back, {username}!</Text>
        </Container>

    )
  }
  
  export default Profile
  