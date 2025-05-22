using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teams.Api.Controllers;

[ApiController]
[Route(TeamRoute)]
[Authorize]
public class BaseController: ControllerBase
{
    public const string TeamRoute = "api/teams";
}