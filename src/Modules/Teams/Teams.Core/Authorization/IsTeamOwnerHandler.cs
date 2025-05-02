using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Infrastructure.Helpers;
using Teams.Core.Queries;
using Teams.Domain.ValueObjects;

namespace Teams.Core.Authorization;

public class IsTeamOwnerHandler : AuthorizationHandler<IsTeamOwnerRequirement>
{
    private readonly ITeamsQueryService _teamsQueryService;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsTeamOwnerHandler(
        ITeamsQueryService teamsQueryService,
        HttpContextHelper httpContextHelper,
        IHttpContextAccessor httpContextAccessor)
    {
        _teamsQueryService = teamsQueryService;
        _httpContextHelper = httpContextHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsTeamOwnerRequirement requirement)
    {
        if (!TryGetTeamId(out var teamId))
        {
            context.Fail();
            return;
        }

        var user = _httpContextHelper.GetCurrentUser();
        if (!await _teamsQueryService.IsUserOwnerOfTeamAsync(user.Id.Value, teamId))
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }

    private bool TryGetTeamId(out TeamId teamId)
    {
        teamId = null;

        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();

        if (routeData == null ||
            !routeData.Values.TryGetValue("id", out var teamIdValue) ||
            !Guid.TryParse(teamIdValue?.ToString(), out var teamIdGuid))
        {
            return false;
        }

        teamId = new TeamId(teamIdGuid);
        return true;
    }
}