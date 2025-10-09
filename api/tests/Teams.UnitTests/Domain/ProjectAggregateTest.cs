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
        var project = new Project(Guid.NewGuid(), name, description, type, status, ownerId);
        
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
            new Project(Guid.NewGuid(), name, description, type, status, ownerId));
        
        Assert.Equal("Invalid status for project type.", ex.Message);
    }
}