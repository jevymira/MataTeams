namespace Teams.Domain.Aggregates.TeamAggregate;

public enum TeamMembershipRequestStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Expired
}