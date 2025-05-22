using Shared.Abstractions.Events;
using Shared.Abstractions.ValueObjects;
using Teams.Core.Commands.Invitations.Dtos;
using Teams.Core.Events;
using Teams.Core.Exceptions;
using Teams.Domain.Const;
using Teams.Domain.Entities;
using Teams.Domain.ValueObjects;
using Teams.Persistence;

namespace Teams.Core.Commands.Invitations;

public class InvitationCommandService : IInvitationCommandService
{
    private readonly TeamsDbContext _dbContext;
    private readonly IEventPublisher _eventPublisher;

    public InvitationCommandService(TeamsDbContext dbContext, IEventPublisher eventPublisher)
    {
        _dbContext = dbContext;
        _eventPublisher = eventPublisher;
    }

    public async Task SendAsync(TeamId teamId, InvitationDto invitationDto)
    {
        var team = await _dbContext.Teams.FindAsync(teamId);

        if (team == null) throw new TeamNotFoundException(teamId);

        var invitation = new TeamInvitation
        {
            TeamId = teamId,
            Email = new Email(invitationDto.Email),
            CreatedAt = DateTime.UtcNow,
            Role = invitationDto.Role,
            Status = InvitationStatus.Accepted
        };

        await _dbContext.TeamInvitations.AddAsync(invitation);
        await _dbContext.SaveChangesAsync();

        var invitationCreatedEvent = new InvitationCreatedEvent(
            invitation.Id,
            invitation.Email,
            team.Name,
            invitation.Role
        );
        await _eventPublisher.PublishAsync(invitationCreatedEvent);
    }

    public async Task AcceptAsync(Guid invitationId)
    {
        var invitation = await _dbContext.TeamInvitations.FindAsync(invitationId);

        if (invitation == null) throw new InvitationNotFoundException(invitationId);

        invitation.Status = InvitationStatus.Accepted;

        // Throw event to notify other services about the acceptance

        await _dbContext.SaveChangesAsync();
    }

    public async Task RejectAsync(Guid invitationId)
    {
        var invitation = await _dbContext.TeamInvitations.FindAsync(invitationId);

        if (invitation == null) throw new InvitationNotFoundException(invitationId);

        invitation.Status = InvitationStatus.Revoked;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid invitationId)
    {
        var invitation = await _dbContext.TeamInvitations.FindAsync(invitationId);

        if (invitation == null) throw new InvitationNotFoundException(invitationId);

        _dbContext.TeamInvitations.Remove(invitation);

        await _dbContext.SaveChangesAsync();
    }
}