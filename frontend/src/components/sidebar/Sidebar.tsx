// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'

export const Sidebar = () => {
  return(
  <div className='sidebar'>
    <div className='sideBarItem'>Featured</div>
    <Link to='/' className='sideBarItem'>Projects</Link>
  </div>
)}