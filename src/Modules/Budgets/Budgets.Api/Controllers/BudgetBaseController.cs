using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgets.Api.Controllers;

[ApiController]
[Route(RouteBase)]
[Authorize]
public class BudgetBaseController : ControllerBase
{
    public const string RouteBase = "api/budgets";
}