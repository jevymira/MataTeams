// libraries
import { Route, Routes } from 'react-router'
import { ChakraProvider, defaultSystem } from '@chakra-ui/react'

// components
import { Header } from './components/header/Header'
import Projects from './pages/projects/Projects'
import Profile from './pages/profile/Profile'
import { NotFound } from './pages/notFound/NotFound'
import { Login } from './pages/login/Login'
import { Signup } from './pages/signup/signup'
import PrivateRoute from './components/privateRoute/PrivateRoute'
import ProjectView from './pages/projectView/ProjectView'

// context
import ProjectsContextProvider from './context/project'
import AuthContextProvider from './context/auth'

// style
import './App.css'
import CreateProjectForm from './components/createProjectForm/CreateProjectForm'

function App() {
  return (
    <AuthContextProvider>
      <ProjectsContextProvider>
        <ChakraProvider value={defaultSystem}>
          <div className="App">
            <Header />
            <Routes>
              <Route path='/' element={<PrivateRoute outlet={<Projects />} />} />
              <Route path='/new' element={<PrivateRoute outlet={<CreateProjectForm />} />} />
              <Route path='/project/view' element={<PrivateRoute outlet={<ProjectView />} />} />
              <Route path='/profile' element={<PrivateRoute outlet={<Profile />} />} />
              <Route path='/login' element={<Login />} />
              <Route path='/signup' element={<Signup />}/>
              <Route path="*" element={<NotFound />}></Route>
            </Routes>
          </div>
        </ChakraProvider>
      </ProjectsContextProvider>
    </AuthContextProvider>
  )
}

export default App;
