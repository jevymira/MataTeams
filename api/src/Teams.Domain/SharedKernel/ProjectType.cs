namespace Teams.Domain.SharedKernel;

/// <summary>
/// "Smart Enum" for Project Types with associated Project Statuses.
/// Encapsulates behavior as in a `class`, but invoked as if an `enum`.
/// </summary>
public sealed class ProjectType : IEquatable<ProjectType>
{
    public string Name { get; }
    public IReadOnlyCollection<ProjectStatus> AllowedStatuses { get; }

    private ProjectType(string name, List<ProjectStatus> allowedStatuses)
    {
        Name = name;
        AllowedStatuses = allowedStatuses;
    }

    public static readonly ProjectType Arcs = new ProjectType(
        "ARCS",
        new List<ProjectStatus>
        {
            ProjectStatus.Draft,
            ProjectStatus.Planning,
            ProjectStatus.Active,
            ProjectStatus.Completed,
            ProjectStatus.Closed
        }
    );

    public static readonly ProjectType Film = new ProjectType(
        "Film",
        new List<ProjectStatus>()
        {
            ProjectStatus.Development,
            ProjectStatus.PreProduction,
            ProjectStatus.Production,
            ProjectStatus.PostProduction,
            ProjectStatus.Released
        }
    );
    
    public static IEnumerable<ProjectType> All => [Arcs, Film];
    
    /// <summary>
    /// Creates a ProjectType based on string, e.g., from user input,
    /// to enforce that only predefined types may be used.
    /// </summary>
    /// <param name="name">The name of the project type.</param>
    /// <returns>ProjectType.</returns>
    public static ProjectType FromName(string name) =>
        All.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException($"Invalid ProjectType: {name}");

    /// <remarks> 
    /// For comparisons between two project types.
    /// </remarks>
    public bool Equals(ProjectType? other) =>
        other != null && Name == other.Name;
    
    /// <remarks> 
    /// For `object.Equals()` and `==` overloads.
    /// </remarks>
    public override bool Equals(object? obj) => Equals(obj as ProjectType);

    /// <remarks> 
    /// For hash-based collections, e.g., `Dictionary`.
    /// </remarks>
    public override int GetHashCode() => Name.GetHashCode();
    
    public override string ToString() => Name;
}

