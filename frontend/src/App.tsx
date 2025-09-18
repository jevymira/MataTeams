import './App.css'
import { Header } from './components/header/Header'
import { Route, Routes } from 'react-router'
import Projects from './pages/projects/Projects'
import Profile from './pages/profile/Profile'
import { ProjectObj } from './types'

// TODO need context
// reorg sample data
const sampleProjects: ProjectObj[] = [
  {title: 'Make a tastier Protein Bar', 
    description: 'We will create the worlds most infinitely tasty protein bar', 
    members: [], 
    startDate: '', 
    skills: [],
    category: ''},
  {title: 'Web app for ducks', 
    description: 'This app will provide all water fowl related needs', 
    members: [], 
    startDate: '', 
    skills: [],
    category: ''},
  {title: '', 
    description: '', 
    members: [], 
    startDate: '', 
    skills: [],
    category: ''},
]

//projects={sampleProjects}
function App() {
  return (
    <div className="App">
      <Header />
      <Routes>
        <Route path='/'  element={<Projects projects={sampleProjects}/>} />
        <Route path='/profile' element={<Profile />}/>
      </Routes>
    </div>
  );
}

export default App;
