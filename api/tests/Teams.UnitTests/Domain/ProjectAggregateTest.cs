using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.UnitTests.Domain;

public class ProjectAggregateTest
{
    private readonly string name = "Sample Project";
    private readonly string description = "Sample Description";
    private readonly Guid ownerId = Guid.NewGuid();
    
    [Fact]
    public void Create_Project_Success()
    {
        // Arrange
        var type = ProjectType.Arcs;
        var status = ProjectStatus.Draft;
        
        // Act
        var project = new Project(name, description, type, status, ownerId);
        
        // Assert
        Assert.NotNull(project);
    }

    [Fact]
    public void Create_ProjectType_InvalidProjectType()
    {
        // Arrange
        var type = "Jira";
        
        var ex = Assert.Throws<InvalidOperationException>(() => ProjectType.FromName(type)); 
        Assert.Equal($"Invalid project type: {type}", ex.Message);
    }

    [Fact]
    public void Create_Project_InvalidProjectStatus()
    {
        // Arrange
        var type = ProjectType.Arcs;
        var status = ProjectStatus.PreProduction;
        
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            new Project(name, description, type, status, ownerId));
        
        Assert.Equal("Invalid status for project type.", ex.Message);
    }

    [Fact]
    public void RemoveTeam_Project_Success()
    {
        var project = new Project(
            name,
            description,
            ProjectType.Arcs,
            ProjectStatus.Draft,
            ownerId);

        project.AddTeamToProject("Team 1", ownerId);
        project.AddTeamToProject("Team 2", ownerId);
        project.AddTeamToProject("Team 3", ownerId);
        
        var teamsExceptFirst = project.Teams
            .Except([project.Teams.First()])
            .Select(t => t.Id)
            .ToList();
        
        project.RemoveExcludedTeams(teamsExceptFirst);
        
        Assert.Equal(2, project.Teams.Count);
    }
}