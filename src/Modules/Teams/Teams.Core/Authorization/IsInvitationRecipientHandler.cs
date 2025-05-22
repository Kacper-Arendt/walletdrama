using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Abstractions.ValueObjects;
using Shared.Infrastructure.Helpers;
using Teams.Core.Queries;
using Teams.Core.Queries.Invitations;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Authorization;

public class IsInvitationRecipientHandler : AuthorizationHandler<IsInvitationRecipientRequirement>
{
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IInvitationQueryService _invitationQueryService;

    public IsInvitationRecipientHandler(
        HttpContextHelper httpContextHelper,
        IHttpContextAccessor httpContextAccessor, IInvitationQueryService invitationQueryService)
    {
        _httpContextHelper = httpContextHelper;
        _httpContextAccessor = httpContextAccessor;
        _invitationQueryService = invitationQueryService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsInvitationRecipientRequirement requirement)
    {
        if (!TryGetInvitationId(out var invitationId) || invitationId == null)
        {
            context.Fail();
            return;
        }

        var user = _httpContextHelper.GetCurrentUser();
        var invitation = await _invitationQueryService.GetByIdAsync((Guid)invitationId);
        var userId = new Email(user.Email);

        var isValid = invitation.Email.Equals(userId);

        if (!isValid)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }

    private bool TryGetInvitationId(out Guid? invitationId)
    {
        invitationId = null;

        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();

        if (routeData == null ||
            !routeData.Values.TryGetValue("invitationId", out var invitationIdValue) ||
            !Guid.TryParse(invitationIdValue?.ToString(), out var invitationIdGuid))
        {
            return false;
        }

        invitationId = invitationIdGuid;
        return true;
    }
}