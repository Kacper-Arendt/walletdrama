using Shared.Abstractions.ValueObjects;
using Teams.Domain.ValueObjects;

namespace Teams.Domain.Entities;

public class Team
{
    public TeamId Id { get; set; }
    public TeamName Name { get; set; }
    public UserId OwnerId { get; set; }

    public List<TeamMember> Members { get; set; } = [];
}