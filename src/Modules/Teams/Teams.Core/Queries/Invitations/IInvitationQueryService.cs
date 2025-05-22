using Shared.Abstractions.ValueObjects;
using Teams.Core.Queries.Invitations.Dtos;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Queries.Invitations;

public interface IInvitationQueryService
{
    Task<IEnumerable<InvitationDto>> GetAllAsync(TeamId teamId);
    Task<InvitationDto> GetSingleAsync(TeamId teamId);
    
    Task<InvitationDto> GetByIdAsync(Guid invitationId);

    Task<IEnumerable<InvitationDto>> GetUserInvitationsAsync(Email email);
}