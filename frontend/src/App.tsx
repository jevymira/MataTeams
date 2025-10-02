import './App.css'
import { Header } from './components/header/Header'
import { Route, Routes } from 'react-router'
import Projects from './pages/projects/Projects'
import Profile from './pages/profile/Profile'
import ProjectsContextProvider from './context/Projects'
import ProjectView from './pages/projectView/ProjectView'

function App() {
  return (
    <ProjectsContextProvider>
      <div className="App">
        <Header />
        <Routes>
          <Route path='/' element={<Projects />}/>
          <Route path='/project/:id' element={<ProjectView />}/>
          <Route path='/profile' element={<Profile />}/>
        </Routes>
      </div>
    </ProjectsContextProvider>
  )
}

export default App;
