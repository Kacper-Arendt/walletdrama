using Shared.Abstractions.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Domain.Entities;

public class TeamInvitation
{
    public Guid Id { get; set; }
    public TeamId TeamId { get; set; }
    public Email Email { get; set; } 
    public DateTime CreatedAt { get; set; }
    public TeamRole Role { get; set; }
    
    public InvitationStatus Status { get; set; }
}