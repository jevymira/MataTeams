import { Avatar, AvatarGroup, Button, Text } from '@chakra-ui/react'
import { LuPlus } from "react-icons/lu"
import { Link, useNavigate } from 'react-router'
import { Flex, Box } from "@chakra-ui/react"
import './Header.css'
import { useContext } from 'react'
import { AuthContext } from '../../context/auth'
import { AuthContextType } from '../../types'

export const Header = () => {
  const { firstName } = useContext(AuthContext) as AuthContextType
  const navigate = useNavigate();
  
  const logout = () => {
    localStorage.removeItem("token")
    localStorage.removeItem("username")
    localStorage.removeItem("firstName")
    localStorage.removeItem("lastName")
    navigate('/login')
  }

  const routeToNewProject = () => {
    navigate('/new')
  }
  
  return(
    <div className='headerContainer'>
      <div className='headerFlex'>
          <Link to='/' className='homeLink'>
          <Text 
            color={"#FBFBFB"}
            fontSize={'26px'}
            fontWeight={'bold'}
            fontFamily={'"Merriweather Sans", sans-serif;'}>
              MataTeams
            </Text>
          </Link>
            {(localStorage.getItem("token") && localStorage.getItem("token")!.length > 0) && (
              <Flex alignItems={'center'}>
                <Button aria-label="Create new Project" onClick={routeToNewProject} marginRight={'15px'}>
                  <LuPlus /> New Project
                </Button>
                <Box display={'flex'} width='200px' padding={'10px'} alignItems={'center'} 
                _hover={{backgroundColor:'rgba(76, 0, 0, 0.3)'}}>
                  <AvatarGroup>
                    <Link to='/profile' className='profileLink'>
                    <Avatar.Root>
                      <Avatar.Fallback />
                      <Avatar.Image />
                    </Avatar.Root>
                    </Link>
                  </AvatarGroup>
                  <Text>{firstName}</Text>
                  <button onClick={logout} className='dropdownContent'>Log Out</button>
                </Box>
              </Flex>
            )}
            
      </div>
    </div>
)}