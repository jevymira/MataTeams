import { Link } from 'react-router'
import './Header.css'

export const Header = () => {
  return(
  <div className='headerContainer'>
    <div className='headerFlex'>
        <Link to='/' className='homeLink'>
        <h1>Mata Teams</h1>
        </Link>
        <Link to='/profile'>
            My Profile
        </Link>
    </div>
  </div>
)}