// libraries
import { Container } from '@chakra-ui/react'
import { useContext } from 'react'

// context
import { AuthContext } from '../../context/auth'

// types
import { AuthContextType } from '../../types'

function Profile() {
const { username } = useContext(AuthContext) as AuthContextType
    return (
        <Container>
            Welcome back, {username}!
        </Container>

    )
  }
  
  export default Profile
  