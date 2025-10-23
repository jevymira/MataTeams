// libraries 
import { useState } from "react"

// components
import { Container, NumberInput } from "@chakra-ui/react"
import SkillsDropdown from "../skillsDropdown/SkillsDropdown"

// styles
import './AddRoleForm.css'
import RolesDropdown from "../roleDropdown/RoleDropdown"

function AddRoleForm() {
    const [skills, setSkills] = useState<string[]>([])

    const setFormSkills = (skillData: string[]) => {
        setSkills(skillData)
    }
    
    return (
        <Container maxWidth={500}>
        <div className="dropdownWrapper">
            <RolesDropdown labelText="Select role type"/>
        </div>
        <NumberInput.Root>
        <NumberInput.Label>How many people for this role?</NumberInput.Label>
        <NumberInput.ValueText />
        <NumberInput.Control>
            <NumberInput.IncrementTrigger />
            <NumberInput.DecrementTrigger />
        </NumberInput.Control>
        <NumberInput.Scrubber />
        <NumberInput.Input />
        </NumberInput.Root>
        <div className="dropdownWrapper">
            <SkillsDropdown setFormSkills={setFormSkills} labelText="Select skills for this role in the project"/>
        </div>
        </Container>
        
    )
}

export default AddRoleForm