// libraries
import { useState } from "react"
import { Button, IconButton, Card, Badge, Wrap } from "@chakra-ui/react"
import { LuUserMinus } from "react-icons/lu"

// types
import { ProjectRole } from "../../types"

type ProjectAdminMemberCardProps = {
    role: ProjectRole
    teamID: string
    openRemoveMemberModal: (open: boolean) => void
}

function ProjectAdminMemberCard({role, teamID, openRemoveMemberModal}: ProjectAdminMemberCardProps) {
    const [isRemoved, setIsRemoved] = useState(false)

    if (isRemoved) {
        return <></>
    }

    return (
        <Card.Root padding={'10px'} marginTop={'10px'}>
            <Card.Title>{`Member: ${role.roleName} developer`}</Card.Title>
            <Card.Body>
                <Card.Description>
                    <p>{`Role: ${role.roleName} developer `}</p>
                    <p>Status: Accepted</p>
                </Card.Description>
            </Card.Body>
            <Card.Footer justifyContent="flex-end">
                <Button width={'180px'}>
                    View Member Profile    
                </Button>
                <IconButton bgColor={'red'} width={'180px'} onClick={() => openRemoveMemberModal(true)}>
                    <LuUserMinus />
                    Remove From Team    
                </IconButton>
            </Card.Footer>
        </Card.Root>
    )
}
export default ProjectAdminMemberCard