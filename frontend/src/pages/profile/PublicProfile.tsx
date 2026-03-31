// libraries
import { useContext, useEffect, useState } from 'react'
import { useParams, useNavigate } from "react-router"

// context
import { UserContext } from '../../context/auth'

// types
import { UserContextType } from '../../types'

// hooks
import { useGetUserByID } from '../../hooks/users'
import { Container, Text, Wrap, Badge, Image, Flex, Card, IconButton } from '@chakra-ui/react'

// images
import ProfilePlaceholder from '../../images/catImg.jpg'
import { LuExternalLink, LuMail } from 'react-icons/lu'

function PublicProfile() {
    let { id } = useParams()
    const navigate = useNavigate()
    const { token } = useContext(UserContext) as UserContextType

    const [user, getUser] = useGetUserByID(id ? id : '', token)

    useEffect(() => {
        if (!id || id == '') {
            navigate('/')
        } else {
            getUser()
        }
    }, [])

    return (
        <Container marginTop={'15px'} paddingTop={'20px'} paddingBottom={'20px'} maxWidth={'700px'} backgroundColor={'white'} borderRadius={'10px'}>
            <Flex flexDirection='row' justifyContent={'center'} alignItems={'center'}>
                <Image width='150px' src={ProfilePlaceholder} borderRadius={'150px'}/>
                <Flex flexDirection={'column'} paddingLeft={'12px'}>
                    <Text fontSize={'30px'}>{user ? user.firstName + " " + user.lastName: ''}</Text>
                    <Text textAlign={'left'} fontSize={'18px'}>CSUN ARCS Advisor</Text>
                    <Flex flexDirection={'row'} alignItems={'center'}>
                        <LuMail />
                        <Text paddingLeft={'5px'} textDecoration={'underline'}>Contact</Text>
                    </Flex>
                </Flex>

            </Flex>
            <Text></Text>
            <Container marginTop={'20px'}>
                <Text fontWeight={'bold'} fontSize={'22px'} textAlign={'left'}>Skills</Text>
                <Wrap>
                    {user && user.skills && user.skills.length > 0 ? (
                        user.skills.map((skill, index) => (
                            <Badge key={index} mt={2}>{skill.name}</Badge>))
                        ) : (
                            <Text mt={2} color="gray">
                        No skills added yet
                        </Text>)
                        }
                </Wrap>
            </Container>
            <Container marginTop={'30px'}>
                <Text marginBottom={'10px'} fontWeight={'bold'} fontSize={'22px'} textAlign={'left'}>Projects with open roles</Text>
                    <Card.Root  width="250px" variant='outline'>
                        <Card.Body>
                            <Card.Title>
                                ARCS: RecyCOOL
                            </Card.Title>
                            <Card.Description>
                                <Text>GOAL: Increase recycling part...</Text>
                                <Text>Open roles: 2</Text>
                            </Card.Description>
                        </Card.Body>
                        <Card.Footer justifyContent="flex-end">
                            <IconButton colorPalette='gray' variant={'subtle'} padding={'10px'}>
                                <LuExternalLink />
                                View Project
                                </IconButton>
                        </Card.Footer>
                    </Card.Root>
            </Container>
        </Container>
    )
}

export default PublicProfile