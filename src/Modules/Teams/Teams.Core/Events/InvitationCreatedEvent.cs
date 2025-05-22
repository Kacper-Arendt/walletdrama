using Shared.Abstractions.Events;
using Shared.Abstractions.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Events;

public class InvitationCreatedEvent : IEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public Guid InvitationId { get; set; }
    public Email Email { get; set; }
    public TeamName TeamName { get; set; }
    public TeamRole Role { get; set; }
    
    public InvitationCreatedEvent(Guid invitationId, Email email, TeamName teamName, TeamRole role)
    {
        InvitationId = invitationId;
        Email = email;
        TeamName = teamName;
        Role = role;
    }
}