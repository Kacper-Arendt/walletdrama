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
            // todo fix owner id
            .FirstOrDefaultAsync(t => t.Id == teamId && t.OwnerId == user.Id);

        if (team == null) throw new TeamNotFoundException(teamId);

        // TODO: add team members dto
        return new TeamDetailsDto(team.Id.Value, team.Name.Value, team.OwnerId.Value, team.Members);
    }

    public async Task<IEnumerable<TeamDto>> GetListAsync()
    {
        var user = _httpContextHelper.GetCurrentUser();

        var teams = await _dbContext.Teams
            .AsNoTracking()
            .Where(t => t.OwnerId == user.Id)
            // todo fix owner id to member id 
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