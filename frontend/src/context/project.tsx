import React, { createContext, useState } from "react"
import { Project, ProjectsContextType } from "../types/index"

export const ProjectsContext = createContext<ProjectsContextType | null>(null)

const ProjectsContextProvider = ({ children }: React.PropsWithChildren<unknown>) => {
    const [projects, setProjects] = useState<Array<Project>>([])
    const [viewProjectId, setViewProjectId] = useState<string>("")

    return <ProjectsContext.Provider value={{
        projects,
        setProjects,
        viewProjectId,
        setViewProjectId
    }}>
        {children}
        </ProjectsContext.Provider>
}

export default ProjectsContextProvider