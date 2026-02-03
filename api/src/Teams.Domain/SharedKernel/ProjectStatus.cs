namespace Teams.Domain.SharedKernel;

public enum ProjectStatus {
    // Default (Programming)
    Draft,
    Planning,
    Active,
    Completed,
    Closed,
    
    // Filmmaking
    Development,
    PreProduction,
    Production,
    PostProduction,
    Released,

    // Game design
    Alpha,
    Beta,
    Gold,

    // Business
    Funding,
    Launched
}