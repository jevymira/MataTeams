import { Avatar, AvatarGroup, Button, Text } from '@chakra-ui/react'
import { LuPlus } from "react-icons/lu"
import { Link, useNavigate } from 'react-router'
import { Flex, Box } from "@chakra-ui/react"
import './Header.css'
import { useContext } from 'react'
import { UserContext } from '../../context/auth'
import { UserContextType } from '../../types'

export const Header = () => {
  const { firstName } = useContext(UserContext) as UserContextType
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
            paddingBottom={'15px'}
            fontFamily={'"Merriweather Sans", sans-serif;'}>
              MataTeams
            </Text>
          </Link>
            {(localStorage.getItem("token") && localStorage.getItem("token")!.length > 0) && (
              <Flex alignItems={'center'}>
                <Button aria-label="Create new Project" onClick={routeToNewProject} marginRight={'15px'}>
                  <LuPlus /> New Project
                </Button>
                <div className='dropdown'>
                    <AvatarGroup display={'flex'} flexDirection={'row'}>
                      <Link to='/profile' className='dropbtn'>
                      <Avatar.Root>
                        <Avatar.Fallback />
                        <Avatar.Image />
                      </Avatar.Root>
                      </Link>
                      <Link to='/profile' className='dropbtn'><Text color={"white"}>{firstName}</Text></Link>
                    </AvatarGroup>
                  <div className='dropdown-content'>
                    <Link to='/profile'>My Profile</Link>
                    <a onClick={logout} className='logoutLink'>Log Out</a>
                  </div>
                </div>
              </Flex>
            )}
            
      </div>
    </div>
)}