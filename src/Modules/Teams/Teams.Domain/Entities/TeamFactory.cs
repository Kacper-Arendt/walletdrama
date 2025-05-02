using Shared.Domain.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Domain.Entities;

public static class TeamFactory
{
    public static Team Create(TeamName name, UserId ownerId, Email ownerEmail)
    {
        var teamId = new TeamId(Guid.NewGuid());

        var ownerMember = new TeamMember
        {
            MemberId = new TeamMemberId(Guid.NewGuid()),
            UserId = ownerId,
            Role = TeamRole.Owner,
            TeamId = teamId,
            Email = ownerEmail
        };

        return new Team
        {
            Id = new TeamId(Guid.NewGuid()),
            Name = name,
            OwnerId = ownerId,
            Members = [ownerMember]
        };
    }
}