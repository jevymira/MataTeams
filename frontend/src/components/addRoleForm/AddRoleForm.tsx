// libraries 
import { Dispatch, useState } from "react"
import { LuTrash2 } from "react-icons/lu"

// components
import { Box, Checkbox, Flex, IconButton, NumberInput } from "@chakra-ui/react"
import SkillsDropdown from "../skillsDropdown/SkillsDropdown"
import RolesDropdown from "../roleDropdown/RoleDropdown"

// styles
import './AddRoleForm.css'

// types 
import { ProjectFormAction, ProjectRoleCreate, Skill } from '../../types'

type AddRoleFormProps = {
    index: number
    role: ProjectRoleCreate
    dispatch: Dispatch<ProjectFormAction>
}

function AddRoleForm({index, dispatch, role}: AddRoleFormProps) {
    const setRoleId = (roleId: string) => {
        dispatch({type: 'SET_ROLE_ID', payload: {roleId, index}})
    }
    const setFormSkills = (skills: Skill[]) => {
        dispatch({type: 'UPDATE_ROLE_SKILLS', payload: {skills, index}})
    }

    const setRoleNumber = (posititionCount: string) => {
        dispatch({type: 'UPDATE_ROLE_POSITION_COUNT', payload: {posititionCount: parseInt(posititionCount), index}})
    }

    const setLeaderRole = () => {
        dispatch({type: 'SET_LEADER_ROLE', payload: index})
    }

    return (
        <Flex flexDirection={'column'} borderRadius={'5px'} borderWidth={'1px'} borderColor={'var(--secondary)'} marginBottom={'25px'} padding={'10px'}>
            <IconButton alignSelf={'flex-end'} variant='ghost' onClick={(e) => {
                dispatch({type: 'REMOVE_ROLE', payload: index})
            }}>
                    <LuTrash2 aria-label="Remove role"/>
            </IconButton>
            <div className="dropdownWrapper">
                <RolesDropdown labelText="Select role type" setRoleId={setRoleId} />
            </div>
            <NumberInput.Root
                value={role.positionCount.toString()}
                style={{marginBottom: '25px'}}
                onValueChange={(e) => setRoleNumber(e.value)}>
            <NumberInput.Label>How many people for this role?</NumberInput.Label>
            <NumberInput.Control>
                <NumberInput.IncrementTrigger />
                <NumberInput.DecrementTrigger />
            </NumberInput.Control>
            <NumberInput.Scrubber />
            <NumberInput.Input backgroundColor={'white'} />
            </NumberInput.Root>
            <div className="dropdownWrapper">
                <SkillsDropdown setFormSkills={setFormSkills} labelText="Select skills for this role in the project"/>
            </div>
            <Checkbox.Root
                checked={role.isLeaderRole}
                onCheckedChange={(e) => {
                    setLeaderRole()
                }}
                colorPalette={'gray'} variant='solid'>
                <Checkbox.HiddenInput />
                <Checkbox.Control>
                </Checkbox.Control>
                <Checkbox.Label>{"Assign myself to role"}</Checkbox.Label>
            </Checkbox.Root>
        </Flex>
        
    )
}

export default AddRoleForm