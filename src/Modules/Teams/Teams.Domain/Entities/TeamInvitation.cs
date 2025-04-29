using Shared.Domain.ValueObjects;
using Teams.Domain.Const;
using Teams.Domain.ValueObjects;

namespace Teams.Domain.Entities;

public class TeamInvitation
{
    public Guid Id { get; set; }
    public TeamId TeamId { get; set; }
    public UserId UserId { get; set; } 
    public DateTime CreatedAt { get; set; }
    
    public InvitationStatus Status { get; set; }
}