import { Avatar, AvatarGroup } from '@chakra-ui/react'
import { Link, useNavigate } from 'react-router'
import './Header.css'

export const Header = () => {
  const navigate = useNavigate();
  
  const logout = () => {
    localStorage.removeItem("token")
    navigate('/login')
  }
  console.log(localStorage.getItem("token"))
  return(
    <div className='headerContainer'>
      <div className='headerFlex'>
          <Link to='/' className='homeLink'>
          <h1>Mata Teams</h1>
          </Link>
            {(localStorage.getItem("token") && localStorage.getItem("token")!.length > 0) && (
              <div>
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