import { Skill } from "./projects"

export type AuthContextType = {
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

    //most of this is actually for user. Move all but token/setToken later
    //related changes for auth.tsx/signup.tsx/signup.ts
}