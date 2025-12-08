// libraries
import { useContext, useState } from "react"
import { Button, Card, Badge, Wrap } from "@chakra-ui/react"
import { LuClock } from "react-icons/lu"


// types
import { ProjectRole, UserContextType } from "../../types"

// utiltiies
import { findMatchingSkillFromRole } from "../../utilities/sortFilterProjects"

// context
import { UserContext } from "../../context/auth"
import { useRequestRole } from "../../hooks/teams"

type RoleCardProps = {
    role: ProjectRole
    teamID: string
    onToast: () => void
}

function RoleCard({role, teamID, onToast}: RoleCardProps) {
    const {skills, token} = useContext(UserContext) as UserContextType
    const [roleRequest, requestRole] = useRequestRole(role.projectRoleId, teamID, token)
    const [didRequestRole, setDidRequestRole] = useState(false)
    const matchingSkill = findMatchingSkillFromRole(role, skills)

    const onRequestRole = () => {
        onToast()
        setDidRequestRole(true)
        //requestRole()
    }

    return (
        <Card.Root padding={'10px'}>
           
            <Card.Title>{`Open role: ${role.roleName} developer`}</Card.Title>
            <Card.Body>
                <Card.Description>
                    <Wrap gap="1">
                        {role.skills.map((skill: any) => {
                            return <Badge>{skill.skillName}</Badge>
                        })}
                </Wrap>
                {(matchingSkill !== "" ? `Recommended: matches your skill ${matchingSkill}` : '')}
                </Card.Description>
            </Card.Body>
            <Card.Footer justifyContent="flex-end">
                <Button disabled={didRequestRole} width={'180px'} onClick={onRequestRole}>
                    {didRequestRole ? "Request pending" : "Request Team Role"}
                    {didRequestRole && <LuClock />}
                    </Button>
            </Card.Footer>
        </Card.Root>
    )
}
export default RoleCard