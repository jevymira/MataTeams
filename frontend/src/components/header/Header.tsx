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
          <Link to='/profile' className='profileLink'>
              My Profile
          </Link>
          <Link to='/login'>Log Out</Link>
        </div>
    </div>
  </div>
)}