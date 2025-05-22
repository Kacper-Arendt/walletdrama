using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.ValueObjects;
using Teams.Core.Exceptions;
using Teams.Core.Queries.Invitations.Dtos;
using Teams.Domain.ValueObjects;
using Teams.Persistence;

namespace Teams.Core.Queries.Invitations;

public class InvitationQueryService : IInvitationQueryService
{
    private readonly TeamsDbContext _dbContext;

    public InvitationQueryService(TeamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<InvitationDto>> GetAllAsync(TeamId teamId)
    {
        var invitations = await _dbContext.TeamInvitations
            .Where(i => i.TeamId == teamId)
            .AsNoTracking()
            .Select(i => new InvitationDto(
                i.Id,
                i.TeamId,
                i.Email,
                i.CreatedAt,
                i.Role,
                i.Status))
            .ToListAsync();

        return invitations;
    }

    public async Task<InvitationDto> GetSingleAsync(TeamId teamId)
    {
        var invitation = await _dbContext.TeamInvitations
            .Where(i => i.TeamId == teamId)
            .AsNoTracking()
            .Select(i => new InvitationDto(
                i.Id,
                i.TeamId,
                i.Email,
                i.CreatedAt,
                i.Role,
                i.Status))
            .FirstOrDefaultAsync();

        return invitation;
    }

    public async Task<InvitationDto> GetByIdAsync(Guid invitationId)
    {
        var invitation = await _dbContext.TeamInvitations
            .Where(i => i.Id == invitationId)
            .AsNoTracking()
            .Select(i => new InvitationDto(
                i.Id,
                i.TeamId,
                i.Email,
                i.CreatedAt,
                i.Role,
                i.Status))
            .FirstOrDefaultAsync();

        if (invitation == null) throw new InvitationNotFoundException(invitationId);

        return invitation;
    }

    public async Task<IEnumerable<InvitationDto>> GetUserInvitationsAsync(Email email)
    {
        var invitations = await _dbContext.TeamInvitations
            .Where(i => i.Email == email)
            .AsNoTracking()
            .Select(i => new InvitationDto(
                i.Id,
                i.TeamId,
                i.Email,
                i.CreatedAt,
                i.Role,
                i.Status))
            .ToListAsync();

        return invitations;
    }
}