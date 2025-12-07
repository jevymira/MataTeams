// libraries
import { useContext, useState } from "react"
import { Button, Card, Badge, Wrap } from "@chakra-ui/react"
import { LuClock } from "react-icons/lu"
import { ToastContainer, toast } from 'react-toastify'

// types
import { ProjectRole, UserContextType } from "../../types"

// utiltiies
import { findMatchingSkillFromRole } from "../../utilities/sortFilterProjects"

// context
import { UserContext } from "../../context/auth"

type RoleCardProps = {
    role: ProjectRole
    teamID: string
}

function RoleCard({role, teamID}: RoleCardProps) {
    const {skills} = useContext(UserContext) as UserContextType
    const [requestedRole, setRequestedRole] = useState(false)
    const matchingSkill = findMatchingSkillFromRole(role, skills)

    const requestRole = () => {
        toast("Sent request to join project role!")
        setRequestedRole(true)
    }

    return (
        <Card.Root padding={'10px'}>
            <ToastContainer theme={"dark"} closeOnClick={true}/>
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
                <Button disabled={requestedRole} width={'180px'} onClick={requestRole}>
                    {requestedRole ? "Request pending" : "Request Team Role"}
                    {requestedRole && <LuClock />}
                    </Button>
            </Card.Footer>
        </Card.Root>
    )
}
export default RoleCard