using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Core.Commands.Invitations;
using Teams.Core.Commands.Invitations.Dtos;
using Teams.Domain.ValueObjects;

namespace Teams.Api.Controllers;

[Route(TeamRoute + "/{id}/invitations")]
public class InvitationController : BaseController
{
    private readonly IInvitationCommandService _invitationCommandService;

    public InvitationController(IInvitationCommandService invitationCommandService)
    {
        _invitationCommandService = invitationCommandService;
    }

    [HttpPost]
    [Authorize(Policy = "IsTeamOwner")]
    public async Task<IActionResult> SendInvitation(Guid id, [FromBody] InvitationDto request)
    {
        var teamId = new TeamId(id);
        await _invitationCommandService.SendAsync(teamId, request);
        return NoContent();
    }

    [HttpDelete("{invitationId}")]
    [Authorize(Policy = "IsTeamOwner")]
    public async Task<IActionResult> DeleteInvitation(Guid invitationId)
    {
        await _invitationCommandService.DeleteAsync(invitationId);
        return NoContent();
    }

    [HttpPost("{invitationId}/accept")]
    [Authorize(Policy = "IsInvitationRecipient")]
    public async Task<IActionResult> AcceptInvitation(Guid invitationId)
    {
        await _invitationCommandService.AcceptAsync(invitationId);
        return NoContent();
    }

    [HttpPost("{invitationId}/reject")]
    [Authorize(Policy = "IsInvitationRecipient")]
    public async Task<IActionResult> RejectInvitation(Guid invitationId)
    {
        await _invitationCommandService.RejectAsync(invitationId);
        return NoContent();
    }
}