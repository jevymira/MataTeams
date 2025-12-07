
// libraries
import { Link } from 'react-router'
import { Box, Text, Badge, Flex, Wrap, Image } from '@chakra-ui/react'

// types
import {UserContextType, Project, ProjectRole, ProjectsContextType, Skill} from '../../types'

// utilities
import { truncateText } from '../../utilities/truncateText'

// images
import cam from '../../images/camera.png'
import comp from '../../images/computer.png'

// style
import './ProjectCard.css'
import { ProjectsContext } from '../../context/project'
import { useContext } from 'react'
import { GoStarFill } from "react-icons/go";
import { UserContext } from '../../context/auth'
import { findMatchingSkill } from '../../utilities/sortFilterProjects'

type ProjectProps = {
    project: Project
    isGoodMatch: boolean
}

const getUniqueSkillsForProject = (project: Project) : Skill[] => {
    let skills: Skill[] = []
    project.roles?.forEach((r: ProjectRole) => {
        r.skills.forEach((skill: Skill) => {
            if (!skills.includes(skill)) {
                skills.push(skill)
            }
        })
    })
    return skills
}

function ProjectCard({project, isGoodMatch} : ProjectProps) {
    const {skills} = useContext(UserContext) as UserContextType
    const { setViewProjectId } = useContext(ProjectsContext) as ProjectsContextType
    return (
        <Box className='projectContainer' justifyContent={'flex-start'}>
            <Flex flexDirection={'column'} alignItems={'flex-start'} justifyContent={'flex-start'}>
                <Flex>
                    <Flex>
                        {project.type =='Film' ? (
                            <Image src={cam} width='100px' height='100px' marginRight={'70px'}></Image>) :
                            (<Image src={comp} width='100px' height='100px' marginRight={'70px'}></Image>)
                        }
                    </Flex>
                    <Flex flexDirection={'column'} width='100%' alignItems={'flex-start'}>
                        <Link to={`/project/view`} className='projectLink' onClick={() => {
                            setViewProjectId(project.id)
                        }}>{project.name} </Link>
                        <Text fontSize={'18px'} fontWeight={190}>{truncateText(project.description)}</Text>
                        <Wrap gap="1">
                            {getUniqueSkillsForProject(project).map(skill => {
                                return <Badge>{skill.name}</Badge>
                            })}
                        </Wrap >
                    </Flex>
                </Flex>
                    {isGoodMatch && (
                        <Flex paddingTop={'10px'}  flexDirection={'row'} alignItems={'center'} alignSelf={'flex-start'}>
                            <GoStarFill color='gold' />
                            {project && <Text fontSize={'16px'} fontWeight={160}paddingLeft={'5px'}>
                                {`Recommended for you: matches your skill ${findMatchingSkill(project, skills)}`}
                                </Text> }
                        </Flex>
                    )}
            </Flex>
        </Box>
    )
  }
  
  export default ProjectCard
  