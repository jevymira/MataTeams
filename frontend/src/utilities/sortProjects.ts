import { Project } from "../types";

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
                if (b.name < a.name) {
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
    }
    return []
}