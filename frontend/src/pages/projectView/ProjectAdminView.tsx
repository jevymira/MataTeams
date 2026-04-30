// libraries
import { Flex, Box, Text, Button, Card, Grid, GridItem } from '@chakra-ui/react'

// types
import { Project } from '../../types'

type ProjectAdminViewProps = {
    project: Project
}

function ProjectAdminView({project} : ProjectAdminViewProps) {
    return (
        <Flex>
            ADMIN VIEW
        </Flex>
    )
}

export default ProjectAdminView