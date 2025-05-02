using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teams.Core.Commands;
using Teams.Core.Commands.Dtos;
using Teams.Core.Queries;

namespace Teams.Api.Controllers;

[ApiController]
[Route("api/teams")]
[Authorize]
public class TeamsController : ControllerBase
{
    private readonly ITeamsCommandService _teamsCommandService;
    private readonly ITeamsQueryService _teamsQueryService;

    public TeamsController(ITeamsCommandService teamsCommandService, ITeamsQueryService teamsQueryService)
    {
        _teamsCommandService = teamsCommandService;
        _teamsQueryService = teamsQueryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var teams = await _teamsQueryService.GetListAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeam(Guid id)
    {
        var team = await _teamsQueryService.GetAsync(id);
        return Ok(team);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto request)
    {
        var teamId = await _teamsCommandService.CreateTeamAsync(request);

        return Ok(new { Id = teamId.Value });
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "IsTeamOwner")]
    public async Task<IActionResult> UpdateTeamDetails(Guid id, [FromBody] UpdateTeamDetailsDto request)
    {
        await _teamsCommandService.UpdateTeamDetailsAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "IsTeamOwner")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        await _teamsCommandService.DeleteTeamAsync(id);
        return NoContent();
    }
}