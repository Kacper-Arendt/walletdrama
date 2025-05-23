using Shared.Abstractions.Events;
using Shared.Abstractions.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Events;

public class InvitationAcceptedEvent : IEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;

    public Guid InvitationId { get; set; }
    public Email Email { get; set; }
    public TeamId TeamId { get; set; }
    public TeamRole Role { get; set; }

    public InvitationAcceptedEvent(Guid invitationId, Email email, TeamId teamId, TeamRole role)
    {
        InvitationId = invitationId;
        Email = email;
        TeamId = teamId;
        Role = role;
    }
}