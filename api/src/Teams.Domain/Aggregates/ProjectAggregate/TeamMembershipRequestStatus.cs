namespace Teams.Domain.Aggregates.ProjectAggregate;

public enum TeamMembershipRequestStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Expired
}