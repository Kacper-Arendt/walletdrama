using Shared.Abstractions.Communication;
using Shared.Abstractions.ValueObjects;
using Teams.Core.Commands.Invitations.Dtos;
using Teams.Core.Exceptions;
using Teams.Domain.Const;
using Teams.Domain.Entities;
using Teams.Domain.ValueObjects;
using Teams.Persistence;

namespace Teams.Core.Commands.Invitations;

public class InvitationCommandService : IInvitationCommandService
{
    private readonly TeamsDbContext _dbContext;
    private readonly IEmailSender _emailSender;

    public InvitationCommandService(TeamsDbContext dbContext, IEmailSender emailSender)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
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

        var body = $@"
            <p style='color:#555;font-size:16px;font-family:sans-serif;margin-bottom:32px;'>
                You have been invited to join the team <strong>{team.Name.Value}</strong>. Click below to accept the invitation.
            </p>
            <a href='xx' style='display:inline-block;padding:12px 28px;background-color:#4f46e5;color:#fff;text-decoration:none;border-radius:4px;font-size:16px;font-family:sans-serif;'>
                Accept Invitation
            </a>";

        await _emailSender.SendEmailAsync(
            [invitation.Email],
            "Invitation to join team",
            body
        );
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