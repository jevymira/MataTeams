import { Project, ProjectRole, Skill } from "../types";

export const findMatchingSkillFromRole = (role: ProjectRole, userSkills: Skill[]): string => {
    let match = ''
    let userSkillNames: string[] = userSkills.map(skill => {
        return skill.name
    })
    role.skills.forEach((skill: any) => {
        if (userSkillNames.includes(skill.skillName)) {
            match = skill.skillName
        }
    })
    return match
}

export const findMatchingSkill = (project: Project, userSkills: Skill[]): string => {
    let match = ''
    let userSkillNames: string[] = userSkills.map(skill => {
        return skill.name
    })
    project.roles.forEach((r: ProjectRole) => {
        r.skills.forEach((skill: Skill) => {
            if (userSkillNames.includes(skill.name)) {
                match = skill.name
            }
        })
    })
    return match
}

export const filterProjectsByVacancies = (projects: Project[]) => {
    return projects.filter(project => {
        let hasVacancy = false
        project.roles.forEach(role => {
            if (role.vacantPositionCount && role.vacantPositionCount > 0) {
                hasVacancy = true
            }
        })
        return hasVacancy
    })
}

export const sortProjects = (projects: Project[], sortBy: string): Project[] => {
    switch (sortBy) {
        case 'rec':
            return projects.sort((a, b) => {
                if (!a.matchPercentage) {
                    a.matchPercentage = 0
                }
                if (!b.matchPercentage) {
                    b.matchPercentage = 0
                }
                if (a.matchPercentage < b.matchPercentage) {
                    return -1
                }
                if (b.matchPercentage < a.matchPercentage) {
                    return 1
                }
                return 0
            })
        case 'name_a':
            return projects.sort((a, b) => {
                if (a.name < b.name) {
                    return -1
                }
                if (b.name < a.name) {
                    return 1
                }
                return 0
            })
        case 'name_d':
            return projects.sort((a, b) => {
                if (a.name < b.name) {
                    return 1
                }
                if (b.name < a.name) {
                    return -1
                }
                return 0
            })
        default:
            return projects
    }
}