import { Skill } from "./projects"

export type UserContextType = {
    userID: number
    username: string
    firstName: string
    lastName: string
    token: string
    skills: Array<Skill>
    setToken: (token: string) => void
    setUserID: (userID: string) => void
    setUsername: (username: string) => void
    setFirst: (firstName: string) => void
    setLast: (lastName: string) => void
    setSkills:(skills: Array<Skill>) => void
}