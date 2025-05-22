using Teams.Core.Commands.Invitations.Dtos;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Commands.Invitations;

public interface IInvitationCommandService
{
    Task SendAsync(TeamId teamId, InvitationDto invitationDto);
    Task AcceptAsync(Guid invitationId);
    Task RejectAsync(Guid invitationId);
    Task DeleteAsync(Guid invitationId);
}