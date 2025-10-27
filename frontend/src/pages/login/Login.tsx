// libraries 
import { useState } from 'react'
import { Link } from 'react-router'
import { Input, Container, Button } from '@chakra-ui/react'
import { PasswordInput } from '../../components/ui/password-input'

// hooks
import { useLogin } from '../../hooks/login'

// style
import './Login.css'

export const Login = () => {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [login] = useLogin(username, password)

  const handleSubmit = async (e: React.MouseEvent<HTMLButtonElement>) => {
    e.preventDefault()
    login()
  }
  
  return (
  <Container maxW='md'>
    <Input placeholder='Username' size='md' onChange={e => {
      setUsername(e.target.value)
    }} />
    <PasswordInput placeholder='Password' size='md' onChange={e => {
      setPassword(e.target.value)
    }}/>
    <Button onClick={e => handleSubmit(e)}>Log In</Button>
    <br /><Link to='/signup'>Sign up</Link>
  </Container>
)}