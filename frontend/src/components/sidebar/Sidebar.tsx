// libraries 
import { Link } from 'react-router'

// style
import './Sidebar.css'

export const Sidebar = () => {
  return(
  <div className='sidebar'>
    <div>Featured</div>
    <Link to='/'>Projects</Link>
    <div>Classes</div>
  </div>
)}