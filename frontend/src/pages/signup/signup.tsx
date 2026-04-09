// libraries 
import { useState } from 'react'
import { Link } from 'react-router'
import { Input, Container, Button, Text, SegmentGroup, CheckboxCard, Wrap, Flex, ScrollArea, Badge, Spinner } from '@chakra-ui/react'
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
import {useForm} from 'react-hook-form'


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
    const [skillEntry, setSkillEntry] = useState("Upload Resume")
    const [isLoading, setIsLoading] = useState(false)

    const handleSetSignupFormSkills = (formSkills: Skill[]) => {
        setSkills(formSkills)
    }

    const setSignupFormSkillsFromPlaceholderResume = () => {
        setIsLoading(true)
        const placeholderSkills: Skill[] = [
        {
            id: '',
            name: 'React'
        },
        {
            id: '',
            name: 'C++'
        },
        {
            id: '',
            name: 'Unreal Engine 5'
        },
        {
            id: '',
            name: 'Blazor'
        },
        {
            id: '',
            name: '.NET ASP'
        },
        {
            id: '',
            name: 'Agile Methodology'
        },
        {
            id: '',
            name: 'Linux'
        },
        {
            id: '',
            name: 'C#'
        },
        {
            id: '',
            name: 'Unity3D'
        },
        {
            id: '',
            name: 'JavaScript'
        },
    ]
        setTimeout(() => {
            setSkills(placeholderSkills)
            setIsLoading(false)
        }, 600)
        
    }

    const [signup] = useSignup(email, password, firstName, lastName, username, isFaculty, skills)

    const uppy = new Uppy({id: "upload", autoProceed: true, debug: false})

    uppy.on("file-added", (file) => {
        setSignupFormSkillsFromPlaceholderResume()
    });

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm();

    return (
        <form onSubmit={handleSubmit(() => {signup()})}>
            <UppyContextProvider uppy={uppy}>
                <Flex alignItems={'center'} flexDirection={'column'}>
                    <ScrollArea.Root maxW='2xl' className='Form' height="80vh" >
                        <ScrollArea.Viewport>
                            <ScrollArea.Content padding={'15px'}>
                                <p>Welcome!</p>
                                <p>Let's get you set up!</p><br/>

                                <p>Please enter your first and last name:</p>
                                <Input placeholder='First Name' size='md' borderColor={errors.firstName ? "red.500" : undefined}
                                {...register("firstName", { required: true })}
                                onChange={e => { setFirst(e.target.value)}}
                                />
                                {errors.firstName && <Text color="red.500">First name is required</Text>}

                                <Input placeholder='Last Name' size='md' borderColor={errors.lastName ? "red.500" : undefined} 
                                {...register("lastName", { required: true })}
                                onChange={e => {setLast(e.target.value)}}
                                />
                                {errors.lastName && <Text color="red.500">Last name is required</Text>}<br/>

                                <p>Please enter your CSUN email:</p>
                                <Input placeholder='Email' size='md' borderColor={errors.email ? "red.500" : undefined}
                                {...register("email", { required: "Email is required"
/////////////////////////////////
/*                               
                                    ,
                                    pattern: {
Enforces CSUN Email format          value: /^[a-z]+\.[a-z]+\.\d{3}@my\.csun\.edu$/,
                                    message: "Must be a valid CSUN email (e.g. jane.doe.123@my.csun.edu)"
                                    }
*/                                    
/////////////////////////////////
                                })}
                                onChange={e => {setEmail(e.target.value)}}
                                />
                                {errors.email && <Text color="red.500">{String(errors.email.message)}</Text>}<br/>

                                <p>Create a Username and Password</p>
                                <Input placeholder='Username' size='md' borderColor={errors.username ? "red.500" : undefined}
                                {...register("username", { required: true })}
                                onChange={e => {setUsername(e.target.value)}}
                                />
                                {errors.username && <Text color="red.500">Username is required</Text>}

                                <PasswordInput placeholder='Password' type='password' size='md' 
                                borderColor={errors.password ? "red.500" : undefined}
                                {...register("password", {
                                    required: "Password is required",
                                    minLength: {
                                    value: 8,
                                    message: "Password must be at least 8 characters"
                                    }
                                })}
                                onChange={e => {setPassword(e.target.value)}}
                                />
                                {errors.password && <Text color="red.500">{String(errors.password.message)}</Text>}<br/>

                                <p>Are you a Student or Faculty member?</p>
                                    <CheckboxCard.Root variant='outline' colorPalette='green' className='checkboxcard'
                                            checked={isFaculty ===true} onCheckedChange={() => setFaculty(true)}>
                                        <CheckboxCard.HiddenInput />
                                        <CheckboxCard.Control>
                                            <CheckboxCard.Label>Faculty</CheckboxCard.Label>
                                            <CheckboxCard.Indicator />
                                        </CheckboxCard.Control>
                                    </CheckboxCard.Root>

                                    <CheckboxCard.Root variant='outline' colorPalette='green' className='checkboxcard'
                                            checked={isFaculty ===false} onCheckedChange={() => setFaculty(false)}>
                                        <CheckboxCard.HiddenInput />
                                        <CheckboxCard.Control>
                                            <CheckboxCard.Label>Student</CheckboxCard.Label>
                                            <CheckboxCard.Indicator />
                                        </CheckboxCard.Control>
                                    </CheckboxCard.Root>

                                <p>Enter your major:</p>
                                <small>(Not required but recommended!)</small>
                                <Input placeholder='Major' size='md'/>

                                <SegmentGroup.Root 
                                    value={skillEntry} 
                                    onValueChange={(e) => setSkillEntry(e.value !== null ? e.value : "Upload Resume")}
                                    marginBottom={'10px'} marginTop={'15px'}>
                                    <SegmentGroup.Indicator />
                                    <SegmentGroup.Items items={["Upload Resume", "Manual Entry"]} />
                                </SegmentGroup.Root>
                                {skillEntry == 'Upload Resume' ? 
                                (<Flex flexDirection={'column'} paddingBottom={'15px'}><input
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
                                    <Text>Skills:</Text>
                                    {isLoading ? <Spinner /> : <Wrap gap="1">{skills && skills.map(s => {
                                        return (<Badge>{s.name}</Badge>)
                                    })}</Wrap>}
                                </div></Flex>) : (
                                    <SkillsDropdown labelText='Select your skills' setFormSkills={handleSetSignupFormSkills} />
                                )}
                                <Button type="submit">Submit</Button>
                                <br /><p>Already have an account? <Link to='/login'>Login here!</Link></p>
                                
                            </ScrollArea.Content>
                        </ScrollArea.Viewport>
                        <ScrollArea.Scrollbar marginTop="15px" height="75vh">
                        <ScrollArea.Thumb />
                        </ScrollArea.Scrollbar>
                        <ScrollArea.Corner />
                    </ScrollArea.Root>
                </Flex>
            </UppyContextProvider>
        </form>
    )
  }