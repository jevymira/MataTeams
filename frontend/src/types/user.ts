import { Skill } from "./projects"

// TODO: Add programs and split User into separate PublicProfile type
export type User = {
    firstName: string
    lastName: string
    email: string
    skills?: Skill[]
    password: string
}