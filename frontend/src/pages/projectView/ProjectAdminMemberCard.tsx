// libraries
import { useContext, useState } from "react"
import { Button, IconButton, Card, Badge, Wrap } from "@chakra-ui/react"
import { LuClock, LuUserMinus } from "react-icons/lu"


// types
import { ProjectRole, UserContextType } from "../../types"

// utiltiies
import { findMatchingSkillFromRole } from "../../utilities/sortFilterProjects"

// context
import { UserContext } from "../../context/auth"
import { useRequestRole } from "../../hooks/teams"

type ProjectAdminMemberCardProps = {
    role: ProjectRole
    teamID: string
}

function ProjectAdminMemberCard({role, teamID}: ProjectAdminMemberCardProps) {
    const {skills, token} = useContext(UserContext) as UserContextType
    const [roleRequest, requestRole] = useRequestRole(role.projectRoleId, teamID, token)
    const [didRequestRole, setDidRequestRole] = useState(false)
    const matchingSkill = findMatchingSkillFromRole(role, skills)

    return (
        <Card.Root padding={'10px'}>
            <Card.Title>{`Member: ${role.roleName} developer`}</Card.Title>
            <Card.Body>
                <Card.Description>
                    {`Role: ${role.roleName} developer`}
                    Status: Accepted
                </Card.Description>
            </Card.Body>
            <Card.Footer justifyContent="flex-end">
                <Button width={'180px'}>
                    View Member Profile    
                </Button>
                <IconButton bgColor={'red'} width={'180px'}>
                    <LuUserMinus />
                    Remove From Team    
                </IconButton>
            </Card.Footer>
        </Card.Root>
    )
}
export default ProjectAdminMemberCard