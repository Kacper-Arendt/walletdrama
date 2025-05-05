using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Helpers;
using Teams.Core.Exceptions;
using Teams.Core.Queries.Dtos;
using Teams.Persistence;

namespace Teams.Core.Queries;

public class TeamsQueryService : ITeamsQueryService
{
    private readonly TeamsDbContext _dbContext;
    private readonly HttpContextHelper _httpContextHelper;

    public TeamsQueryService(TeamsDbContext dbContext, HttpContextHelper httpContextHelper)
    {
        _dbContext = dbContext;
        _httpContextHelper = httpContextHelper;
    }

    public async Task<TeamDetailsDto> GetAsync(Guid teamId)
    {
        var user = _httpContextHelper.GetCurrentUser();

        var team = await _dbContext.Teams
            .Include(t => t.Members)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == teamId && t.Members.Any(m => m.UserId == user.Id));

        if (team == null) throw new TeamNotFoundException(teamId);

        var teamMembers = team.Members
            .Select(m => new TeamMemberDto(m.UserId.Value, m.Email.Value, m.Role.ToString()))
            .ToList();
        return new TeamDetailsDto(team.Id.Value, team.Name.Value, team.OwnerId.Value, teamMembers);
    }

    public async Task<IEnumerable<TeamDto>> GetListAsync()
    {
        var user = _httpContextHelper.GetCurrentUser();

        var teams = await _dbContext.Teams
            .AsNoTracking()
            .Include(t => t.Members)
            .Where(t => t.Members.Any(m => m.UserId == user.Id))
            .Select(t => new TeamDto(t.Id.Value, t.Name.Value, t.OwnerId.Value))
            .ToListAsync();

        return teams;
    }

    public async Task<bool> IsUserOwnerOfTeamAsync(Guid userId, Guid teamId)
    {
        var team = await _dbContext.Teams
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == teamId && t.OwnerId == userId);

        return team != null;
    }
}