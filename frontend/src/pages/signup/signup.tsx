// libraries 
import { useState } from 'react'
import { Link } from 'react-router'
import { Input, Container, Button, Text, Checkbox, CheckboxCard, CheckboxGroup, Flex, ScrollArea } from '@chakra-ui/react'
import { PasswordInput } from '../../components/ui/password-input'
import SkillsDropdown from '../../components/skillsDropdown/SkillsDropdown'
import Uppy from '@uppy/core'
import Dashboard from '@uppy/dashboard'
import '@uppy/core/css/style.min.css'
import '@uppy/dashboard/css/style.min.css'
import {
  Dropzone,
  FilesGrid,
  FilesList,
  UploadButton,
  UppyContextProvider,
} from '@uppy/react'

// hooks
import { useSignup} from '../../hooks/signup'
import { useLogin } from '../../hooks/login'

//context 

//types
import { User, Skill} from '../../types'

// style
import './signup.css'

export const Signup = () => {
    const [email, setEmail] = useState('')
    const [skills, setSkills] = useState<Array<Skill>>([])
    const [firstName, setFirst] = useState('')
    const [lastName, setLast] = useState('')
    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [isFaculty, setFaculty] = useState(false) 

    const handleSetSignupFormSkills = (formSkills: Skill[]) => {
        setSkills(formSkills)
    }

    const setSignupFormSkillsFromPlaceholderResume = () => {
        console.log("??")
        const placeholderSkills: Skill[] = [{
            id: '',
            name: 'React'
        }]
        setSkills(placeholderSkills)
    }

    const [signup] = useSignup(email, password, firstName, lastName, username, isFaculty, skills)

    const handleSubmit = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault()
        signup()
    }

    const uppy = new Uppy({id: "upload", autoProceed: true, debug: false})

    uppy.on("file-added", (file) => {
        setSignupFormSkillsFromPlaceholderResume()
    });

    return (
        <UppyContextProvider uppy={uppy}>
            <Flex alignItems={'center'} flexDirection={'column'}>
                <ScrollArea.Root maxW='2xl' className='Form' height="80vh" >
                    <ScrollArea.Viewport>
                        <ScrollArea.Content padding={'15px'}>
                            <p>Welcome!</p>
                            <p>Let's get you set up!</p>

                            <p>Please enter your first and last name:</p>
                            <Input placeholder='First Name' size='md' onChange={e => {
                            setFirst(e.target.value)
                            }}/>

                            <Input placeholder='Last Name' size='md' onChange={e => {
                            setLast(e.target.value)
                            }}/>

                            <p>Please enter your CSUN email:</p>
                            <Input placeholder='Email' size='md' onChange={e => {
                            setEmail(e.target.value)
                            }}/>

                            <p>Create a Username and Password</p>
                            <Input placeholder='Username' size='md' onChange={e => {
                            setUsername(e.target.value)
                            }}/>

                            <Input placeholder='Password' type='password' size='md' onChange={e => {
                            setPassword(e.target.value)
                            }}/>

                            <p>Are you a Student or Faculty member?</p>
                                <CheckboxCard.Root variant='outline' colorPalette='green'
                                        checked={isFaculty ===true} onCheckedChange={() => setFaculty(true)}>
                                    <CheckboxCard.HiddenInput />
                                    <CheckboxCard.Control>
                                        <CheckboxCard.Label>Faculty</CheckboxCard.Label>
                                        <CheckboxCard.Indicator />
                                    </CheckboxCard.Control>
                                </CheckboxCard.Root>

                                <CheckboxCard.Root variant='outline' colorPalette='green'
                                        checked={isFaculty ===false} onCheckedChange={() => setFaculty(false)}>
                                    <CheckboxCard.HiddenInput />
                                    <CheckboxCard.Control>
                                        <CheckboxCard.Label>Student</CheckboxCard.Label>
                                        <CheckboxCard.Indicator />
                                    </CheckboxCard.Control>
                                </CheckboxCard.Root>

                            <p>Enter your major:</p>
                            <Input placeholder='Major' size='md'/>

                            <SkillsDropdown labelText='Select your skills' setFormSkills={handleSetSignupFormSkills} />
                            <Button onClick={e => handleSubmit(e)}>Submit</Button>
                            <br /><p>Already have an account? <Link to='/login'>Login here!</Link></p>

                            
                            <input
                                type="file"
                                name="image"
                                onChange={(e) => {
                                    if (e.target.files) {
                                        setSignupFormSkillsFromPlaceholderResume()
                                        uppy.addFile({ data: e.target.files[0], name: "image" });
                                    }
                                }}
                            />
                            <div>
                                <Text>Skills from resume:</Text>
                                <div>{skills && skills.map(s => {
                                    return (<div>{s.name}</div>)
                                })}</div>
                            </div>
                            
                        </ScrollArea.Content>
                    </ScrollArea.Viewport>
                    <ScrollArea.Scrollbar height="70vh">
                    <ScrollArea.Thumb />
                    </ScrollArea.Scrollbar>
                    <ScrollArea.Corner />
                </ScrollArea.Root>
            </Flex>
        </UppyContextProvider>
    )
  }