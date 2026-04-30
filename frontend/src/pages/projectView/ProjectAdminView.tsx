// libraries
import { useState } from 'react'
import { Flex, Box, Text, Button, Card, Grid, GridItem } from '@chakra-ui/react'
import { LuPencil } from 'react-icons/lu'

// components


// types
import { Project } from '../../types'
import ProjectAdminMemberCard from './ProjectAdminMemberCard'
import PopUp from '../../components/popup/PopUp'

type ProjectAdminViewProps = {
    project: Project
}

const ProjectAdminView = ({project} : ProjectAdminViewProps) => {
    const [open, setOpen] = useState(false)

    const setOpenModal = (openStatus: boolean) => {
        console.log("open modal!")
        setOpen(openStatus)
    }

    return (
        <>
            <PopUp 
                message={"Are you sure you want to remove this member from this project?"}
                setOpen={setOpenModal}
                open={open}
                confirmButtonMessage={"Remove Member"}
                confirmAction={() => {}} />

            <Box textAlign={'left'} backgroundColor={'white'} marginTop={'25px'} padding={'25px'} borderRadius={'20px'}>
                <Text fontFamily={'"Merriweather Sans", sans-serif;'} fontWeight={750} fontSize={'26px'} paddingTop={'20px'} >
                    {project.name}
                </Text>
                <Flex>
                    <Box>
                        <Text fontWeight={650} fontSize={'18px'}>About this project</Text>
                        <Text width={"600px"}>{project.description}</Text>
                        <Text marginTop={'50px'} fontWeight={650} fontSize={'18px'}>Project Team Members</Text>
                        <Grid templateColumns="repeat(2, 1fr)" gap="6">
                            {project.teams && project.teams.length > 0 && project.roles.map(r => {
                                return (<GridItem> 
                                    <ProjectAdminMemberCard 
                                    role={r} 
                                    teamID={project.teams[0].id} 
                                    openRemoveMemberModal={setOpenModal} /> 
                                </GridItem>)
                            })}
                        </Grid>
                    </Box>
                    <Box width={'200px'}>
                        <Flex flexDirection={'row'} alignItems={'center'}>
                            <LuPencil />
                            <Text paddingLeft={'10px'}>Type: {project.type}</Text>
                        </Flex>
                        <Flex flexDirection={'row'} alignItems={'center'}>
                            <LuPencil />
                            <Text paddingLeft={'10px'}>Status: {project.status}</Text>
                        </Flex>
                            <Flex flexDirection={'row'} alignItems={'center'}>
                            <LuPencil />
                            <Text paddingLeft={'10px'}>{project.roles.length} open roles</Text>
                        </Flex>
                    </Box>
                </Flex>
            </Box>
        </>
    )
}

export default ProjectAdminView