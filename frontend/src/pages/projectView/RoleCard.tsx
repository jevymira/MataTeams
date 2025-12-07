import { Button, Card, Badge, Wrap } from "@chakra-ui/react"
import { ProjectRole, UserContextType } from "../../types"
import { findMatchingSkillFromRole } from "../../utilities/sortFilterProjects"
import { UserContext } from "../../context/auth"
import { useContext } from "react"

type RoleCardProps = {
    role: ProjectRole
}

function RoleCard({role}: RoleCardProps) {
    const {skills} = useContext(UserContext) as UserContextType
    const matchingSkill = findMatchingSkillFromRole(role, skills)
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
            <Card.Footer>
                <Button width={'180px'} alignSelf={'center'}>Request to Join Team</Button>
            </Card.Footer>
        </Card.Root>
    )
}
export default RoleCard