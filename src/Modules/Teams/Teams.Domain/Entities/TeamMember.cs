using Shared.Abstractions.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Domain.Entities;

public class TeamMember
{
    public TeamMemberId MemberId { get; set; }
    public TeamId TeamId { get; set; }
    public UserId UserId { get; set; }
    public Email Email { get; set; }

    public TeamRole Role { get; set; }
}