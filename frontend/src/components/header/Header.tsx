import { Avatar, AvatarGroup, Button } from '@chakra-ui/react'
import { LuPlus } from "react-icons/lu"
import { Link, useNavigate } from 'react-router'
import './Header.css'

export const Header = () => {
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
          <h1>Mata Teams</h1>
          </Link>
            {(localStorage.getItem("token") && localStorage.getItem("token")!.length > 0) && (
              <div>
                <Button aria-label="Create new Project" onClick={routeToNewProject}>
                  <LuPlus /> New Project
                </Button>
                <AvatarGroup>
                  <Link to='/profile' className='profileLink'>
                  <Avatar.Root>
                    <Avatar.Fallback />
                    <Avatar.Image />
                  </Avatar.Root>
                  </Link>
                </AvatarGroup>
                <button onClick={logout} className='profileLink'>Log Out</button>
              </div>
            )}
            
      </div>
    </div>
)}