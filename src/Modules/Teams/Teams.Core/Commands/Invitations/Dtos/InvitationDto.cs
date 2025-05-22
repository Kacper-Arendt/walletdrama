using System.ComponentModel.DataAnnotations;
using Shared.Abstractions.ValueObjects;
using Teams.Core.Commands.Invitations.Attributes;
using Teams.Domain.Const;

namespace Teams.Core.Commands.Invitations.Dtos;

public class InvitationDto
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    [NotAdminRole]
    public TeamRole Role { get; set; }
}