// libraries
import { useContext, useEffect, useState } from 'react'
import { Container, Text, Button, Grid, Box } from '@chakra-ui/react'

// context
import { UserContext } from '../../context/auth'

// types
import { UserContextType } from '../../types'
import CreateProjectForm from '../../components/createProjectForm/CreateProjectForm'
import { useGetPendingRequests } from '../../hooks/teams'

function Profile() {
    const { firstName, skills, token } = useContext(UserContext) as UserContextType
    const [ pendingRequests, getRequests ] = useGetPendingRequests(token)
    
    useEffect(() => {
        getRequests()
    }, [])

    return (
        <Container style={{paddingTop: '20px'}}>
            <Text fontSize={'22px'}>Welcome back, {firstName}!</Text>
            {skills?.length > 0 ? (
                skills.map((skill, index) => (
                <Text key={index} mt={2}>{skill.name}</Text>))
                ) : (
                <Text mt={2} color="gray">
                No skills added yet
                </Text>
            )}
            <Text>Pending Requests</Text>
            <Grid>
                {pendingRequests.map(request => {
                    return <Box>{request.projectName}</Box>
                })}
            </Grid>
        </Container>
    )
  }
  
  export default Profile
  