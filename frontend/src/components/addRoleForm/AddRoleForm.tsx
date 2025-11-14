// libraries 
import { Dispatch, useState } from "react"

// components
import { Box, Container, NumberInput } from "@chakra-ui/react"
import SkillsDropdown from "../skillsDropdown/SkillsDropdown"
import RolesDropdown from "../roleDropdown/RoleDropdown"

// styles
import './AddRoleForm.css'

// types 
import { ProjectFormAction, ProjectRole, Skill } from '../../types'

type AddRoleFormProps = {
    index: number
    role: ProjectRole
    dispatch: Dispatch<ProjectFormAction>
}

function AddRoleForm({index, dispatch, role}: AddRoleFormProps) {
    const setFormSkills = (skills: Skill[]) => {
        dispatch({type: 'UPDATE_ROLE_SKILLS', payload: {skills, index}})
    }

    const setRoleNumber = (posititionCount: string) => {
        dispatch({type: 'UPDATE_ROLE_POSITION_COUNT', payload: {posititionCount, index}})

    }

    return (
        <Box borderRadius={'5px'} borderWidth={'1px'} borderColor={'var(--secondary)'} marginBottom={'25px'} padding={'10px'}>
            <div className="dropdownWrapper">
                <RolesDropdown labelText="Select role type"/>
            </div>
            <NumberInput.Root
                value={role.positionCount}
                style={{marginBottom: '25px'}}
                onValueChange={(e) => setRoleNumber(e.value)}>
            <NumberInput.Label>How many people for this role?</NumberInput.Label>
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
        </Box>
        
    )
}

export default AddRoleForm