import React, { createContext, useState } from "react"
import { Project, ProjectsContextType } from "../types"

export const ProjectsContext = createContext<ProjectsContextType | null>(null)

const ProjectsContextProvider = ({ children }: React.PropsWithChildren<unknown>) => {
    const [projects, setProjects] = useState<Array<Project>>([])

    return <ProjectsContext.Provider value={{
        projects,
        setProjects
    }}>
        {children}
        </ProjectsContext.Provider>
}

export default ProjectsContextProvider