// libraries
import { Route, Routes } from 'react-router'
import { ChakraProvider, defaultSystem } from '@chakra-ui/react'

// components
import { Header } from './components/header/Header'
import Projects from './pages/projects/Projects'
import Profile from './pages/profile/Profile'
import { NotFound } from './pages/notFound/NotFound'
import { Login } from './pages/login/Login'
import PrivateRoute from './components/privateRoute/PrivateRoute'
import ProjectView from './pages/projectView/ProjectView'

// context
import ProjectsContextProvider from './context/projects'
import AuthContextProvider from './context/auth'

// style
import './App.css'

function App() {
  return (
    <AuthContextProvider>
      <ProjectsContextProvider>
        <ChakraProvider value={defaultSystem}>
          <div className="App">
            <Header />
            <Routes>
              <Route path='/' element={<PrivateRoute outlet={<Projects />} />} />
              <Route path='/project/:id' element={<PrivateRoute outlet={<ProjectView />} />} />
              <Route path='/profile' element={<PrivateRoute outlet={<Profile />} />} />
              <Route path='/login' element={<Login />} />
              <Route path="*" element={<NotFound />}></Route>
            </Routes>
          </div>
        </ChakraProvider>
      </ProjectsContextProvider>
    </AuthContextProvider>
  )
}

export default App;
