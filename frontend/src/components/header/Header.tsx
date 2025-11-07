import { Avatar, AvatarGroup } from '@chakra-ui/react'
import { Link } from 'react-router'
import './Header.css'

export const Header = () => {
  return(
  <div className='headerContainer'>
    <div className='headerFlex'>
        <Link to='/' className='homeLink'>
        <h1>Mata Teams</h1>
        </Link>
        <div>
          <AvatarGroup>
            <Link to='/profile' className='profileLink'>
            <Avatar.Root>
              <Avatar.Fallback />
              <Avatar.Image />
            </Avatar.Root>
            </Link>
          </AvatarGroup>
          <Link to='/login' className='profileLink'>Log Out</Link>
        </div>
    </div>
  </div>
)}